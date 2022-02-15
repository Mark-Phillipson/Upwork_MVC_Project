using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Upwork_MVC.Startup))]
namespace Upwork_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
