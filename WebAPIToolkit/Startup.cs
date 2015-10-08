using System.Web.Http;
using Microsoft.Owin;
using Owin;
using Swashbuckle.Application;
using WebAPIToolkit.Common;
using WebAPIToolkit.Controllers;

[assembly: OwinStartup(typeof(WebAPIToolkit.Startup))]

namespace WebAPIToolkit
{
    public partial class Startup
    {

        public static bool UnitTests = false;

        public void Configuration(IAppBuilder app)
        {
            // Initialize IoC
            if (UnitTests)
            {
                UnityResolver.UnitTestInitialize();
            }
            else
            {
                UnityResolver.Initialize();
            }

            var configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);
            // FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            // RouteConfig.RegisterRoutes(RouteTable.Routes);

            //GlobalConfiguration.Configure(WebApiConfig.Register);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);

            ConfigureAuth(app);
            ConfigureJsonSerializer(configuration);

            // Enable swagger docs
            // See https://github.com/domaindrivendev/Swashbuckle for details
            configuration
            .EnableSwagger("swagger/{apiVersion}/docs", (c) =>
            {
                c.SingleApiVersion(BaseController.Version, "WebAPI Toolkit");
                c.IncludeXmlComments(GetXmlCommentsPath());
            })
            .EnableSwaggerUi();
            
            

            app.UseWebApi(configuration);
        }

        protected static string GetXmlCommentsPath()
        {
            return $@"{System.AppDomain.CurrentDomain.BaseDirectory}\bin\WebAPIToolkit.XML";
        }


    }
}
