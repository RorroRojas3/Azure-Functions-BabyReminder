using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Rodrigo.Tech.Services.Helpers
{
    public static class DirectoryHelper
    {
        public static string GetCurrentDirectory()
        {
            string directory = "D:\\home\\site\\wwwroot";
            if (Environment.GetEnvironmentVariable("ASPNET_ENVIRONMENT").Equals("local", StringComparison.OrdinalIgnoreCase))
            {
                directory = Directory.GetCurrentDirectory();
            }
            return directory;
        }
    }
}
