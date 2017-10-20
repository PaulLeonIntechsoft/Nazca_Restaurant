using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nazca_Restaurant.Startup))]
namespace Nazca_Restaurant
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
