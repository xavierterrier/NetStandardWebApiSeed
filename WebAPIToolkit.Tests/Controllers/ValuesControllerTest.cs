using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPIToolkit;
using WebAPIToolkit.Controllers;

namespace WebAPIToolkit.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest : BaseControllerTest
    {
        [TestMethod]
        public async Task Get()
        {
            using (var server = TestServer.Create<Startup>())
            {
                using (var client = new HttpClient(server.Handler))
                {
                    var token = await GetValidBearerToken(client);
                    client.DefaultRequestHeaders.Add("Authorization", $"bearer {token}");
                    
                    var response = await client.GetAsync($"http://testserver/{BaseController.Version}/values");
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
                }
            }
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            string result = controller.Get(5);

            // Assert
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            controller.Post("value");

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            controller.Put(5, "value");

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            controller.Delete(5);

            // Assert
        }
    }
}
