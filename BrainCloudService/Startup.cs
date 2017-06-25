using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BrainCloudService.Startup))]

namespace BrainCloudService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}