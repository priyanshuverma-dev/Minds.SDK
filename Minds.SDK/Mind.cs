using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minds.SDK
{
    /// <summary>
    /// Represents a mind entity in the Minds SDK.
    /// Provides methods to update, add, or delete datasources linked to the mind, and manages mind configuration settings.
    /// </summary>
    public class Mind
    {
        private readonly Client _client;
        private readonly string _project = "mindsdb";

        /// <summary>
        /// Gets the name of the mind.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the model name associated with the mind.
        /// </summary>
        public string ModelName { get; private set; }

        /// <summary>
        /// Gets the provider associated with the mind.
        /// </summary>
        public string Provider { get; private set; }

        /// <summary>
        /// Gets the parameters of the mind as a dictionary.
        /// </summary>
        public Dictionary<string, object> Parameters { get; private set; }

        /// <summary>
        /// Gets the list of datasources linked to the mind.
        /// </summary>
        public List<string> Datasources { get; private set; }

        /// <summary>
        /// Gets the date and time when the mind was created.
        /// </summary>
        public DateTime? CreatedAt { get; private set; }

        /// <summary>
        /// Gets the date and time when the mind was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mind"/> class with the specified client and configuration options.
        /// </summary>
        /// <param name="client">The client instance for making API requests.</param>
        /// <param name="name">The name of the mind.</param>
        /// <param name="modelName">The name of the model to use.</param>
        /// <param name="provider">The provider of the model.</param>
        /// <param name="parameters">A dictionary of parameters for configuring the mind.</param>
        /// <param name="datasources">A list of datasources associated with the mind.</param>
        /// <param name="createdAt">The creation date of the mind.</param>
        /// <param name="updatedAt">The last update date of the mind.</param>
        public Mind(Client client, string name, string modelName = null, string provider = null,
                    Dictionary<string, object> parameters = null, List<string> datasources = null,
                    DateTime? createdAt = null, DateTime? updatedAt = null)
        {
            _client = client;
            Name = name;
            ModelName = modelName;
            Provider = provider;
            Parameters = parameters ?? new Dictionary<string, object>();
            Datasources = datasources ?? new List<string>();
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        /// <summary>
        /// Returns a string that represents the current object, including its name, model, provider, creation, and update timestamps.
        /// </summary>
        /// <returns>A string that represents the current mind object.</returns>
        public override string ToString()
        {
            return $"Mind(Name={Name}, ModelName={ModelName}, Provider={Provider}, CreatedAt={CreatedAt}, UpdatedAt={UpdatedAt}, Parameters={Parameters}, Datasources={Datasources})";
        }

        /// <summary>
        /// Updates the mind configuration by optionally changing its name, model, provider, prompt template, datasources, and parameters.
        /// </summary>
        /// <param name="name">The new name for the mind (optional).</param>
        /// <param name="modelName">The new model name for the mind (optional).</param>
        /// <param name="provider">The new provider for the mind (optional).</param>
        /// <param name="promptTemplate">The new prompt template to use (optional).</param>
        /// <param name="datasources">A list of datasources to associate with the mind (optional).</param>
        /// <param name="parameters">A dictionary of parameters to update (optional).</param>
        public async Task Update(string name = null, string modelName = null, string provider = null,
                                 string promptTemplate = null, List<string> datasources = null,
                                 Dictionary<string, object> parameters = null)
        {
            var data = new Dictionary<string, object>();

            if (datasources != null)
            {
                data["datasources"] = datasources.Select(ds => _client.Minds.CheckDatasource(ds)).ToList();
            }

            if (name != null)
            {
                data["name"] = name;
            }

            if (modelName != null)
            {
                data["model_name"] = modelName;
            }

            if (provider != null)
            {
                data["provider"] = provider;
            }

            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }

            if (promptTemplate != null)
            {
                parameters["prompt_template"] = promptTemplate;
            }

            data["parameters"] = parameters;

            await _client.Api.Patch($"/projects/{_project}/minds/{Name}", data);

            // Update the local Name if it was changed
            if (name != null && name != Name)
            {
                Name = name;
            }
        }

        /// <summary>
        /// Adds a datasource to the mind by name.
        /// </summary>
        /// <param name="datasource">The name of the datasource to add.</param>
        public async Task AddDatasource(string datasource)
        {
            var dsName = _client.Minds.CheckDatasource(datasource);
            await _client.Api.Post($"/projects/{_project}/minds/{Name}/datasources", new { name = dsName });

            // Fetch the updated mind to refresh the list of datasources
            var updated = await _client.Api.Get<Mind>($"/projects/{_project}/minds/{Name}");
            Datasources = updated.Datasources;
        }

        /// <summary>
        /// Deletes a datasource from the mind.
        /// </summary>
        /// <param name="datasource">The datasource to remove, which can be a <see cref="Datasource"/> object or a string name.</param>
        /// <exception cref="ArgumentException">Thrown if the provided datasource type is invalid.</exception>
        public async Task DelDatasource(object datasource)
        {
            string dsName;

            // Determine the type of the datasource
            if (datasource is Datasource ds)
            {
                dsName = ds.Name; // If it's a Datasource object, get the name
            }
            else if (datasource is string name)
            {
                dsName = name; // If it's a string, use it directly
            }
            else
            {
                throw new ArgumentException($"Unknown type of datasource: {datasource}");
            }

            // Delete the datasource
            await _client.Api.Delete($"/projects/{_project}/minds/{Name}/datasources/{dsName}");

            // Fetch the updated mind to refresh the list of datasources
            var updatedMind = await _client.Api.Get<Mind>($"/projects/{_project}/minds/{Name}");
            Datasources = updatedMind.Datasources;
        }
    }
}
