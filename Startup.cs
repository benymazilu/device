using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DeviceManagement.Startup))]
namespace DeviceManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
