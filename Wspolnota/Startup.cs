using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Wspolnota.Startup))]
namespace Wspolnota
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
