using AutoMapper;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rodrigo.Tech.Azure.Functions.Extensions.ServiceCollection;
using Rodrigo.Tech.Models.AutoMapper;
using Rodrigo.Tech.Repository.Context;
using Rodrigo.Tech.Repository.Pattern.Implementation;
using Rodrigo.Tech.Repository.Pattern.Interface;
using Rodrigo.Tech.Services.Implementation;
using Rodrigo.Tech.Services.Interface;
using Serilog;
using System;
using System.IO;

[assembly: FunctionsStartup(typeof(Rodrigo.Tech.Azure.Functions.Startup))]
namespace Rodrigo.Tech.Azure.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();
            builder.Services.AddLoggingServiceCollection(configuration);
            Log.Logger.Information($"Registering AutoMapper");
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            Log.Logger.Information($"Registering Services");
            builder.Services.AddServicesServiceCollection();
            Log.Logger.Information($"Registering Database");
            builder.Services.AddDatabaseServiceCollection();
        }
    }
}
