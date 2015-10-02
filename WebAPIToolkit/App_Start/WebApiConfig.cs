using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using WebAPIToolkit.Common;

namespace WebAPIToolkit
{
    public static class WebApiConfig
    {
        /// <summary>
        /// Register WebApiConfig
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Attributes routing
            config.MapHttpAttributeRoutes();

            // Dependency Resolver
            config.DependencyResolver = new UnityResolver();


            // Convention-based routing is not used as it is usually a good idea to use Attribute routing
            // See http://www.asp.net/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2#why 
            // Uncomment following lines to enable Convention-based routing + see RouteConfig

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
