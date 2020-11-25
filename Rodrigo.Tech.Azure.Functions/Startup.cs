using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rodrigo.Tech.Repository.Context;
using Rodrigo.Tech.Repository.Pattern.Implementation;
using Rodrigo.Tech.Repository.Pattern.Interface;
using Rodrigo.Tech.Services.Implementation;
using Rodrigo.Tech.Services.Interface;


[assembly: FunctionsStartup(typeof(Rodrigo.Tech.Azure.Functions.Startup))]
namespace Rodrigo.Tech.Azure.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var db = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BabyReminder;Integrated Security=True";
            builder.Services.AddTransient<IStmpService, StmpService>();
            builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(db));
            builder.Services.AddScoped(typeof(IRepositoryPattern<>), typeof(RepositoryPattern<>));
        }
    }
}
