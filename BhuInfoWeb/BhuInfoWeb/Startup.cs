using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BhuInfoWeb.Startup))]
namespace BhuInfoWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
