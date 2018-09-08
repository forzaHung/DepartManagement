using Dispatch;
using DispatchManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DispatchManagement
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        // Custom property
        //http://stackoverflow.com/questions/13264496/asp-net-mvc-4-custom-authorize-attribute-with-permission-codes-without-roles
        public string ModuleName { get; set; }
        public string AccessLevel { get; set; }
        public AuthorizeUserAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var sess = SessionSystem.GetUser();
            if (sess == null)
            {
                return false;
            }

            if (HttpContext.Current.Session["Role_" + sess.UserName] == null)
                return false;
            var collection = (List<RoleEntity>)HttpContext.Current.Session["Role_" + sess.UserName];
            var role = collection.Where(x => x.Name == this.ModuleName).FirstOrDefault();
            if (role == null)
                return false;
            var isAuthor = false;
            switch (this.AccessLevel)
            {
                case Constants.Add:
                    {
                        isAuthor = role.Add;
                        break;
                    }

                case Constants.Edit:
                    {
                        isAuthor = role.Edit;
                        break;
                    }
                case Constants.View:
                    {
                        isAuthor = role.View;
                        break;
                    }
                case Constants.Delete:
                    {
                        isAuthor = role.Delete;
                        break;
                    }

                case Constants.Import:
                    {
                        isAuthor = role.Import;
                        break;
                    }
                case Constants.Export:
                    {
                        isAuthor = role.Export;
                        break;
                    }
                case Constants.Upload:
                    {
                        isAuthor = role.Upload;
                        break;
                    }
                case Constants.Save:
                    {
                        isAuthor = role.Publish;
                        break;
                    }
                case Constants.Report:
                    {
                        isAuthor = role.Report;
                        break;
                    }
                case Constants.Print:
                    {
                        isAuthor = role.Print;
                        break;
                    }

            }
            //}
            return isAuthor;

        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "NotFound",
                                action = "Denied"
                            })
                        );
        }
    }
}