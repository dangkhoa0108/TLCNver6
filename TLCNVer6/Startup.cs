using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TLCNVer6.Startup))]
namespace TLCNVer6
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
