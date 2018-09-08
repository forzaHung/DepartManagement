using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Framework.Helper;

namespace DispatchManagement
{
    public static class ViewExtenstions
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string src, string altText = "", string height = "50")
        {
            if (src != null && !src.StartsWith("http", true, CultureInfo.InvariantCulture))
            {
                src = Constants.ProductImagePath + src;
            }
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", src);
            builder.MergeAttribute("alt", altText);
            builder.MergeAttribute("height", height);
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
        public static MvcHtmlString DateFormat(this HtmlHelper helper, DateTime? date = null ,string format = Constants.DateFormat, bool showTime = false)
        {
            if (date == null)
                return MvcHtmlString.Empty;

            return MvcHtmlString.Create(date.Value.ToString(format + (showTime ? Constants.TimeFormat : string.Empty)));
        }
    }
}