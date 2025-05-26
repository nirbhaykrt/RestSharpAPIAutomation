using RestSharpAutomation.Helper;
using RestSharpAutomation.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace APIAutomationFramework.Tests.GET
{
    [TestClass]
    public class GetEndpoints
    {
        private IClient clientManager = null!;
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
        public void GetListUser()
        {
            var queryParams = new Dictionary<string, string> { { "page", "2" } };
            var headers = new Dictionary<string, string> { { "x-api-key", "reqres-free-v1" } };
            var request = requestBuilder.CreateRequest("/api/users", Method.Get, queryParams: queryParams, headers: headers);

            var response = responseHandler.Execute(request);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response);

            int responseCode = (int)response.StatusCode;
            Console.WriteLine($"{responseCode} " + response.StatusCode);

            var usersResponse = responseHandler.Deserialize<UserResponse>(response);

            Assert.IsNotNull(usersResponse);
            Assert.IsNotNull(usersResponse.Data);

            foreach (var user in usersResponse.Data)
            {
                Console.WriteLine($"{user.Id}: {user.First_name} {user.Last_name} - {user.Email}");
            }
        }

        [TestMethod]
        public void GetSingleUser()
        {
            var headers = new Dictionary<string, string> { { "x-api-key", "reqres-free-v1" } };
            var request = requestBuilder.CreateRequest("/api/users/2", Method.Get, headers: headers);

            var response = responseHandler.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response);

            int responseCode = (int)response.StatusCode;
            Console.WriteLine($"{responseCode} " + response.StatusCode);

            var usersResponse = responseHandler.Deserialize<SingleUserResponse>(response);
            Assert.IsNotNull(usersResponse);
            Assert.IsNotNull(usersResponse.Data);

            Console.WriteLine($"{usersResponse.Data.Id}: {usersResponse.Data.First_name} {usersResponse.Data.Last_name} - {usersResponse.Data.Email}");
        }

        [TestMethod]
        public void GetSingleUserNotfound()
        {
            var headers = new Dictionary<string, string> { { "x-api-key", "reqres-free-v1" } };
            var request = requestBuilder.CreateRequest("/api/users/23", Method.Get, headers: headers);

            var response = responseHandler.Execute(request);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsNotNull(response);

            int responseCode = (int)response.StatusCode;
            Console.WriteLine($"{responseCode} " + response.StatusCode);
        }

        [TestMethod]
        public void GetListOfResources()
        {
            var headers = new Dictionary<string, string> { { "x-api-key", "reqres-free-v1" } };
            var request = requestBuilder.CreateRequest("/api/unknown", Method.Get, headers: headers);

            var response = responseHandler.Execute(request);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            int responseCode = (int)response.StatusCode;
            Console.WriteLine($"{responseCode} " + response.StatusCode);

            var resourceResponse = responseHandler.Deserialize<ResourceListResponse>(response);
            Assert.IsNotNull(resourceResponse);
            Assert.IsNotNull(resourceResponse.Data);

            foreach (var resource in resourceResponse.Data)
            {
                Console.WriteLine($"{resource.Id}: {resource.Name}, {resource.Year}, {resource.Color}, {resource.Pantone_value}");
            }
        }

        [TestMethod]
        public void GetSingleResources()
        {
            var headers = new Dictionary<string, string> { { "x-api-key", "reqres-free-v1" } };
            var request = requestBuilder.CreateRequest("/api/unknown/2", Method.Get, headers: headers);

            var response = responseHandler.Execute(request);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            int responseCode = (int)response.StatusCode;
            Console.WriteLine($"{responseCode} " + response.StatusCode);

            var usersResponse = responseHandler.Deserialize<SingleResourceResponse>(response);
            Assert.IsNotNull(usersResponse);
            Assert.IsNotNull(usersResponse.Data);

            Console.WriteLine($"{usersResponse.Data.Id}: {usersResponse.Data.Name}, {usersResponse.Data.Year}, {usersResponse.Data.Color}, {usersResponse.Data.Pantone_value}");
        }

        [TestMethod]
        public void GetSingleResourceNotFound()
        {
            var headers = new Dictionary<string, string> { { "x-api-key", "reqres-free-v1" } };
            var request = requestBuilder.CreateRequest("/api/unknown/23", Method.Get, headers: headers);

            var response = responseHandler.Execute(request);
            Assert.IsNotNull(response);

            int responseCode = (int)response.StatusCode;
            Console.WriteLine($"{responseCode} " + response.StatusCode);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetDelayedResponse()
        {
            var queryParams = new Dictionary<string, string> { { "delay", "3" } };
            var headers = new Dictionary<string, string> { { "x-api-key", "reqres-free-v1" } };
            var request = requestBuilder.CreateRequest("/api/users", Method.Get, queryParams: queryParams, headers: headers);

            var response = responseHandler.Execute(request);
            Assert.IsNotNull(response);

            int responseCode = (int)response.StatusCode;
            Console.WriteLine($"{responseCode} " + response.StatusCode);

            var usersResponse = responseHandler.Deserialize<UserResponse>(response);
            Assert.IsNotNull(usersResponse);
            Assert.IsNotNull(usersResponse.Data);

            foreach (var user in usersResponse.Data)
            {
                Console.WriteLine($"{user.Id}: {user.First_name} {user.Last_name} - {user.Email}");
            }
        }
    }
}
