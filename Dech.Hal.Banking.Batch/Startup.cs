using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Dech.Hal.Banking.Batch
{
    public class Startup : IHostedService
    {

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Log.Information("Start Batch");
            Console.WriteLine("Started");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Log.Information("Completed Batch");
            Console.WriteLine("Completed");

            //Log.CloseAndFlush();
            return Task.CompletedTask;

           
        }
    }
}
