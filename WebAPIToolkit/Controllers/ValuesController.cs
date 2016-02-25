using System.Collections.Generic;
using System.Web.Http;

namespace WebAPIToolkit.Controllers
{
    /// <summary>
    /// Controller Example    
    /// </summary>
    [Authorize] // User must be authentified except if specified
    [RoutePrefix(Version + "/values")] // The Base route
    public class ValuesController : BaseController
    {
        // GET v1.0/values
        [Route("")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET v1.0/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST v1.0/values
        public void Post([FromBody]string value)
        {
        }

        // PUT v1.0/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE v1.0/values/5
        public void Delete(int id)
        {
        }
    }
}
