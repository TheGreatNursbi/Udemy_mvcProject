using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Proj.Startup))]
namespace Proj
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
