﻿using System.Web.Http;
using Microsoft.Owin;
using Owin;
using Swashbuckle.Application;
using WebAPIToolkit.Common;
using WebAPIToolkit.Controllers;

[assembly: OwinStartup(typeof(WebAPIToolkit.Startup))]

namespace WebAPIToolkit
{
    /// <summary>
    /// This is the entry point of WebAPI
    /// </summary>
    public partial class Startup
    {

        /// <summary>
        /// By settings this to true, MOQ are injected instead of real storage items (DB or whatever)
        /// </summary>
        public static bool UnitTests = false;

        /// <summary>
        /// OWIN Configuration
        /// </summary>
        /// <param name="app"></param>
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
