using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPIToolkit.Controllers;
using WebAPIToolkit.Dtos;

namespace WebAPIToolkit.Tests.Controllers
{
    [TestClass]
    public class BaseControllerTest
    {

        [TestInitialize]
        public void Init()
        {
            Startup.UnitTests = true;
        }

        /// <summary>
        /// This helper initialize a test user and return a valid Beared token
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public async Task<string> GetValidBearerToken(HttpClient client)
        {
            var registerDto = new RegisterDto()
            {
                Username = "test2",
                Password = "testtest2",
                ConfirmPassword = "testtest2"
            };

            var basicLoginDto = new BasicLoginDto()
            {
                Username = registerDto.Username,
                Password = registerDto.Password
            };

            await client.PostAsJsonAsync($"http://testserver/{BaseController.Version}/account/register", registerDto);

            var response2 = await client.PostAsJsonAsync($"http://testserver/{BaseController.Version}/account/login", basicLoginDto);
            var authToken = await response2.Content.ReadAsAsync<AuthTokenDto>();

            Assert.AreEqual(HttpStatusCode.OK, response2.StatusCode);
            Assert.IsNotNull(authToken);
            Assert.IsNotNull(authToken.AccessToken);

            return authToken.AccessToken;
        }

        protected async Task<HttpClient> GetAuthentifiedClient()
        {
            var client = new AuthentifiedHttpClient();
            var token = await GetValidBearerToken(client);
            client.DefaultRequestHeaders.Add("Authorization", $"bearer {token}");

            return client;
        }

        protected Uri GetURI(string url, IDictionary<string, string> parameters = null)
        {
            var builder = new UriBuilder(url) {Port = -1};
            var query = HttpUtility.ParseQueryString(builder.Query);

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    query[param.Key] = param.Value;
                }
            }
          
            builder.Query = query.ToString();

            return builder.Uri;
        }
    }
}
