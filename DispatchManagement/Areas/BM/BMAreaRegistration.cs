using System.Web.Mvc;

namespace DispatchManagement.Areas.BM
{
    public class BMAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "BM";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "BM_default",
                "BM/{controller}/{action}/{id}",
                new {controller = "BookRooms", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "DispatchManagement.Areas.BM.Controllers" }
            );
        }
    }
}