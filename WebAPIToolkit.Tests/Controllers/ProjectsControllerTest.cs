using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPIToolkit.Controllers;
using WebAPIToolkit.Dtos;
using WebAPIToolkit.Model;

namespace WebAPIToolkit.Tests.Controllers
{
    [TestClass]
    public class ProjectsControllerTest : BaseControllerTest
    {
        private static string BaseUrl = $"http://testserver/{BaseController.Version}/projects";

        [TestMethod]
        public async Task Create()
        {
            using (var client = await GetAuthentifiedClient())
            {
                var newDto = await CreateProject(client);
                Assert.AreEqual(HttpStatusCode.OK, newDto.Item1.StatusCode);
                Assert.IsNotNull(newDto.Item2);
                Assert.IsTrue(newDto.Item2.Id > 0);
            }
        }

        [TestMethod]
        public async Task Update()
        {
            using (var client = await GetAuthentifiedClient())
            {
                var create = await CreateProject(client);
                Assert.AreEqual(HttpStatusCode.OK, create.Item1.StatusCode);
                create.Item2.Name = "New name";
                var response = await client.PutAsJsonAsync(BaseUrl, create.Item2);
                var dto = await response.Content.ReadAsAsync<ProjectDto>();
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(create.Item2.Name, dto.Name);
            }
        }

        [TestMethod]
        public async Task GetAll()
        {
            using (var client = await GetAuthentifiedClient())
            {
                var create = await CreateProject(client);
                Assert.AreEqual(HttpStatusCode.OK, create.Item1.StatusCode);

                var response = await client.GetAsync(BaseUrl);
                var dtos = await response.Content.ReadAsAsync<IEnumerable<ProjectDto>>();
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.IsTrue(dtos.Any());
            }
        }

        [TestMethod]
        public async Task GetCodes()
        {
            using (var client = await GetAuthentifiedClient())
            {
                var create = await CreateProject(client);
                Assert.AreEqual(HttpStatusCode.OK, create.Item1.StatusCode);

                var response = await client.GetAsync($"{BaseUrl}/codes");
                var dtos = await response.Content.ReadAsAsync<IEnumerable<string>>();
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.IsTrue(dtos.Any());
            }
        }

        [TestMethod]
        public async Task GetById()
        {
            using (var client = await GetAuthentifiedClient())
            {
                var create = await CreateProject(client);
                Assert.AreEqual(HttpStatusCode.OK, create.Item1.StatusCode);
                //var uri = GetURI(BaseUrl, new Dictionary<string, string> {{"id", create.Item2.Id.ToString()}});

                var response = await client.GetAsync($"{BaseUrl}/{create.Item2.Id.ToString()}");
                var dto = await response.Content.ReadAsAsync<ProjectDto>();
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(create.Item2.Id, dto.Id);
            }
        }

        [TestMethod]
        public async Task Delete()
        {
            using (var client = await GetAuthentifiedClient())
            {
                var create = await CreateProject(client);
                Assert.AreEqual(HttpStatusCode.OK, create.Item1.StatusCode);
                //var uri = GetURI(BaseUrl, new Dictionary<string, string> {{"id", create.Item2.Id.ToString()}});

                var response = await client.DeleteAsync($"{BaseUrl}/{create.Item2.Id.ToString()}");
                Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            }
        }


        public static async Task<Tuple<HttpResponseMessage, ProjectDto>> CreateProject(HttpClient client)
        {
            var dto = new ProjectDto()
            {
                Client = "The client",
                Code = "The code",
                Contact = "The contact",
                EndDate = DateTime.Now.AddDays(30),
                Manager = "The manager",
                Name = "The name",
                PersonInCharge = "The person in charge",
                Phase = "The phase",
                Practice = Enums.Practice.mobile,
                StartDate = DateTime.Now.AddDays(25),
                Status = Enums.Status.ON_TIME
            };

            var response = await client.PostAsJsonAsync(BaseUrl, dto);
            dto = await response.Content.ReadAsAsync<ProjectDto>();

            return new Tuple<HttpResponseMessage, ProjectDto>(response, dto);
        }

    }
}
