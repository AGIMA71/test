using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ABNLookup.Settings;

namespace ABNLookupUnitTests
{
    [TestClass]
    public class DependencyInjectionTests : TestBase
    {
        [TestMethod]
        public void DI_ServiceProvider_Exists()
        {
            Assert.IsNotNull(_serviceProvider);
        }


        [TestMethod]
        public void DI_AppSettings_exists()
        {
            var serviceProvider = _services.BuildServiceProvider();
            Assert.IsNotNull(serviceProvider);

            var ioptions = serviceProvider.GetService<IOptions<AppSettings>>();
            Assert.IsNotNull(ioptions);
        }
    }
}

