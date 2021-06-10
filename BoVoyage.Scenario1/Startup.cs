using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BoVoyage.Scenario1.Startup))]
namespace BoVoyage.Scenario1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
