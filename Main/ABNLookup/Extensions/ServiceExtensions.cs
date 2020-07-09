using ABNLookup.Infrastructure.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace ABNLookup.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureLoggerManager(this IServiceCollection services) =>
            services.AddScoped<ILoggerManager, LoggerManager>();

    }
}
