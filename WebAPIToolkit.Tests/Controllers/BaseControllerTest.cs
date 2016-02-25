using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
                Email = "test2@test.com",
                Password = "testtest2",
                ConfirmPassword = "testtest2"
            };

            var basicLoginDto = new BasicLoginDto()
            {
                Username = registerDto.Email,
                Password = registerDto.Password
            };

            await client.PostAsJsonAsync($"http://testserver/{BaseController.Version}/account/register", registerDto);

            var response2 = await client.PostAsJsonAsync($"http://testserver/{BaseController.Version}/account/token", basicLoginDto);
            var authToken = await response2.Content.ReadAsAsync<AuthTokenDto>();

            Assert.AreEqual(HttpStatusCode.OK, response2.StatusCode);
            Assert.IsNotNull(authToken);
            Assert.IsNotNull(authToken.AccessToken);

            return authToken.AccessToken;
        }



    }
}
