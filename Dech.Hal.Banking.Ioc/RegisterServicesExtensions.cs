using System;
using Dech.Hal.Banking.Contracts;
using Dech.Hal.Banking.Contracts.Interfaces;
using Dech.Hal.Banking.Managers;
using Dech.Hal.Banking.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Dech.Hal.Banking.Ioc
{
    public static class RegisterServicesExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterLogger(configuration); 
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<IProcessManager, ProcessManager>();

            services.AddSingleton<UniqueCode>();
            services.AddSingleton<IServiceDataProtection, ServiceDataProtection>();

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
