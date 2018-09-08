using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DispatchManagement
{
    [Serializable]
    public class SessionSystem
    {
        public static string sessionName = "user";
        public static UserSession GetUser()
        {

            if (HttpContext.Current != null &&
                HttpContext.Current.Session != null &&
                HttpContext.Current.Session.Count > 0 &&
                HttpContext.Current.Session[SessionSystem.sessionName] != null)
            {
                return HttpContext.Current.Session[SessionSystem.sessionName] as UserSession;
            }
            return null;
        }

        /// <summary>
        /// Sets the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public static bool SetUser(UserSession user)
        {
            HttpContext.Current.Session.Remove(SessionSystem.sessionName);
            user.SessionId = HttpContext.Current.Session.SessionID;
            HttpContext.Current.Session.Add(SessionSystem.sessionName, user);
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