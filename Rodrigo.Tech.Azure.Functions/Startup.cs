using AutoMapper;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rodrigo.Tech.Models.AutoMapper;
using Rodrigo.Tech.Repository.Context;
using Rodrigo.Tech.Repository.Pattern.Implementation;
using Rodrigo.Tech.Repository.Pattern.Interface;
using Rodrigo.Tech.Services.Implementation;
using Rodrigo.Tech.Services.Interface;
using System;

[assembly: FunctionsStartup(typeof(Rodrigo.Tech.Azure.Functions.Startup))]
namespace Rodrigo.Tech.Azure.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            builder.Services.AddTransient<IStmpService, StmpService>();


            builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("AZURE_DB")));
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("AZURE_DB"));

            using (var context = new DatabaseContext(optionsBuilder.Options))
                context.Database.Migrate();
            builder.Services.AddScoped(typeof(IRepositoryPattern<>), typeof(RepositoryPattern<>));
            builder.Services.AddTransient<IEmailRepositoryService, EmailRepositoryService>();
            builder.Services.AddTransient<IEmailBodyRepositoryService, EmailBodyRepositoryService>();
            builder.Services.AddTransient<ILanguageRepositoryService, LanguageRepositoryService>();
        }
    }
}
