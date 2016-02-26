using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPIToolkit.Common.ErrorHandlers;
using WebAPIToolkit.Dtos;
using WebAPIToolkit.Model;
using WebAPIToolkit.Model.Database;

namespace WebAPIToolkit.Controllers
{
    [Authorize] // User must be authentified except if specified
    [RoutePrefix(Version + "/projects")] // The Base route
    public class ProjectsController : BaseController
    {
        private readonly IDbProvider _dbProvider;

        public ProjectsController(IDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        /// <summary>
        /// Get all projects
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<ProjectDto>> GetAll()
        {
            List<Project> projects;
            using (var db = _dbProvider.GetModelContext())
            {
                projects = await db.Projects.ToListAsync();
            }

            return projects.Select(AutoMapper.Mapper.Map<ProjectDto>);
        }

        /// <summary>
        /// Get project by id
        /// </summary>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        public async Task<ProjectDto> Get(int id)
        {
            Project project;
            using (var db = _dbProvider.GetModelContext())
            {
                project = await db.Projects.SingleOrDefaultAsync(p => p.Id == id);
            }

            return AutoMapper.Mapper.Map<ProjectDto>(project);
        }

        /// <summary>
        /// Get all projects codes
        /// </summary>
        /// <returns></returns>
        [Route("codes")]
        [HttpGet]
        public async Task<IEnumerable<string>> GetCodes()
        {
            IEnumerable<string> names;
            using (var db = _dbProvider.GetModelContext())
            {
                names = await db.Projects.Select(p => p.Name).Distinct().ToListAsync();
            }

            return names;
        }

        /// <summary>
        /// Create new project
        /// </summary>
        /// <param name="dto"></param>
        [Route("")]
        [HttpPost]
        public async Task<ProjectDto> Create([FromBody]ProjectDto dto)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException(ModelState);
            }
            var project = AutoMapper.Mapper.Map<Project>(dto);

            using (var db = _dbProvider.GetModelContext())
            {
                db.Projects.Add(project);
                await db.SaveChangesAsync();
            }

            return AutoMapper.Mapper.Map<ProjectDto>(project);
        }

        /// <summary>
        /// Update the project
        /// </summary>
        /// <param name="dto"></param>
        [Route("")]
        [HttpPut]
        public async Task<ProjectDto> Update([FromBody]ProjectDto dto)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException(ModelState);
            }
            var project = AutoMapper.Mapper.Map<Project>(dto);

            using (var db = _dbProvider.GetModelContext())
            {
                db.Projects.Attach(project);
                db.Entry(project).State = EntityState.Modified;

                await db.SaveChangesAsync();
            }

            return AutoMapper.Mapper.Map<ProjectDto>(project);
        }

        /// <summary>
        /// Delete the project
        /// </summary>
        [Route("{id}")]
        [HttpDelete]
        public async Task Delete(int id)
        {
            using (var db = _dbProvider.GetModelContext())
            {
                var project = new Project()
                {
                    Id = id
                };
                db.Projects.Attach(project);
                db.Entry(project).State = EntityState.Deleted;

                await db.SaveChangesAsync();
            }
        }
    }
}