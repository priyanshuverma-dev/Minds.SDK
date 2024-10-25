using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Minds.SDK;

namespace Minds.SDK {
    public class RestAPI {
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public RestAPI(string apiKey, string baseUrl = null) {
            _baseUrl = (baseUrl ?? "https://mdb.ai").TrimEnd('/');
            if (!_baseUrl.EndsWith("/api")) {
                _baseUrl += "/api";
            }

            _apiKey = apiKey;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        private async Task RaiseForStatus(HttpResponseMessage response) {
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                throw new ObjectNotFoundException(await response.Content.ReadAsStringAsync());
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden) {
                throw new ForbiddenException(await response.Content.ReadAsStringAsync());
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) {
                throw new UnauthorizedException(await response.Content.ReadAsStringAsync());
            }

            if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 600) {
                throw new UnknownErrorException($"{response.ReasonPhrase}: {await response.Content.ReadAsStringAsync()}");
            }
        }

        public async Task<T> Get<T>(string url) {
            var response = await _httpClient.GetAsync(_baseUrl + url);
            await RaiseForStatus(response);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<HttpResponseMessage> Delete(string url) {
            var response = await _httpClient.DeleteAsync(_baseUrl + url);
            await RaiseForStatus(response);
            return response;
        }

        public async Task<HttpResponseMessage> Post(string url, object data) {
            var json = JsonConvert.SerializeObject(data);  // Use Newtonsoft.Json for serialization
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseUrl + url, content);
            await RaiseForStatus(response);
            return response;
        }

        public async Task<HttpResponseMessage> Patch(string url, object data) {
            var json = JsonConvert.SerializeObject(data);  // Use Newtonsoft.Json for serialization
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Manually create a PATCH request
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), _baseUrl + url) {
                Content = content
            };

            var response = await _httpClient.SendAsync(request);  // Send the request asynchronously
            await RaiseForStatus(response);
            return response;
        }

      

    }
}
