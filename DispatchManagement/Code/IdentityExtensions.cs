using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Prototype.Code
{
    public static class IdentityExtensions
    {
        public static int GetCustomerId(this IIdentity identity)
        {
            if (identity == null)
                return 0;
            try
            {
                return Convert.ToInt32((identity as ClaimsIdentity).FirstOrNull(CustomClaimTypes.CustomerId));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        internal static string FirstOrNull(this ClaimsIdentity identity, string claimType)
        {
            var val = identity.FindFirst(claimType);

            return val == null ? null : val.Value;
        }
    }

    public static class CustomClaimTypes
    {
        public const string CustomerId = "CustomerId";
    }
}