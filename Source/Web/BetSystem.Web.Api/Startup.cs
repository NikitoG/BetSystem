using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BetSystem.Web.Api.Startup))]

namespace BetSystem.Web.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}