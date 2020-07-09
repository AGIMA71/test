using ABNLookup.Constants;
using ABNLookup.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace ABNLookup.CustomFilters
{
    public class DeprecatedFilterAttribute : ResultFilterAttribute
    {
        private readonly string _retirementDate;
        private readonly AppSettings appSettings;

        public DeprecatedFilterAttribute(IConfiguration configuration)
        {
            appSettings = configuration
               .GetSection(AbnLookupConstants.AppSettings)
               .Get<AppSettings>();
            var apiSettings = appSettings.API;
            _retirementDate = apiSettings.RetirementDate?.V1;
        }
                 
        /// <summary>
        /// Add custom headers to response before it is
        /// sent to the client.
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {                                  
            Type type = context.Controller.GetType();
            string requestUrlPath = context.HttpContext.Request.Path.Value;

            string apiVersionFromRequest = requestUrlPath.Split('/')[2].Replace("v", "", StringComparison.OrdinalIgnoreCase);

            var attrs = type.GetCustomAttributes(false).Where(att => att is ApiVersionAttribute && ((ApiVersionAttribute)att).Deprecated).ToArray();

            foreach (var attr in attrs)
            {
                ApiVersionAttribute apiVersion = (ApiVersionAttribute)attr;
                string apiVersionFromAttribute = apiVersion.Versions[0].MajorVersion?.ToString();

                if (apiVersionFromRequest == apiVersionFromAttribute)
                {
                    context.HttpContext.Response.Headers.Add(AbnLookupConstants.DeprecatedHeader, new string[] { "true" });
                    context.HttpContext.Response.Headers.Add(AbnLookupConstants.RetirementHeader, new string[] { _retirementDate });
                }
            }

            base.OnResultExecuting(context);
        }

    }
}
