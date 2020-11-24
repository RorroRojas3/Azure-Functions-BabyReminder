using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Rodrigo.Tech.Services.Implementation;
using Rodrigo.Tech.Services.Interface;


[assembly: FunctionsStartup(typeof(Rodrigo.Tech.Azure.Functions.Startup))]
namespace Rodrigo.Tech.Azure.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IStmpService, StmpService>();
        }
    }
}
