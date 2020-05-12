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
            Log.Information("Start Batch");
            Console.WriteLine("Started");

            _process.Run();

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
