using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestWebForm.Startup))]
namespace TestWebForm
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
