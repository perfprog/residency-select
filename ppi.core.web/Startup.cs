using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PPI.Core.Web.Startup))]
namespace PPI.Core.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
