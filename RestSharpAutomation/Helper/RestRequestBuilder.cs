using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace RestSharpAutomation.Helper
{
    public class RestRequestBuilder : IRequest
    {
        public RestRequest CreateRequest(
            string resource,
            Method method,
            Dictionary<string, string>? headers = null,
            Dictionary<string, string>? queryParams = null,
            object? body = null,
            string bodyType = "json") // json or xml
        {
            var request = new RestRequest(resource, method);

            // Add query parameters
            if (queryParams != null)
            {
                foreach (var param in queryParams)
                    request.AddQueryParameter(param.Key, param.Value);
            }

            // Add headers
            if (headers != null)
            {
                foreach (var header in headers)
                    request.AddHeader(header.Key, header.Value);
            }

            // Add request body if provided
            if (body != null)
            {
                if (bodyType.ToLower() == "json")
                {
                    string jsonBody = JsonConvert.SerializeObject(body);
                    request.AddStringBody(jsonBody, DataFormat.Json);
                }
                else if (bodyType.ToLower() == "xml")
                {
                    var xmlBody = SerializeToXml(body);
                    request.AddStringBody(xmlBody, DataFormat.Xml);
                }
            }

            return request;
        }

        // Helper to serialize an object to XML string
        private string SerializeToXml(object obj)
        {
            using (var stringWriter = new System.IO.StringWriter())
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
                serializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }
    }
}
