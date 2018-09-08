using Framework.EF;
using Dispatch;
using DispatchManagement.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DispatchManagement.Controllers
{
    public class RolesController : Controller
    {
        // GET: Backend/Roles
        [AuthorizeUser(ModuleName = "Roles", AccessLevel = Constants.View)]
        public ActionResult Index(int id)
        {
            ViewBag.UserId = id;
            var ipluser = SingletonIpl.GetInstance<IplUser>();
            var empInfo = ipluser.ViewDetail(id);
            var iplRole = SingletonIpl.GetInstance<IplRole>();
            ViewData["ListRole"] = iplRole.ListAll(id);
            return View(empInfo);
        }
        [HttpPost]
        [AuthorizeUser(ModuleName = "Roles", AccessLevel = Constants.Save)]
        public JsonResult EditRoles(int userId, int moduleId, string action, bool access)
        {
            var iplRole = SingletonIpl.GetInstance<IplRole>();
            var ret = iplRole.Update(userId, moduleId, action, access);
            return Json(new { status = ret });
        }
    }
}