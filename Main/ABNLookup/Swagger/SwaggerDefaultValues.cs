using System.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using ABNLookup.Settings;
using Microsoft.AspNetCore.Mvc;

namespace ABNLookup.Swagger
{
    // See https://github.com/microsoft/aspnet-api-versioning/wiki/Swashbuckle-Integration
    // See https://github.com/microsoft/aspnet-api-versioning/blob/master/samples/aspnetcore/SwaggerSample/SwaggerDefaultValues.cs
    /// <summary>
    /// Represents the Swagger/Swashbuckle operation filter used to document the implicit API version parameter.
    /// </summary>
    /// <remarks>This <see cref="IOperationFilter"/> is only required due to bugs in the <see cref="SwaggerGenerator"/>.
    /// Once they are fixed and published, this class can be removed.</remarks>
    public class SwaggerDefaultValues : IOperationFilter
    {
        private readonly IConfiguration configuration;
        private readonly AppSettings appSettings;

        public SwaggerDefaultValues(IConfiguration configuration)
        {
            this.configuration = configuration;
            appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
        }

        /// <summary>
        /// Applies the filter to the specified operation using the given context.
        /// </summary>
        /// <param name="operation">The operation to apply the filter to.</param>
        /// <param name="context">The current operation filter context.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            // currently the OpenApi operation can be deprecated by:
            // - the [Obsolete] attribute on an operation
            // - via version status in appsettings
            // - via aspnet api versioning lib (ApiVersion attributes)
            operation.Deprecated |= IsDeprecated(context);

            if (operation.Parameters == null)
                return;

            // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/412
            // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/pull/413
            foreach (var parameter in operation.Parameters)
            {
                var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if (parameter.Schema.Default == null && description.DefaultValue != null)
                {
                    parameter.Schema.Default = new OpenApiString(description.DefaultValue.ToString());
                }

                parameter.Required |= description.IsRequired;
            }
        }

        /// <summary>
        /// Grabs the deprecation status in order of appsettings file first with fallback to api versioning attributes.
        /// </summary>
        public bool IsDeprecated(OperationFilterContext context)
        {
            ApiVersion apiVersion = context.ApiDescription.GetApiVersion();

            if (apiVersion.MajorVersion == 1 && appSettings.API.Status.V1 == OpenApiConstants.Deprecated.ToUpper())
            {
                return true;
            }
            else if (apiVersion.MajorVersion == 2 && appSettings.API.Status.V1 == OpenApiConstants.Deprecated.ToUpper())
            {
                return (appSettings.API.Status.V2 == OpenApiConstants.Deprecated.ToUpper());
            }

            return context.ApiDescription.IsDeprecated();
        }

    }
}