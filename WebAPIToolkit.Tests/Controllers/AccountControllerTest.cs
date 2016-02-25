using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPIToolkit.Controllers;
using WebAPIToolkit.Dtos;

namespace WebAPIToolkit.Tests.Controllers
{

    [TestClass]
    public class AccountControllerTest : BaseControllerTest
    {


        [TestMethod]
        public async Task Register()
        {
            var dto = new RegisterDto()
            {
                Email = "test@test.com",
                Password = "testtest",
                ConfirmPassword = "testtest"
            };

            using (var server = TestServer.Create<Startup>())
            {

                using (var client = new HttpClient(server.Handler))
                {
                    var response = await client.PostAsJsonAsync($"http://testserver/{BaseController.Version}/account/register", dto);
                    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                }

            }
        }

        [TestMethod]
        public async Task GetBearerToken()
        {
            using (var server = TestServer.Create<Startup>())
            {
                using (var client = new HttpClient(server.Handler))
                {
                    var token = await GetValidBearerToken(client);
                    Assert.IsNotNull(token);
                }
            }           
        }

    }
}
