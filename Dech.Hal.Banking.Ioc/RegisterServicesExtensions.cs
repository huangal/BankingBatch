using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Dech.Hal.Banking.Ioc
{
    public static class RegisterServicesExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {

            return services;
        }

        public static void RegisterLogger(this IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
