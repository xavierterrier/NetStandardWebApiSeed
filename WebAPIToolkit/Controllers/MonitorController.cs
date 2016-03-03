using System.Web.Http;

namespace WebAPIToolkit.Controllers
{

    [RoutePrefix(Version + "/isalive")] // The Base route
    public class MonitorController : BaseController
    {

        [HttpGet]
        public bool Get()
        {
            return true;
        }

    }
}