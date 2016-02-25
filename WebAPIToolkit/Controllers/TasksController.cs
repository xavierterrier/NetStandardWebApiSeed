using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebAPIToolkit.Common.ErrorHandlers;
using WebAPIToolkit.Dtos;
using WebAPIToolkit.Model;
using WebAPIToolkit.Model.Database;

namespace WebAPIToolkit.Controllers
{

    [Authorize] // User must be authentified except if specified
    [RoutePrefix(Version + "/tasks")] // The Base route
    public class TasksController : BaseController
    {
        private readonly IDbProvider _dbProvider;

        public TasksController(IDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }


        /// <summary>
        /// Find task for a given range
        /// </summary>
        /// <returns></returns>
        [Route("~/ " + Version + "/projects/tasks")] //{id}
        [HttpGet]
        public async Task<IEnumerable<TaskDto>> Get(DateTime from, DateTime to)
        {
            List<ProjectTask> tasks;
            using (var db = _dbProvider.GetModelContext())
            {
                tasks = await db.Tasks.Where(t => t.DueDate.HasValue && t.DueDate >= from && t.DueDate <= to).ToListAsync();
            }

            return tasks.Select(AutoMapper.Mapper.Map<TaskDto>);
        }

        /// <summary>
        /// Get all taks for a project
        /// </summary>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        public async Task<TaskDto> Get(int id)
        {
            ProjectTask task;
            using (var db = _dbProvider.GetModelContext())
            {
                task = await db.Tasks.SingleOrDefaultAsync(t => t.Id == id);
            }

            return AutoMapper.Mapper.Map<TaskDto>(task);
        }

        /// <summary>
        /// Get all taks for a project
        /// </summary>
        /// <returns></returns>
        [Route("~/" + Version + "/projects/{id}/tasks")]
        [HttpGet]
        public async Task<IEnumerable<TaskDto>> GetProjectTasks(int id)
        {
            List<ProjectTask> tasks;
            using (var db = _dbProvider.GetModelContext())
            {
                tasks = await db.Tasks.Where(t => t.Project.Id == id).ToListAsync();
            }

            return tasks.Select(AutoMapper.Mapper.Map<TaskDto>);
        }

        /// <summary>
        /// Create new task
        /// </summary>
        /// <returns></returns>
        [Route("~/" + Version + "/projects/{id}/tasks")]
        [HttpPost]
        public async Task Create(TaskDto dto)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException(ModelState);
            }
            var task = AutoMapper.Mapper.Map<ProjectTask>(dto);

            using (var db = _dbProvider.GetModelContext())
            {
                db.Tasks.Add(task);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Update the task
        /// </summary>
        /// <returns></returns>
        [Route("~/" + Version + "/projects/{id}/tasks")]
        [HttpPut]
        public async Task Update(TaskDto dto)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException(ModelState);
            }
            var task = AutoMapper.Mapper.Map<ProjectTask>(dto);

            using (var db = _dbProvider.GetModelContext())
            {
                db.Tasks.Attach(task);
                db.Entry(task).State = EntityState.Modified;

                await db.SaveChangesAsync();
            }
        }

    }
}