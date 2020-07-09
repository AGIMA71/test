using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ABNLookup.Settings;
using ABNLookup.Interfaces;
using ABNLookup.Services;

namespace ABNLookupUnitTests
{
    [TestClass]
    public class TestBase
    {
        internal IConfigurationRoot _configurationRoot;
        internal ServiceCollection _services;
        internal ServiceProvider _serviceProvider;

        public TestBase()
        {
            _configurationRoot = ConfigurationHelper.GetIConfigurationRoot(Directory.GetCurrentDirectory());
            var appSettings = _configurationRoot.GetSection(nameof(AppSettings));

            _services = new ServiceCollection();

            // update this to match the same configuration as the API
            _services.AddLogging();
            _services.Configure<AppSettings>(appSettings);
            _services.AddSingleton(appSettings)
                .AddSingleton<IAbnService, AbnService>()
                .AddSingleton<IAcnService, AcnService>()
                .AddSingleton<IOrgNameService, OrgNameService>()
                .AddHealthChecks();

            _serviceProvider = _services.BuildServiceProvider();
        }

        ~TestBase()
        {
            if (_serviceProvider != null)
                _serviceProvider.Dispose();
        }
    }
}
