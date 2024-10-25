using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minds.SDK {
    public class Minds {
        private readonly Client _client;
        private readonly string _project = "mindsdb";

        public Minds(Client client) {
            _client = client;
        }

        public async Task<List<Mind>> List() {
            var data = await _client.Api.Get<List<Mind>>($"/projects/{_project}/minds");
            return data.Select(item => new Mind(_client, item.Name, item.ModelName, item.Provider,
                                                 item.Parameters, item.Datasources,
                                                 item.CreatedAt, item.UpdatedAt)).ToList();
        }

        public async Task<Mind> Get(string name) {
            var item = await _client.Api.Get<Mind>($"/projects/{_project}/minds/{name}");
            return item;
        }

        public string CheckDatasource(object ds) {
            if (ds is Datasource datasource) {
                return datasource.Name;
            } else if (ds is DatabaseConfig config) {
                // Check and create datasource logic
                return config.Name;
            } else if (ds is string name) {
                return name;
            } else {
                throw new ArgumentException($"Unknown type of datasource: {ds}");
            }
        }

        public async Task<Mind> Create(string name, string modelName = null, string provider = null,
                                        string promptTemplate = null, List<string> datasources = null,
                                        Dictionary<string, object> parameters = null, bool replace = false) {
            if (replace) {
                try {
                    await Get(name);
                    await Drop(name);
                } catch (ObjectNotFoundException) {
                    // Mind not found, proceed to create
                }
            }

            var dsNames = datasources?.Select(ds => CheckDatasource(ds)).ToList() ?? new List<string>();
            if (parameters == null) {
                parameters = new Dictionary<string, object>();
            } 
            if (promptTemplate != null) {
                parameters["prompt_template"] = promptTemplate;
            }

            var data = new {
                name,
                model_name = modelName,
                provider,
                parameters,
                datasources = dsNames
            };

            await _client.Api.Post($"/projects/{_project}/minds", data);
            return await Get(name);
        }

        public async Task Drop(string name) {
            await _client.Api.Delete($"/projects/{_project}/minds/{name}");
        }
    }
}
