﻿using System;
using System.IO;
using System.Threading.Tasks;
using Dech.Hal.Banking.Contracts;
using Dech.Hal.Banking.Ioc;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
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

                Log.Information("Finished Batch process");
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
                
                services.RegisterServices(hostingContext.Configuration);
                services.AddHostedService<Startup>();
                services.Configure<HostOptions>(option =>
                {
                    option.ShutdownTimeout = TimeSpan.FromSeconds(2);
                });

                services.AddDataProtection();
                //.PersistKeysToFileSystem(new DirectoryInfo(@"c:\temp-keys"))
                //.ProtectKeysWithDpapi();

                services.AddDataProtection()
                .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
                {
                    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_GCM,
                    ValidationAlgorithm = ValidationAlgorithm.HMACSHA512

                });
                //.UseCryptographicAlgorithms(new AuthenticatedEncryptionSettings
                //{
                //    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_GCM,
                //    ValidationAlgorithm = ValidationAlgorithm.HMACSHA512
                //});


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
