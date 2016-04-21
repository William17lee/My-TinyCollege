using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mytinycollege.Startup))]
namespace mytinycollege
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
