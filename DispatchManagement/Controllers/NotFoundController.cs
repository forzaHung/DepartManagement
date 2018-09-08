using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DispatchManagement.Controllers
{
    public class NotFoundController : Controller
    {
        // GET: Backend/NotFound
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Denied()
        {
            return View();
        }
    }
}