using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPIToolkit.Controllers;
using WebAPIToolkit.Dtos;
using WebAPIToolkit.Model;

namespace WebAPIToolkit.Tests.Controllers
{
    [TestClass]
    public class TasksControllerTest : BaseControllerTest
    {
        private readonly string ProjectUrl = $"http://testserver/{BaseController.Version}/projects";
        private readonly string TasksUrl = $"http://testserver/{BaseController.Version}/tasks";


        [TestMethod]
        public async Task Create()
        {
            using (var client = await GetAuthentifiedClient())
            {
                var newDto = await CreateTask(client);
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
                var create = await CreateTask(client);
                Assert.AreEqual(HttpStatusCode.OK, create.Item1.StatusCode);
                create.Item2.Name = "New name";
                var response = await client.PutAsJsonAsync($"{ProjectUrl}/{create.Item2.ProjectId}/tasks", create.Item2);
                var dto = await response.Content.ReadAsAsync<TaskDto>();
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(create.Item2.Name, dto.Name);
            }
        }

        [TestMethod]
        public async Task GetById()
        {
            using (var client = await GetAuthentifiedClient())
            {
                var create = await CreateTask(client);
                Assert.AreEqual(HttpStatusCode.OK, create.Item1.StatusCode);
                //var uri = GetURI(BaseUrl, new Dictionary<string, string> {{"id", create.Item2.Id.ToString()}});

                var response = await client.GetAsync($"{TasksUrl}/{create.Item2.Id}");
                var dto = await response.Content.ReadAsAsync<TaskDto>();
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(create.Item2.Id, dto.Id);
            }
        }

        [TestMethod]
        public async Task GetProjectAllTasks()
        {
            using (var client = await GetAuthentifiedClient())
            {
                var create = await CreateTask(client);
                Assert.AreEqual(HttpStatusCode.OK, create.Item1.StatusCode);
                //var uri = GetURI(BaseUrl, new Dictionary<string, string> {{"id", create.Item2.Id.ToString()}});

                var response = await client.GetAsync($"{ProjectUrl}/{create.Item2.ProjectId}/tasks");
                var dtos = await response.Content.ReadAsAsync<IEnumerable<TaskDto>>();
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.IsTrue(dtos.Any());
            }
        }

        
        [TestMethod]
        public async Task GetProjectTasksGivenRange()
        {
            using (var client = await GetAuthentifiedClient())
            {
                var create = await CreateTask(client);
                Assert.AreEqual(HttpStatusCode.OK, create.Item1.StatusCode);
                var uri = GetURI($"{ProjectUrl}/{create.Item2.ProjectId}/tasks",
                    new Dictionary<string, string>
                    {
                        {"from", create.Item2.DueDate.Value.ToShortDateString()},
                        {"to", create.Item2.DueDate.Value.ToShortDateString()}
                    });

                var response = await client.GetAsync(uri);
                var dtos = await response.Content.ReadAsAsync<IEnumerable<TaskDto>>();
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.IsTrue(dtos.Any());
            }
        }

        private async Task<Tuple<HttpResponseMessage, TaskDto>> CreateTask(HttpClient client)
        {
            var create = await ProjectsControllerTest.CreateProject(client);

            var dto = new TaskDto()
            {
                Name = "The name",
                Contact = "The contact",
                Manager = "The manager",
                DueDate = DateTime.Now.AddDays(25),
                State = Enums.State.TO_DO,
                ProjectId = create.Item2.Id
            };

            var response = await client.PostAsJsonAsync($"{ProjectUrl}/{create.Item2.Id}/tasks", dto);
            dto = await response.Content.ReadAsAsync<TaskDto>();

            return new Tuple<HttpResponseMessage, TaskDto>(response, dto);
        }
    }
}
