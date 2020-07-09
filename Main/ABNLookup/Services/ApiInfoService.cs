using ABNLookup.Domain.Models;
using ABNLookup.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using ABNLookup.Settings;

namespace ABNLookup.Services
{
    public class ApiInfoService : IApiInfoService
    {
        private readonly AppSettings appSettings;
        public ApiInfoService(IConfiguration configuration)
        {
            appSettings = configuration
               .GetSection("AppSettings")
               .Get<AppSettings>();
        }

        public ApiInfo GetApiInfo()
        {
            var apiSettings = appSettings.API;
            string apiStatus = apiSettings.Status?.V1;
            return GetApiInfo(apiStatus);           
        }
        public ApiInfo GetApiV2Info()
        {
            var apiSettings = appSettings.API;
            string apiStatus = apiSettings.Status?.V2;
            return GetApiInfo(apiStatus);
        }

        /// <summary>
        /// Returns the api inf
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private ApiInfo GetApiInfo(string status)
        {
            string abndllVersion = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            var apiInfo = new ApiInfo { Version = abndllVersion, Status = status };
            return apiInfo;
        }
    }
}
