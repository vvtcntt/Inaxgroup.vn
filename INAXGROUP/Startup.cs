using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(INAXGROUP.Startup))]
namespace INAXGROUP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
