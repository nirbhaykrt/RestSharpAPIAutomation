using RestSharpAutomation.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomationFramework.Tests.PATCH
{
    [TestClass]
    public class PATCHEndpoints
    {
        private IClient clientManager= null!;
        private IRequest requestBuilder = null!;
        private IResponse responseHandler = null!;

        [TestInitialize]
        public void Setup()
        {
            clientManager = new RestClientManager("https://reqres.in/");
            requestBuilder = new RestRequestBuilder();
            responseHandler = new RestResponseHandler(clientManager);
        }
        [TestMethod]
        public void PatchUpdateUser()
        {
            var headers = new Dictionary<string, string> { { "x-api-key", "reqres-free-v1" } };
            object body = new { name = "morpheus", job = "India resident" };
            var request = requestBuilder.CreateRequest("/api/users/2", Method.Patch, headers: headers, body: body);

            var response = responseHandler.Execute(request);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            int responseCode = (int)response.StatusCode;
            Console.WriteLine($"{responseCode} " + response.StatusCode);
            var json = responseHandler.ParseToJObject(response);
            Console.WriteLine($"Name: {json["name"]}\nJob: {json["job"]} \nUpdatedAt:{json["updatedAt"]}");
        }
    }
}
