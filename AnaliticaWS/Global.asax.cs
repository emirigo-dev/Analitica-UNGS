using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace AnaliticaWS
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Scheduler.StartAhorro();
            Scheduler.Start();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
