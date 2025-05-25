using RestSharpAutomation.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomationFramework.Tests.POST
{
    [TestClass]
    public class POSTEndpoints
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
        public void PostCreateUser()
        {
            var headers = new Dictionary<string, string> { { "x-api-key", "reqres-free-v1" } };
            object body = new { name = "morpheus", job = "leader" };
            var request = requestBuilder.CreateRequest("/api/users", Method.Post, headers: headers, body: body);

            var response = responseHandler.Execute(request);
            Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode);
            int responseCode = (int)response.StatusCode;
            Console.WriteLine($"{responseCode} " + response.StatusCode);
            var json = responseHandler.ParseToJObject(response);
            Console.WriteLine($"ID: {json["id"]} \nCreatedAT:{json["createdAt"]}");
        }
        [TestMethod]
        public void PostResgisterSuccessful()
        {
            var headers = new Dictionary<string, string> { { "x-api-key", "reqres-free-v1" } };
            object body = new { email = "eve.holt@reqres.in", password = "pistol" };
            var request = requestBuilder.CreateRequest("/api/register", Method.Post, headers: headers, body: body);
            var response = responseHandler.Execute(request);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            int responseCode = (int)response.StatusCode;
            Console.WriteLine($"{responseCode} " + response.StatusCode);
            var json = responseHandler.ParseToJObject(response);
            Console.WriteLine($"ID: {json["id"]}\nToken: {json["token"]}");
        }
        [TestMethod]
        public void PostResgisterUnSuccessful()
        {
            var headers = new Dictionary<string, string> { { "x-api-key", "reqres-free-v1" } };
            object body = new { email = "sydney@fife" };
            var request = requestBuilder.CreateRequest("/api/register", Method.Post, headers: headers, body: body);
            var response = responseHandler.Execute(request);
            Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            int responseCode = (int)response.StatusCode;
            Console.WriteLine($"{responseCode} " + response.StatusCode);
            var json = responseHandler.ParseToJObject(response);
            Console.WriteLine($"Error: {json["error"]}");
        }
        [TestMethod]
        public void PostLoginSuccessful()
        {
            var headers = new Dictionary<string, string> { { "x-api-key", "reqres-free-v1" } };
            object body = new { email = "eve.holt@reqres.in", password = "cityslicka" };
            var request = requestBuilder.CreateRequest("/api/login", Method.Post, headers: headers, body: body);
            var response = responseHandler.Execute(request);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            int responseCode = (int)response.StatusCode;
            Console.WriteLine($"{responseCode} " + response.StatusCode);
            var json = responseHandler.ParseToJObject(response);
            Console.WriteLine($"Token: {json["token"]}");
        }
        [TestMethod]
        public void PostLoginUnSuccessful()
        {
            var headers = new Dictionary<string, string> { { "x-api-key", "reqres-free-v1" } };
            object body = new { email = "peter@klaven" };
            var request = requestBuilder.CreateRequest("/api/login", Method.Post, headers: headers, body: body);
            var response = responseHandler.Execute(request);
            Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            int responseCode = (int)response.StatusCode;
            Console.WriteLine($"{responseCode} " + response.StatusCode);
            var json = responseHandler.ParseToJObject(response);
            Console.WriteLine($"Error: {json["error"]}");
        }
    }
}