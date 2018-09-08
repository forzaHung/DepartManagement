using Dispatch;
using Prototype.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DispatchManagement
{
    public class Authenticate
    {
        public static bool checkRole(string ModuleName, string AccessLevel)
        {
            var sess = SessionSystem.GetUser();
            if (sess == null)
            {
                return false;
            }

            if (HttpContext.Current.Session["Role_" + sess.UserName] == null)
                return false;
            var collection = (List<RoleEntity>)HttpContext.Current.Session["Role_" + sess.UserName];
            var role = collection.Where(x => x.Name == ModuleName).FirstOrDefault();
            var isAuthor = false;
            switch (AccessLevel)
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
    }
}