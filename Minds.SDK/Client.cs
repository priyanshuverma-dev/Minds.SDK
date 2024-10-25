namespace Minds.SDK {
    public class Client {
        public RestAPI Api { get; }
        public Datasources Datasources { get; }
        public Minds Minds { get; }

        public Client(string apiKey, string baseUrl = null) {
            Api = new RestAPI(apiKey, baseUrl);
            Datasources = new Datasources(this);
            Minds = new Minds(this);
        }
    }
}
