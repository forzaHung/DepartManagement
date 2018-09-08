using DispatchManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prototype
{
    [Serializable]
    public class SessionCustomer
    {
        public static string sessionName = "customer";
        public static MemberSession GetUser()
        {

            if (HttpContext.Current != null &&
                HttpContext.Current.Session != null &&
                HttpContext.Current.Session.Count > 0 &&
                HttpContext.Current.Session[SessionCustomer.sessionName] != null)
            {
                return HttpContext.Current.Session[SessionCustomer.sessionName] as MemberSession;
            }
            return null;
        }

        /// <summary>
        /// Sets the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public static bool SetUser(MemberSession user)
        {
            if(HttpContext.Current != null &&
                HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Remove(SessionCustomer.sessionName);
                user.SessionId = HttpContext.Current.Session.SessionID;
            }
            HttpContext.Current.Session.Add(SessionCustomer.sessionName, user);
            return true;
        }

        /// <summary>
        /// Clears the session.
        /// </summary>
        public static void ClearSession()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }
    }
}