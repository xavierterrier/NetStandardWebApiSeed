using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Owin;

namespace WebAPIToolkit.Tests
{
    public class TestConf : Startup
    {

        public new void Configuration(IAppBuilder app)
        {
            

            //HttpConfiguration config = new HttpConfiguration();
            //config.Services.Replace(typeof(IAssembliesResolver), new TestWebApiResolver());
            //config.MapHttpAttributeRoutes();
            //app.UseWebApi(config);
        }
    }
}
