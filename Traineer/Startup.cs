using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Traineer.Startup))]
namespace Traineer
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
