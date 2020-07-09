using System.IO;
using Microsoft.Extensions.Configuration;

namespace ABNLookupUnitTests
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(!string.IsNullOrEmpty(outputPath) ? outputPath : Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            return configurationBuilder.Build();
        }
    }
}