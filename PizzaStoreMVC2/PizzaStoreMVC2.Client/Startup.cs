using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PizzaStoreMVC2.Client.Startup))]
namespace PizzaStoreMVC2.Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
