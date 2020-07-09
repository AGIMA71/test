using System.IO;
using System.Net.Mime;
using System.Reflection;
using ABNLookup.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using AutoMapper;
using ABNLookup.Profiles;
using ABNLookup.Services;
using ABNLookup.Swagger;
using ABNLookup.Settings;
using ABNLookup.Middlewares;
using ABNLookup.Hypermedia;
using ABNLookup.Infrastructure;
using NLog;
using ABNLookup.Extensions;
using ABNLookup.CustomFilters;
using ABNLookup.Filters;

namespace ABNLookup
{
    // Based on Microsoft sample pattern
    // REF: https://github.com/microsoft/aspnet-api-versioning/blob/master/samples/aspnetcore/SwaggerSample/Startup.cs
    public class Startup
    {
        private AppSettings appSettings;
        private readonly IHostEnvironment environment;
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            this.environment = environment;

            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureLoggerManager();

            services.AddControllers(config =>
            {
                config.ReturnHttpNotAcceptable = true;
                config.Filters.Add(new ConsumesAttribute(
                    MediaTypeNames.Application.Json));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Latest);

            appSettings = Configuration
                .GetSection("AppSettings")
                .Get<AppSettings>();

            //services.AddDbContext<AbnContext>(options =>
            //    options.UseSqlite(appSettings.ConnectionString));

            services.AddSingleton(appSettings)
                .AddScoped(_ =>
                    new AbnContext(appSettings.ConnectionString,
                        this.environment.IsDevelopment()))
                .AddScoped<IAbnService, AbnService>()
                .AddScoped<IAcnService, AcnService>()
                .AddScoped<IOrgNameService, OrgNameService>()
                .AddScoped<IMessageCodeService, MessageCodeService>()
                .AddScoped<CustomAsyncActionFilter>()
                .AddSingleton<IHypermediaService, HypermediaService>()
                .AddSingleton<ILinkData, LinkData>()
                .AddSingleton<IEmailService, EmailService>()
                .AddScoped<ISortMappingService, SortMappingService>()
                .AddScoped<IApiInfoService, ApiInfoService>()
                .AddScoped<DeprecatedFilterAttribute>()
                .AddScoped<ValidateModelAttribute>()
                .AddHealthChecks();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                // when false(default), model state is checked earlier in the pipeline as part of [ApiController] features
                // allows our custom validation handling filter to get hit later in the pipeline
                options.SuppressModelStateInvalidFilter = true;
            });

            // For more info on option available and what they mean
            // REF: https://github.com/microsoft/aspnet-api-versioning/wiki/API-Versioning-Options
            services.AddApiVersioning(config =>
            {
                config.ReportApiVersions = true;
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.DefaultApiVersion = new ApiVersion(2, 0);
                config.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            // REF: https://github.com/microsoft/aspnet-api-versioning
            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            // This is direct from the github docs, can configure this later.
            services.AddVersionedApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
            });

            // Swagger
            if (appSettings.Swagger.Enabled)
            {
                services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

                    services.AddSwaggerGen(options =>
                    {
                        options.OperationFilter<SwaggerDefaultValues>();
                        options.IncludeXmlComments(XmlCommentsFilePath);
                    });
            }

            services.AddAutoMapper(typeof(AbnProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseMiddleware<ExceptionHandler>();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/api/abn/health");
            });

            //SWAGGER
            if (appSettings.IsValid() && appSettings.Swagger.Enabled)
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                });
            }
        }

        private static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = $"{typeof(Startup).GetTypeInfo().Assembly.GetName().Name}.xml";
                return Path.Combine(basePath, fileName);
            }
        }
    }
}