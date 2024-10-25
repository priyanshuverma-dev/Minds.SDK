namespace Minds.SDK
{
    /// <summary>
    /// The <see cref="Client"/> class provides access to MindsDB API functionalities.
    /// It manages the connection to the API, exposes interfaces for interacting with datasources and minds, and acts as a wrapper for the API calls.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Gets the <see cref="RestAPI"/> instance that manages API calls to MindsDB.
        /// </summary>
        public RestAPI Api { get; }

        /// <summary>
        /// Gets the <see cref="Datasources"/> manager that handles datasource-related operations.
        /// </summary>
        public Datasources Datasources { get; }

        /// <summary>
        /// Gets the <see cref="Minds"/> manager that handles mind-related operations.
        /// </summary>
        public Minds Minds { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class, which provides access to the MindsDB API, datasources, and minds.
        /// </summary>
        /// <param name="apiKey">The API key required for authentication with the MindsDB API.</param>
        /// <param name="baseUrl">The base URL of the MindsDB API (optional).</param>
        public Client(string apiKey, string baseUrl = null)
        {
            // Initialize the API instance with the provided API key and optional base URL
            Api = new RestAPI(apiKey, baseUrl);

            // Initialize the Datasources and Minds components with a reference to this client
            Datasources = new Datasources(this);
            Minds = new Minds(this);
        }
    }
}
