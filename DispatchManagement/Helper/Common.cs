using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DispatchManagement.Helper
{
    public class Common
    {
        public static string FormatCurrence (decimal amount)
        {
            return string.Format("{0:N0}", amount) + " đ";
        }
        public static int StringToInt(string num, int defVal = 0)
        {
            try
            {
                return int.Parse(num);
            }
            catch
            {
                return defVal;
            }
        }
    }
}