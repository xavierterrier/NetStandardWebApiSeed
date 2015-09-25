using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPIToolkit.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]    
    public class BaseController : ApiController
    {
        public const string Version = "v1.0";

        /// <summary>
        /// Get the current user identity
        /// </summary>
        /// <returns></returns>
        protected IIdentity GetCurrentUser()
        {
            IPrincipal principal = RequestContext.Principal;
            return principal?.Identity;
        }
    }
}