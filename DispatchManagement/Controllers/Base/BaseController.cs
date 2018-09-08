using Framework.EF;
using Dispatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DispatchManagement.Helper;
using System.Collections.Specialized;

namespace DispatchManagement.Controllers
{
    public class BaseController : Controller
    {
        public static HttpContextBase HttpContextBase { get; set; }
        public BaseController()
        {
            var sess = SessionSystem.GetUser();
            if (sess == null)
            {
                return;
            }
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)

        {
            var sess = SessionSystem.GetUser();
            var returnUrl = filterContext.RequestContext.HttpContext.Request.RawUrl;
            int indexParam = returnUrl.IndexOf('?');
            int indexP = 0;
            int temp1 = returnUrl.Count(f => f == '/');
            if (returnUrl.Count(f => f == '/') > 2)
            {
                indexP = returnUrl.LastIndexOf('/');
            }
            string temp = returnUrl;
            if (indexParam > -1)
            {
                temp = returnUrl.Substring(0, indexParam);
            }
            if (indexP > 0)
            {
                temp = returnUrl.Substring(0, indexP);
            }
            // LongNDG Check url request and get current module
            var urls = temp.Split('/');
            var url = "";
            if (urls.Length > 2)
                url = urls[2];
            else
                url = "Home";
            //var test = urls[2];
            if (sess == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index",returnUrl = returnUrl.ToString() }));

            }
            else
            {
                //Check token online. Logging a only PC
                //var userDetail = obj.ViewDetail(sess.AccID,sess.Token);
                //if (userDetail == null)
                //{
                //    this.Session.Clear();
                //    filterContext.Result = new RedirectToRouteResult(new
                //           RouteValueDictionary(new { controller = "Login", action = "Index", Area = "manager", returnUrl = returnUrl.ToString() }));
                //}
                //else {
                if (Session["Role_" + sess.UserName] != null)
                {
                    if (temp == "/ChangePassword/ChangePwd")
                        return;
                    var checkRole = (List<RoleEntity>)Session["Role_" + sess.UserName];
                    int index = -1;
                    if (returnUrl != "/")
                    {
                        for (int i = 0; i < checkRole.Count; i++)
                        {
                            if (checkRole[i].Name.ToLower().IndexOf(url.ToLower()) != -1)
                            {
                                index = i;
                            }
                        }
                        if (index >= 0)
                        {
                            if (!checkRole[index].View)
                            {
                                filterContext.Result = new RedirectToRouteResult(new
                                RouteValueDictionary(new { controller = "NotFound", action = "Denied",returnUrl = returnUrl.ToString() }));
                            }
                        }
                        //else
                        //{
                        //    filterContext.Result = new RedirectToRouteResult(new
                        //        RouteValueDictionary(new { controller = "NotFound", action = "Denied", Area = "Backend", returnUrl = returnUrl.ToString() }));
                        //    //filterContext.Result = new RedirectResult("/manager/Denied/Index");
                        //}
                    }
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new
                                RouteValueDictionary(new { controller = "NotFound", action = "Denied", returnUrl = returnUrl.ToString() }));
                }
                //}
            }
            base.OnActionExecuting(filterContext);
        }
        protected PagingRequest GetPagingMessage(NameValueCollection queries, bool returnOffset = true)
        {
            var limit = Common.StringToInt(queries["limit"]);
            var offset = Common.StringToInt(queries["offset"]);
            var sort = queries["sort"];
            var order = queries["order"];
            return new PagingRequest
            {
                PageIndex = limit <= 0 ? 1 : ((returnOffset == true) ? offset / limit + 1 : offset),
                PageSize = limit,
                Sort = sort + " " + order,
            };
        }
    }
}