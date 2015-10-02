using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPIToolkit.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")] // Enable Cors for all origins
    public class BaseController : ApiController
    {
        public const string Version = "v1.0"; // Our API Version

        /// <summary>
        /// Get the current user identity. This works work any type of authentication (bearertoken, sso, whatever).
        /// </summary>
        /// <returns></returns>
        protected IIdentity GetCurrentUser()
        {
            IPrincipal principal = RequestContext.Principal;
            return principal?.Identity;
        }
    }
}