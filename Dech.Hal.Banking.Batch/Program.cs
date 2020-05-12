using System;
using System.IO;
using System.Threading.Tasks;
using Dech.Hal.Banking.Contracts;
using Dech.Hal.Banking.Ioc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Serilog;

namespace Dech.Hal.Banking.Batch
{
    class Program
    {
        static void Main(string[] args)
        {
            SetLogger();
            try
            {
                Log.Information("Initiate Batch process");

                CreateHostBuilder(args).RunConsoleAsync();

                Log.Information("Finishe Batch process");
                Environment.ExitCode = (int)ExitCodes.Success;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "An unhandled exception occurred.");
                Environment.ExitCode = (int)ExitCodes.Failed;
            }
            finally{
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            new HostBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", true);
                if (args != null) config.AddCommandLine(args);
            })
            .ConfigureServices((hostingContext, services) =>
            {
                services.RegisterServices();
                services.AddHostedService<Startup>();
                
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration);
                logging.AddConsole();
            })
            .UseSerilog();


        private static void SetLogger()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            configuration.RegisterLogger();

        }
    }
}
