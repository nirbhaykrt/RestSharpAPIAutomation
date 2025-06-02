using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpAutomation.Helper;
using RestSharpAutomation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomationFramework.Tests.GoRestAPIs
{
    [TestClass]
    [DoNotParallelize]
    public class GoRestAPIsTest
    {
        private IClient clientManager = null!;
        private IRequest requestBuilder = null!;
        private IResponse responseHandler = null!;
        private string token = "Bearer 5e9ab8791279a40d8cb3adac20a1a057a5f43f06780dd889e103d94800ef11d2";
        public static int userId;

        [TestInitialize]
        public void Setup()
        {
            clientManager = new RestClientManager("https://gorest.co.in");
            requestBuilder = new RestRequestBuilder();
            responseHandler = new RestResponseHandler(clientManager);
        }
        [TestMethod]
        public void Test_01_GetListUser()
        {
            var headers = new Dictionary<string, string> {{"Authorization",token }};
            var request = requestBuilder.CreateRequest("/public/v2/users", Method.Get, headers: headers);

            var response = responseHandler.Execute(request);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            int responseCode = (int)response.StatusCode;
            Console.WriteLine($"{responseCode} " + response.StatusCode);
            List<GoRestUser> GoRestUser = responseHandler.Deserialize<List<GoRestUser>>(response);

            Assert.IsNotNull(GoRestUser);

            foreach (var user in GoRestUser)
            {
                Console.WriteLine($"{user.Id}: {user.Name} , {user.Email}, {user.Gender}, {user.Status}");
            }
        }
        [TestMethod]
        public void Test_02_CreateUser()
        {
            var headers = new Dictionary<string, string> { { "Authorization", token } };
            Object body = new
            {
                name = "Sten cena",
                email = "sten.cena@mail.com",
                gender = "Male",
                status = "Active"
            };
            var request = requestBuilder.CreateRequest("/public/v2/users", Method.Post, body: body, headers: headers);

            var response = responseHandler.Execute(request);
            Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode);
            int responseCode = (int)response.StatusCode;
            Console.WriteLine($"{responseCode} " + response.StatusCode);
            var json = responseHandler.ParseToJObject(response);
            userId = json["id"]?.Value<int>() ?? 0;
            TestDataStore.SaveUserId(userId);
            Console.WriteLine(json.ToString());
  
        }
        [TestMethod]
        public void Test_03_UpdateUser()
        {
            int userId = TestDataStore.LoadUserId();
            var headers = new Dictionary<string, string> { { "Authorization", token } };
            Object body = new
            {
                name = "Allasani Peddana",
                email = "allasani.peddana@mail.com",
                gender  ="male",
                status = "Active"
            };    
            var request = requestBuilder.CreateRequest($"/public/v2/users/{userId}", Method.Patch, body: body, headers: headers);
            Console.WriteLine($"User ID: {userId}");
            var response = responseHandler.Execute(request);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            int responseCode = (int)response.StatusCode;
            Console.WriteLine($"{responseCode} " + response.StatusCode);
            var json = responseHandler.ParseToJObject(response);
            Console.WriteLine(json.ToString());
        }
        [TestMethod]
        public void Test_04_DeleteUser()
        {
            int userId = TestDataStore.LoadUserId();
            var headers = new Dictionary<string, string> { { "Authorization", token } };
            Console.WriteLine($"User ID: {userId}");
            var request = requestBuilder.CreateRequest($"/public/v2/users/{userId}", Method.Delete, headers: headers);

            var response = responseHandler.Execute(request);
            Assert.AreEqual(System.Net.HttpStatusCode.NoContent, response.StatusCode);
            int responseCode = (int)response.StatusCode;
        }
    }
}