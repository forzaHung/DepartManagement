using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DispatchManagement.Models
{
    public static class Utils
    {
        public static string GetShortReview(string str, int NumberOfWords)
        {
            string[] Words = str.Split(' ');
            string sReturn = string.Empty;
            string sTemp = string.Empty;
            if (Words.Length <= NumberOfWords)
            {
                sReturn = str;
            }
            else
            {
                for (int i = 0; i < NumberOfWords; i++)
                {
                    sTemp += Words.GetValue(i).ToString() + " ";
                }
                sReturn = sTemp.ToString() + " ...";
            }
            return sReturn.ToString();
        }
        public static string convertToUnSign2(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            var str = sb.ToString().Normalize(NormalizationForm.FormD);
            str = System.Text.RegularExpressions.Regex.Replace(str, @"[^A-Za-z0-9\s-]", ""); // Remove all non valid chars          
            str = System.Text.RegularExpressions.Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space  
            str = System.Text.RegularExpressions.Regex.Replace(str, @" ", "-"); // //Replace spaces by dashes
            string kq = str.ToLower();
            return kq;
        }

        private const long OneKb = 1024;
        private const long OneMb = OneKb * 1024;
        private const long OneGb = OneMb * 1024;
        private const long OneTb = OneGb * 1024;

        //public static string ToPrettySize(this int value, int decimalPlaces = 0)
        //{
        //    return ((long)value).ToPrettySize(decimalPlaces);
        //}

        public static string ToPrettySize(this long value, int decimalPlaces = 0)
        {
            var asTb = Math.Round((double)value / OneTb, decimalPlaces);
            var asGb = Math.Round((double)value / OneGb, decimalPlaces);
            var asMb = Math.Round((double)value / OneMb, decimalPlaces);
            var asKb = Math.Round((double)value / OneKb, decimalPlaces);
            string chosenValue = asTb > 1 ? string.Format("{0}Tb", asTb)
                : asGb > 1 ? string.Format("{0}Gb", asGb)
                : asMb > 1 ? string.Format("{0}Mb", asMb)
                : asKb > 1 ? string.Format("{0}Kb", asKb)
                : string.Format("{0}B", Math.Round((double)value, decimalPlaces));
            return chosenValue;
        }
    }
}