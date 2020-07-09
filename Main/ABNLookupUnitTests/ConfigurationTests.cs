using ABNLookup.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABNLookupUnitTests
{
    [TestClass]
    public class ConfigurationTests : TestBase
    {
        [TestMethod]
        public void ConfigurationRoot_Exists()
        {
            Assert.IsNotNull(_configurationRoot);
        }

        [TestMethod]
        public void AppSettingsIConfiguration_Exists()
        {
            Assert.IsNotNull(_configurationRoot);

            var appSettings = _configurationRoot.GetSection(nameof(AppSettings));
            Assert.IsNotNull(appSettings);
        }

        [TestMethod]
        public void AppSettings_Exists()
        {
            Assert.IsNotNull(_configurationRoot);

            var iConfiguration = _configurationRoot.GetSection(nameof(AppSettings));
            Assert.IsNotNull(iConfiguration);

            var appSettings = iConfiguration.Get<AppSettings>();
            Assert.IsNotNull(appSettings);
        }
    }
}
