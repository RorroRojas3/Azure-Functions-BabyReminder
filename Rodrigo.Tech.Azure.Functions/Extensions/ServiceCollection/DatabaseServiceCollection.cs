using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rodrigo.Tech.Models.Constants;
using Rodrigo.Tech.Repository.Context;
using Rodrigo.Tech.Repository.Pattern.Implementation;
using Rodrigo.Tech.Repository.Pattern.Interface;
using Rodrigo.Tech.Services.Implementation;
using Rodrigo.Tech.Services.Interface;
using System;

namespace Rodrigo.Tech.Azure.Functions.Extensions.ServiceCollection
{
    public static class DatabaseServiceCollection
    {
        public static void AddDatabaseServiceCollection(this IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable(EnvironmentVariableConstants.AZURE_DB);
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new DatabaseContext(optionsBuilder.Options))
                context.Database.Migrate();
            services.AddScoped(typeof(IRepositoryPattern<>), typeof(RepositoryPattern<>));
            services.AddTransient<IEmailRepositoryService, EmailRepositoryService>();
            services.AddTransient<IEmailBodyRepositoryService, EmailBodyRepositoryService>();
            services.AddTransient<ILanguageRepositoryService, LanguageRepositoryService>();
        }
    }
}
