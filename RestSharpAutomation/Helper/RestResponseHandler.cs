using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace RestSharpAutomation.Helper
{
    public class RestResponseHandler : IResponse
    {
        private readonly IClient _client;

        public RestResponseHandler(IClient client)
        {
            _client = client;
        }

        public RestResponse Execute(RestRequest request)
        {
            return _client.Client.Execute(request);
        }

        public T Deserialize<T>(RestResponse response)
        {
            if (string.IsNullOrEmpty(response.Content))
                throw new InvalidOperationException("Response content is null or empty.");

            var result = JsonConvert.DeserializeObject<T>(response.Content);

            if (result == null)
                throw new JsonSerializationException("Deserialization returned null.");

            return result;
        }

        public JObject ParseToJObject(RestResponse response)
        {
            if (string.IsNullOrWhiteSpace(response.Content))
                throw new InvalidOperationException("Response content is empty or null.");

            return JObject.Parse(response.Content);
        }

    }
}
