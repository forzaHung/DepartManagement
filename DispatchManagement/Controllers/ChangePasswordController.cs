using Framework.Configuration;
using Framework.EF;
using Dispatch;
using DispatchManagement.Models;
using Prototype.Code;
using System.Web.Mvc;

namespace DispatchManagement.Controllers
{
    public class ChangePasswordController : BaseController
    {
        private IEmployee _iplEmployee;
       
        public ChangePasswordController()
        {
            _iplEmployee = SingletonIpl.GetInstance<IplEmployee>();
           
        }
        // GET: Backend/ChangePassword
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult Change(ChangePasswordModel model)
        {
           
            var sess = SessionSystem.GetUser();            
            if (ModelState.IsValid)
            {
                
                var userInfo = SingletonIpl.GetInstance<IplUser>().Login(sess.UserName);
                string password = Framework.Security.Crypt.SHA.sha256_hash(model.OldPassword);
                if (userInfo.Password.Equals(password))
                {
                    var newPass = Framework.Security.Crypt.SHA.sha256_hash(model.NewPassword);
                    var flag = SingletonIpl.GetInstance<IplUser>().ChangePass(sess.UserId, newPass);
                    if (flag)
                    {
                        Logs.logs("Thay đổi pass", "thay đổi pass thành công", "/ChangPassword/Change", sess.UserId);
                        SessionSystem.ClearSession();
                        return Redirect("/Login");
                    }
                }
                else
                {
                    
                    ViewBag.MsgOldPass = ConstantMsg.OldPassInvalid;
                }
            }
            return View("Index");
        }
    }
}