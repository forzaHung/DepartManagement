using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DispatchManagement.Startup))]
namespace DispatchManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
