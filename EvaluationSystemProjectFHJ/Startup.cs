using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EvaluationSystemProjectFHJ.Startup))]
namespace EvaluationSystemProjectFHJ
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
