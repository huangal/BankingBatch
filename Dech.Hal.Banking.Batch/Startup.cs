using System;
using System.Threading;
using System.Threading.Tasks;
using Dech.Hal.Banking.Contracts.Interfaces;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Dech.Hal.Banking.Batch
{
    public class Startup : IHostedService
    {
        private readonly IProcessManager _process;

        public Startup(IProcessManager process)
        {
            _process = process;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            Log.Information("Startup.StartAsync: Start Batch");
            Console.WriteLine("Startup.StartAsync: Start Batch");

            _process.Run();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Log.Information("Startup.StopAsync Completed Batch");

            Console.WriteLine("Startup.StopAsync Completed Batch");

            //Log.CloseAndFlush();
            return Task.CompletedTask;

           
        }
    }
}
