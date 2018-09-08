using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace DispatchManagement.Code
{
    public class Culture
    {
        public static int CurrentCulture
        {
            get
            {
                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "en-US":
                        return 1;
                    default:
                        return 0;
                }
            }
            set
            {
                var cookie = new HttpCookie("lang");
                CultureInfo ci;
                switch (value)
                {
                    case 1:
                        ci = new CultureInfo("en-US");
                        cookie.Value = "en-US";
                        break;
                    default:
                        ci = new CultureInfo("vi-VN");
                        cookie.Value = "vi-VN";
                        break;
                }
                cookie.Expires = DateTime.Now.AddDays(30);
                HttpContext.Current.Response.Cookies.Add(cookie);
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
        }
    }
}