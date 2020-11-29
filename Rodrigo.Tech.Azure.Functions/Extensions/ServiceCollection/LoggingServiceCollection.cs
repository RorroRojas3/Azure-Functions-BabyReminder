using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Rodrigo.Tech.Azure.Functions.Extensions.ServiceCollection
{
    public static class LoggingServiceCollection
    {
        /// <summary>
        ///     Adds Logging service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddLoggingServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var logDirectory = Path.Combine(currentDirectory, "Logs");
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            var logger = new LoggerConfiguration()
                            .WriteTo.Console(LogEventLevel.Information)
                            .ReadFrom.Configuration(configuration)
                            .CreateLogger();
            services.AddSingleton(logger);
            services.AddLogging(l => l.AddSerilog(logger));

            Log.Logger = new LoggerConfiguration()
                        .WriteTo.Console(LogEventLevel.Information)
                        .ReadFrom.Configuration(configuration)
                        .CreateLogger();
            services.AddSingleton(Log.Logger);
        }
    }
}
