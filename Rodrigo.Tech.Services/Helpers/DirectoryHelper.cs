using Rodrigo.Tech.Models.Constants;
using System;
using System.IO;

namespace Rodrigo.Tech.Services.Helpers
{
    public static class DirectoryHelper
    {
        public static string GetCurrentDirectory()
        {
            string directory = Environment.GetEnvironmentVariable(EnvironmentVariableConstants.AZURE_PORTAL_DIRECTORY);
            if (Environment.GetEnvironmentVariable(EnvironmentVariableConstants.ASPNETCORE_ENVIRONMENT).Equals("local", StringComparison.OrdinalIgnoreCase))
            {
                directory = Directory.GetCurrentDirectory();
            }
            return directory;
        }
    }
}
