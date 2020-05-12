using System;
using Dech.Hal.Banking.Contracts;
using Dech.Hal.Banking.Contracts.Interfaces;
using Microsoft.Extensions.Logging;

namespace Dech.Hal.Banking.Managers
{
    public class ProcessManager : IProcessManager
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger _logger;
        public ProcessManager(ICustomerService customerService, ILogger<ProcessManager> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }


        public bool Run()
        {

            try
            {
                var customers = _customerService.GetCustomers();
                Console.WriteLine();

                foreach (var customer in customers)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Customer:{customer.Name} email:{customer.Email} Product:{customer.Product}");
                    Console.ResetColor();
                }
                Console.WriteLine();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error running process");
                return false;
            }


        }


    }
}
