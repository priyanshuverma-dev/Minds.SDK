using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minds.SDK
{
    /// <summary>
    /// The <see cref="Minds"/> class provides methods to manage and interact with MindsDB's mind entities.
    /// It allows users to list, create, retrieve, and delete minds and check or create datasources.
    /// </summary>
    public class Minds
    {
        private readonly Client _client;
        private readonly string _project = "mindsdb";
        internal Client Client => _client;
        /// <summary>
        /// Initializes a new instance of the <see cref="Minds"/> class with the provided client.
        /// </summary>
        /// <param name="client">The client instance used to make API requests.</param>
        public Minds(Client client)
        {
            _client = client;
        }

        /// <summary>
        /// Retrieves a list of all minds in the current project.
        /// </summary>
        /// <returns>A list of <see cref="Mind"/> objects.</returns>
        public async Task<List<Mind>> List()
        {
            var data = await _client.Api.Get<List<Mind>>($"/projects/{_project}/minds");
            // Creates Mind instances based on the retrieved data
            return data.Select(item => new Mind(_client, item.Name, item.ModelName, item.Provider,
                                                item.Parameters, item.Datasources,
                                                item.CreatedAt, item.UpdatedAt)).ToList();
        }

        /// <summary>
        /// Retrieves a specific mind by name.
        /// </summary>
        /// <param name="name">The name of the mind to retrieve.</param>
        /// <returns>A <see cref="Mind"/> object representing the retrieved mind.</returns>
        public async Task<Mind> Get(string name)
        {
            var item = await _client.Api.Get<Mind>($"/projects/{_project}/minds/{name}");
            return item;
        }

        /// <summary>
        /// Verifies and retrieves the name of a datasource.
        /// </summary>
        /// <param name="ds">The datasource object, which can be a <see cref="Datasource"/>, a <see cref="DatabaseConfig"/>, or a string.</param>
        /// <returns>The name of the datasource as a string.</returns>
        /// <exception cref="ArgumentException">Thrown if the datasource type is unknown.</exception>
        public string CheckDatasource(object ds)
        {
            if (ds is Datasource datasource)
            {
                return datasource.Name;
            }
            else if (ds is DatabaseConfig config)
            {
                // Handle datasource creation logic if needed
                return config.Name;
            }
            else if (ds is string name)
            {
                return name;
            }
            else
            {
                throw new ArgumentException($"Unknown type of datasource: {ds}");
            }
        }

        /// <summary>
        /// Creates a new mind with the given configuration options.
        /// Optionally, it can replace an existing mind with the same name.
        /// </summary>
        /// <param name="name">The name of the mind to create.</param>
        /// <param name="modelName">The name of the model to associate with the mind (optional).</param>
        /// <param name="provider">The provider for the model (optional).</param>
        /// <param name="promptTemplate">A prompt template for the mind (optional).</param>
        /// <param name="datasources">A list of datasources associated with the mind (optional).</param>
        /// <param name="parameters">A dictionary of parameters to configure the mind (optional).</param>
        /// <param name="replace">If set to true, will replace an existing mind with the same name (optional).</param>
        /// <returns>A <see cref="Mind"/> object representing the newly created mind.</returns>
        /// <exception cref="ObjectNotFoundException">Thrown if the mind to replace doesn't exist.</exception>
        public async Task<Mind> Create(string name, string modelName = null, string provider = null,
                                        string promptTemplate = null, List<string> datasources = null,
                                        Dictionary<string, object> parameters = null, bool replace = false)
        {
            if (replace)
            {
                try
                {
                    await Get(name); // Check if the mind exists
                    await Drop(name); // Delete if it exists
                }
                catch (ObjectNotFoundException)
                {
                    // Mind not found, proceed with creation
                }
            }

            // Resolve datasource names
            var dsNames = datasources?.Select(ds => CheckDatasource(ds)).ToList() ?? new List<string>();

            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }
            if (promptTemplate != null)
            {
                parameters["prompt_template"] = promptTemplate;
            }

            // Prepare data for mind creation
            var data = new
            {
                name,
                model_name = modelName,
                provider,
                parameters,
                datasources = dsNames
            };

            // Create the mind via the API
            await _client.Api.Post($"/projects/{_project}/minds", data);

            // Retrieve and return the newly created mind
            return await Get(name);
        }

        /// <summary>
        /// Deletes a mind by name from the project.
        /// </summary>
        /// <param name="name">The name of the mind to delete.</param>
        public async Task Drop(string name)
        {
            await _client.Api.Delete($"/projects/{_project}/minds/{name}");
        }
    }
}
