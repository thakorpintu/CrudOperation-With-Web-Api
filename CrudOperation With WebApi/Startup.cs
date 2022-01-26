using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrudOperation_With_WebApi.Startup))]
namespace CrudOperation_With_WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
