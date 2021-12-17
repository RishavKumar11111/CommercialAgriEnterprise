using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CommercialAgriEnterprise.Startup))]
namespace CommercialAgriEnterprise
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}