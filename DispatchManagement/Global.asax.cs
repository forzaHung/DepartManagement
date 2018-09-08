using Framework.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DispatchManagement
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
        }

        //protected void Application_Error(Object sender, EventArgs e)
        //{
        //    Exception lastException = Server.GetLastError();
        //    Logging.PutError("App_Error", lastException);
        //    if (lastException == null) return;
        //    Response.Clear();
        //    // clear error on server
        //    Server.ClearError();// clear error on server
        //    Response.Redirect("~/NotFound/Index");
        //    //Response.RedirectToRoute("PageNotFound");
        //}
    }
}
