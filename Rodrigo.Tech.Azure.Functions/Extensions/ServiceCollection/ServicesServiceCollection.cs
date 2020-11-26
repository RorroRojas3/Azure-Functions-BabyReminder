using Microsoft.Extensions.DependencyInjection;
using Rodrigo.Tech.Services.Implementation;
using Rodrigo.Tech.Services.Interface;

namespace Rodrigo.Tech.Azure.Functions.Extensions.ServiceCollection
{
    public static class ServicesServiceCollection
    {
        public static void AddServicesServiceCollection(this IServiceCollection services)
        {
            services.AddTransient<IStmpService, StmpService>();
        }
    }
}
