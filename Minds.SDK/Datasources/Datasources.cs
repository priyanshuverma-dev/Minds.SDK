using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minds.SDK
{
    /// <summary>
    /// Provides methods to interact with datasources, including creation, listing, retrieval, and deletion.
    /// </summary>
    public class Datasources
    {
        private readonly Client _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="Datasources"/> class.
        /// </summary>
        /// <param name="client">The client instance for making API requests.</param>
        public Datasources(Client client)
        {
            _client = client;
        }

        /// <summary>
        /// Creates a new datasource based on the provided <see cref="DatabaseConfig"/>.
        /// If the datasource already exists and <paramref name="replace"/> is true, it will be deleted and replaced.
        /// </summary>
        /// <param name="dsConfig">The database configuration for the datasource to be created.</param>
        /// <param name="replace">If true, replaces the existing datasource with the same name, if it exists.</param>
        /// <returns>A <see cref="Datasource"/> object representing the created datasource.</returns>
        public async Task<Datasource> Create(DatabaseConfig dsConfig, bool replace = false)
        {
            string name = dsConfig.Name;

            if (replace)
            {
                try
                {
                    await Get(name);
                    await Drop(name);
                }
                catch (ObjectNotFoundException)
                {
                    // Ignore if the datasource does not exist
                }
            }

            await _client.Api.Post("/datasources", dsConfig);
            return await Get(name);
        }

        /// <summary>
        /// Retrieves a list of all available datasources.
        /// Filters out datasources that do not support SQL.
        /// </summary>
        /// <returns>A list of <see cref="Datasource"/> objects.</returns>
        public async Task<List<Datasource>> List()
        {
            var data = await _client.Api.Get<List<Datasource>>("/datasources");
            var dsList = new List<Datasource>();

            foreach (var item in data)
            {
                // Skip datasources without SQL support
                if (string.IsNullOrEmpty(item.Engine))
                {
                    continue;
                }
                dsList.Add(item);
            }

            return dsList;
        }

        /// <summary>
        /// Retrieves a datasource by name.
        /// Throws an exception if the datasource does not support SQL.
        /// </summary>
        /// <param name="name">The name of the datasource to retrieve.</param>
        /// <returns>A <see cref="Datasource"/> object representing the retrieved datasource.</returns>
        /// <exception cref="ObjectNotSupportedException">Thrown if the datasource does not support SQL.</exception>
        public async Task<Datasource> Get(string name)
        {
            var data = await _client.Api.Get<Datasource>($"/datasources/{name}");

            // Skip datasources without SQL support
            if (string.IsNullOrEmpty(data.Engine))
            {
                throw new ObjectNotSupportedException($"Wrong type of datasource: {name}");
            }

            return data;
        }

        /// <summary>
        /// Deletes a datasource by name.
        /// </summary>
        /// <param name="name">The name of the datasource to delete.</param>
        public async Task Drop(string name)
        {
            await _client.Api.Delete($"/datasources/{name}");
        }
    }
}
