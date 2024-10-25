using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minds.SDK {
    public class Mind {
        private readonly Client _client;
        private readonly string _project = "mindsdb";

        public string Name { get; private set; }
        public string ModelName { get; private set; }
        public string Provider { get; private set; }
        public Dictionary<string, object> Parameters { get; private set; }
        public List<string> Datasources { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public Mind(Client client, string name, string modelName = null, string provider = null,
                    Dictionary<string, object> parameters = null, List<string> datasources = null,
                    DateTime? createdAt = null, DateTime? updatedAt = null) {
            _client = client;
            Name = name;
            ModelName = modelName;
            Provider = provider;
            Parameters = parameters ?? new Dictionary<string, object>();
            Datasources = datasources ?? new List<string>();
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public override string ToString() {
            return $"Mind(Name={Name}, ModelName={ModelName}, Provider={Provider}, CreatedAt={CreatedAt}, UpdatedAt={UpdatedAt}, Parameters={Parameters}, Datasources={Datasources})";
        }


        public async Task Update(string name = null, string modelName = null, string provider = null,
                                 string promptTemplate = null, List<string> datasources = null,
                                 Dictionary<string, object> parameters = null) {
            var data = new Dictionary<string, object>();

            if (datasources != null) {
                data["datasources"] = datasources.Select(ds => _client.Minds.CheckDatasource(ds)).ToList();
            }

            if (name != null) {
                data["name"] = name;
            }
            if (modelName != null) {
                data["model_name"] = modelName;
            }
            if (provider != null) {
                data["provider"] = provider;
            }
            if (parameters == null) {
                parameters = new Dictionary<string, object>();
            }

            if (promptTemplate != null) {
                parameters["prompt_template"] = promptTemplate;
            }

            data["parameters"] = parameters;

            await _client.Api.Patch($"/projects/{_project}/minds/{Name}", data);
            if (name != null && name != Name) {
                Name = name;
            }
        }

        public async Task AddDatasource(string datasource) {
            var dsName = _client.Minds.CheckDatasource(datasource);
            await _client.Api.Post($"/projects/{_project}/minds/{Name}/datasources", new { name = dsName });

            var updated = await _client.Api.Get<Mind>($"/projects/{_project}/minds/{Name}");
            Datasources = updated.Datasources;
        }

        public async Task DelDatasource(object datasource) {
            string dsName;

            // Check the type of the datasource
            if (datasource is Datasource ds) {
                dsName = ds.Name; // If it's a Datasource object, get the name
            } else if (datasource is string name) {
                dsName = name; // If it's already a string, use it directly
            } else {
                throw new ArgumentException($"Unknown type of datasource: {datasource}");
            }

            // Perform the deletion
            await _client.Api.Delete($"/projects/{_project}/minds/{Name}/datasources/{dsName}");

            // Fetch the updated Mind object to refresh the Datasources list
            var updatedMind = await _client.Api.Get<Mind>($"/projects/{_project}/minds/{Name}");
            Datasources = updatedMind.Datasources; // Update the local Datasources list
        }



    }
}
