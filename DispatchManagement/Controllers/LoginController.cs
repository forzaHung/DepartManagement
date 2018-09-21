using Framework.EF;
using Dispatch;
using DispatchManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DispatchManagement.Controllers
{
    public class LoginController : Controller
    {
        
        // GET: Backend/Login
        public LoginController(){
           
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                string password = Framework.Security.Crypt.SHA.sha256_hash(model.Password);

                var obj = SingletonIpl.GetInstance<IplUser>();
                var userInfo = obj.Login(model.UserName);
                if (userInfo != null && !string.IsNullOrEmpty(userInfo.UserName))
                {
                    string token = generalToken();
                    if (userInfo.Password.Equals(password))
                    {
                        Logs.logs("Đăng nhập vào trang quản trị", "Đăng nhập", "/Login", userInfo.Id);
                        var sessUser = new UserSession
                        {
                            UserId = userInfo.Id,
                            EmployeeId = userInfo.EmployeeId,
                            FirstName = userInfo.FirstName,
                            LastName = userInfo.LastName,
                            ImagePath = userInfo.PicturePath,
                            Password = userInfo.Password,
                            UserName = userInfo.UserName,
                            Token = token
                        };
                        SessionSystem.SetUser(sessUser);
                        var roleIPL = SingletonIpl.GetInstance<IplRole>();
                        var list = roleIPL.ListAll(userInfo.Id);
                        Session["Role_" + sessUser.UserName] = list;
                        // obj.Update(userInfo.ID, token);
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return RedirectToAction(returnUrl);
                        }

                        return RedirectToAction("Index", "Dashboard", new { id = userInfo.Id });
                    }
                    else
                    {
                        ViewBag.MsgLogin = "Nhập sai tên đăng nhập hoặc mật khẩu.";
                    }
                }
                else
                {
                    ViewBag.MsgLogin = "Nhập sai tên đăng nhập hoặc mật khẩu.";
                }
                return View("Index");
            }
            return View("Index");
        }
        public ActionResult logout()
        {
            var sessUser = SessionSystem.GetUser();
            Logs.logs("Thoát ra ngoài,kết thúc phiên làm việc", "Logout", "/Login/Logout", sessUser.UserId);
            SessionSystem.ClearSession();
            return View("Index");

        }
        private string generalToken()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());
            return token;
        }

        //protected void logs(string action,string description,string link,int userid)
        //{
        //    TransactionLogsEntity translog = new TransactionLogsEntity();
        //    translog.Action = action;
        //    translog.Description = description;
        //    translog.Link = link;
        //    translog.LogDate = DateTime.Now;
        //    translog.UserId = userid;
        //    _transactionlogs.Insert(translog);
        //}
    }
}