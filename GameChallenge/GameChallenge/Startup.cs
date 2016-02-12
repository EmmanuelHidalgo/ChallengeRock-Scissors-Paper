using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GameChallenge.Startup))]
namespace GameChallenge
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
