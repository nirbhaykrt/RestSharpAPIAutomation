using RestSharp;
using System.Collections.Generic;

namespace RestSharpAutomation.Helper
{
    public interface IRequest
    {
        RestRequest CreateRequest(
            string resource,
            Method method,
            Dictionary<string, string>? headers = null,
            Dictionary<string, string>? queryParams = null,
            object? body = null,
            string bodyType = "json");
    }
}
