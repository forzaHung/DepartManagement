using Framework.EF;
using Dispatch;
using DispatchManagement.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DispatchManagement.Models;

namespace DispatchManagement.Controllers
{
    public class UserController : BaseController
    {
        // GET: Backend/User
        [AuthorizeUser(ModuleName = "User", AccessLevel = Constants.View)]
        public ActionResult Index(int id)
        {
            ViewBag.EmpId = id;
            var iplemp = SingletonIpl.GetInstance<IplEmployee>();
            var empInfo = iplemp.ViewDetail(id);
            ViewData["EmpInfo"] = empInfo;
            var userEntity = new UserEntity();
            return View(userEntity);
        }
        [AuthorizeUser(ModuleName = "User", AccessLevel = Constants.Save)]
        public ActionResult Save(UserEntity entity)
        {
            if (ModelState.IsValid)
            {
                var sess = SessionSystem.GetUser();
                Logs.logs("Sửa User", "Truy cập vào trang User", "/User/", sess.UserId);
                var ipluser = SingletonIpl.GetInstance<IplUser>();
                entity.Password = Framework.Security.Crypt.SHA.sha256_hash(entity.Password);
                var userId = ipluser.Insert(entity);
                if (userId > 0)
                    return RedirectToAction("Index", "Roles", new { id = userId });
                ViewBag.Msg = ConstantMsg.ErrorProgress;
            }
            var iplemp = SingletonIpl.GetInstance<IplEmployee>();
            var empInfo = iplemp.ViewDetail(entity.EmployeeId);
            ViewData["EmpInfo"] = empInfo;
            return View("Index", entity);
        }
        [AuthorizeUser(ModuleName = "User", AccessLevel = Constants.Edit)]
        [HttpPost]
        public JsonResult ResetPass(int userId)
        {
            var sess = SessionSystem.GetUser();
            Logs.logs("Reset lại pass", "Truy cập vào trang User", "/User/", sess.UserId);
            var ipluser = SingletonIpl.GetInstance<IplUser>();
            var userExists = ipluser.ViewDetail(userId);
            if (userExists != null && userExists.Id > 0)
            {
                string newPass = Framework.Security.Crypt.SHA.sha256_hash("12345678");
                var retVal = ipluser.ChangePass(userId, newPass);
                return Json(new
                {
                    status = retVal
                });
            }
            return Json(new { status = false });
        }
    }
}