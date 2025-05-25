using RestSharpAutomation.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomationFramework.Tests.DELETE
{
    [TestClass]
    public class DELETEEndpoints
    {
        private IClient clientManager=null!;
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
        public void DeleteUser()
        {    
            var headers = new Dictionary<string, string> { { "x-api-key", "reqres-free-v1" } };
            var request = requestBuilder.CreateRequest("/api/users/2", Method.Delete, headers: headers);
            var response = responseHandler.Execute(request);
            Assert.AreEqual(System.Net.HttpStatusCode.NoContent, response.StatusCode);
            int responseCode = (int)response.StatusCode;
            Console.WriteLine($"{responseCode} " + response.StatusCode);
        }
    }
}
