using RestSharp;

namespace RestSharpAutomation.Helper
{
    public interface IClient
    {
        RestClient Client { get; }
    }
}
