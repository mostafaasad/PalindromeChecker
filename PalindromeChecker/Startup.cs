using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PalindromeChecker.Startup))]
namespace PalindromeChecker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
