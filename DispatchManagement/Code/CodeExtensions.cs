using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static DispatchManagement.EnumSystem;

namespace Prototype
{
    public static class CodeExtensions
    {
        public static string ReturnSettingType(int enumSetting)
        {
            string retVal = string.Empty;
            switch (enumSetting)
            {
                case (int)EnumSettings.HotProduct:
                    retVal = EnumSettings.HotProduct.ToString();
                    break;
            }
            return retVal;
        }
    }
}