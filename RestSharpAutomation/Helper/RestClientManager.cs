using RestSharp;

namespace RestSharpAutomation.Helper
{
    public class RestClientManager : IClient
    {
        public RestClient Client { get; private set; }

        public RestClientManager(string baseUrl)
        {
            Client = new RestClient(baseUrl);
        }
    }
}
