using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebDocBao.Startup))]
namespace WebDocBao
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
