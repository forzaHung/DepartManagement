using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Framework.EF;

namespace DispatchManagement.Controllers
{
    public class DashboardController : BaseController
    {

        public DashboardController()
        {

        }
        // GET: Backend/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}