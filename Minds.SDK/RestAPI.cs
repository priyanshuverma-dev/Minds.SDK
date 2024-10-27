using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Minds.SDK
{
    /// <summary>
    /// Represents the RestAPI class that provides functionality to interact with MindsDB API endpoints.
    /// </summary>
    public class RestAPI
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;


        /// <summary>
        /// Gets the API key used for authenticating requests.
        /// </summary>
        internal string ApiKey => _apiKey;

        /// <summary>
        /// Gets the base URL of the API.
        /// </summary>
        internal string BaseUrl => _baseUrl;



        /// <summary>
        /// Initializes a new instance of the <see cref="RestAPI"/> class.
        /// Sets up the API base URL and initializes the HttpClient with the provided API key for authorization.
        /// </summary>
        /// <param name="apiKey">The API key used for authenticating requests.</param>
        /// <param name="baseUrl">The base URL of the API (optional). If not provided, defaults to https://mdb.ai.</param>
        public RestAPI(string apiKey, string baseUrl = null)
        {
            // Set the base URL and append "/api" if it's not already included.
            _baseUrl = (baseUrl ?? "https://mdb.ai").TrimEnd('/');
            if (!_baseUrl.EndsWith("/api"))
            {
                _baseUrl += "/api";
            }

            _apiKey = apiKey;

            // Initialize HttpClient and set the Authorization header.
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        /// <summary>
        /// Raises appropriate exceptions based on the HTTP status code of the response.
        /// Handles NotFound (404), Forbidden (403), Unauthorized (401), and other error responses.
        /// </summary>
        /// <param name="response">The HTTP response received from the API.</param>
        private async Task RaiseForStatus(HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new ObjectNotFoundException(await response.Content.ReadAsStringAsync());
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                throw new ForbiddenException(await response.Content.ReadAsStringAsync());
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException(await response.Content.ReadAsStringAsync());
            }

            if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 600)
            {
                throw new UnknownErrorException($"{response.ReasonPhrase}: {await response.Content.ReadAsStringAsync()}");
            }
        }

        /// <summary>
        /// Sends an asynchronous GET request to the specified URL and returns the deserialized response.
        /// </summary>
        /// <typeparam name="T">The type of the response object.</typeparam>
        /// <param name="url">The URL relative to the API base path.</param>
        /// <returns>A task that represents the asynchronous operation, with the deserialized response as the result.</returns>
        public async Task<T> Get<T>(string url)
        {
            var response = await _httpClient.GetAsync(_baseUrl + url);
            await RaiseForStatus(response);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        /// <summary>
        /// Sends an asynchronous DELETE request to the specified URL.
        /// </summary>
        /// <param name="url">The URL relative to the API base path.</param>
        /// <returns>A task that represents the asynchronous operation, with the HTTP response as the result.</returns>
        public async Task<HttpResponseMessage> Delete(string url)
        {
            var response = await _httpClient.DeleteAsync(_baseUrl + url);
            await RaiseForStatus(response);
            return response;
        }

        /// <summary>
        /// Sends an asynchronous POST request with the specified data serialized as JSON.
        /// </summary>
        /// <param name="url">The URL relative to the API base path.</param>
        /// <param name="data">The object to be serialized and sent in the request body.</param>
        /// <returns>A task that represents the asynchronous operation, with the HTTP response as the result.</returns>
        public async Task<HttpResponseMessage> Post(string url, object data)
        {
            var json = JsonConvert.SerializeObject(data);  // Use Newtonsoft.Json for serialization
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseUrl + url, content);
            await RaiseForStatus(response);
            return response;
        }

        /// <summary>
        /// Sends an asynchronous PATCH request with the specified data serialized as JSON.
        /// </summary>
        /// <param name="url">The URL relative to the API base path.</param>
        /// <param name="data">The object to be serialized and sent in the request body.</param>
        /// <returns>A task that represents the asynchronous operation, with the HTTP response as the result.</returns>
        public async Task<HttpResponseMessage> Patch(string url, object data)
        {
            var json = JsonConvert.SerializeObject(data);  // Use Newtonsoft.Json for serialization
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Manually create a PATCH request
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), _baseUrl + url)
            {
                Content = content
            };

            var response = await _httpClient.SendAsync(request);  // Send the request asynchronously
            await RaiseForStatus(response);
            return response;
        }
    }
}
