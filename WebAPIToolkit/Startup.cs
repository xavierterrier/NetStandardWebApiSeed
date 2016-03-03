using System;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;
using WebAPIToolkit.Common;
using WebAPIToolkit.Common.Authentication;
using WebAPIToolkit.Common.ErrorHandlers;
using WebAPIToolkit.Controllers;
using WebAPIToolkit.DtoMappers;
using WebAPIToolkit.Model.Database;

[assembly: OwinStartup(typeof(WebAPIToolkit.Startup))]

namespace WebAPIToolkit
{
    /// <summary>
    /// This is the entry point of WebAPI (because we use OWIN), all is starting here !
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// By settings this to true, MOQ are injected instead of real storage items (DB or whatever)
        /// </summary>
        public static bool UnitTests = false;

        /// <summary>
        /// OAuth options used for authentication
        /// </summary>
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        /// <summary>
        /// ClientId used for authentication
        /// </summary>
        public static string PublicClientId { get; private set; }

        /// <summary>
        /// OWIN Configuration
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            // Initialize IoC (see Dependency injection pattern https://en.wikipedia.org/wiki/Dependency_injection)
            if (UnitTests)
            {
                UnityResolver.UnitTestInitialize();
            }
            else
            {
                UnityResolver.Initialize();
            }

            var configuration = new HttpConfiguration {DependencyResolver = new UnityResolver()};



            // Attributes routing
            configuration.MapHttpAttributeRoutes();

            // Convention-based routing is not used as it is usually a good idea to use Attribute routing
            // See http://www.asp.net/Web-api/overview/Web-api-routing-and-actions/attribute-routing-in-Web-api-2#why 
            // Uncomment following lines to enable Convention-based routing
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{Id}",
            //    defaults: new { Id = RouteParameter.Optional }
            //);


            ConfigureAuth(app, configuration);
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

            // Init Automapper mappings
            AutoMapperMapping.Configure();

            // Attach Exception Handlers
            configuration.Filters.Add(new GlobalExceptionFilter());
            configuration.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            app.UseWebApi(configuration);
        }

        /// <summary>
        /// Here we configure DB repositories authentication
        /// If you need to use SSO authentication, basically you have to comment this and enable Windows Authentication in IIS 
        /// For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        private void ConfigureAuth(IAppBuilder app, HttpConfiguration configuration)
        {
            // Configure Web API to use only bearer token authentication.
            // Change OAuthDefaults.AuthenticationType if you want to change "Bearer" string in Authentication HEADER
            configuration.SuppressDefaultHostAuthentication();
            configuration.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Configure the user manager and role manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                // Default token endpointPath is disable as it is made for an asp.net mvc application (it requires application/x-www-form-urlencoded)
                // You can enable it with TokenEndpointPath = new PathString($"/{BaseController.Version}/account/token"), // Here is the url to obtain a BearerToken
                // See AccountController.GetBearerToken instead
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString($"/{BaseController.Version}/account/ExternalLogin"), //  Here is the url to obtain an external token
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // TODO: In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true,
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }

        /// <summary>
        /// Change the default way Newtonsoft is seralizing to JSON
        /// </summary>
        /// <param name="configuration"></param>
        private void ConfigureJsonSerializer(HttpConfiguration configuration)
        {
            var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;

            // Indent child objects
            settings.Formatting = Formatting.Indented;

            // serialize to Json using CamelCase
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Remove all converters
            settings.Converters.Clear();

            // Serialize C# enum as string            
            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        }

        /// <summary>
        /// Swagger needs XML generated documentation
        /// </summary>
        /// <returns></returns>
        protected static string GetXmlCommentsPath()
        {
            return $@"{System.AppDomain.CurrentDomain.BaseDirectory}\bin\WebAPIToolkit.XML";
        }


    }
}
