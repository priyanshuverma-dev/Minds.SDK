
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minds.SDK {

    public class Datasources {
        private readonly Client _client;

        public Datasources(Client client) {
            _client = client;
        }

        public async Task<Datasource> Create(DatabaseConfig dsConfig, bool replace = false) {
            string name = dsConfig.Name;

            if (replace) {
                try {
                    await Get(name);
                    await Drop(name);
                } catch (ObjectNotFoundException) {
                    // Ignore if the datasource does not exist
                }
            }

            await _client.Api.Post("/datasources", dsConfig);
            return await Get(name);
        }

        public async Task<List<Datasource>> List() {
            var data = await _client.Api.Get<List<Datasource>>("/datasources");
            var dsList = new List<Datasource>();

            foreach (var item in data) {
                // TODO: Skip not SQL skills
                if (string.IsNullOrEmpty(item.Engine)) {
                    continue;
                }
                dsList.Add(item);
            }

            return dsList;
        }

        public async Task<Datasource> Get(string name) {
            var data = await _client.Api.Get<Datasource>($"/datasources/{name}");

            // TODO: Skip not SQL skills
            if (string.IsNullOrEmpty(data.Engine)) {
                throw new ObjectNotSupportedException($"Wrong type of datasource: {name}");
            }

            return data;
        }

        public async Task Drop(string name) {
            await _client.Api.Delete($"/datasources/{name}");
        }
    }

}