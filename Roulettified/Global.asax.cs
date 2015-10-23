using System;
using System.Collections;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Roulettified
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            // At session start set the spins counter to zero (will be maintained by spin controller method)
            Session["spins"] = 0;

            // Also track history to provide statistics
            ArrayList history = new ArrayList();
            Session["history"] = history;
        }
    }
}
