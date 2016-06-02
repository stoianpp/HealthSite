using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AptSystems.Web.Startup))]
namespace AptSystems.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
