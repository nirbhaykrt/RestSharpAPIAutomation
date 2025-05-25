using Newtonsoft.Json.Linq;
using RestSharp;

namespace RestSharpAutomation.Helper
{
    public interface IResponse
    {
        RestResponse Execute(RestRequest request);
        T Deserialize<T>(RestResponse response);
        JObject ParseToJObject(RestResponse response);
    }
}
