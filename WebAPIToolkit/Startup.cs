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
                IoC.UnitTestInitialize();
            }
            else
            {
                IoC.Initialize();
            }

            var configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);
            // FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            // RouteConfig.RegisterRoutes(RouteTable.Routes);

            //GlobalConfiguration.Configure(WebApiConfig.Register);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);

            ConfigureAuth(app);
            ConfigureJsonSerializer();

            configuration
            .EnableSwagger("docs/{apiVersion}/swagger", c => c.SingleApiVersion(BaseController.Version, "WebAPI Toolkit"))
            .EnableSwaggerUi();

            app.UseWebApi(configuration);
        }

        
    }
}
