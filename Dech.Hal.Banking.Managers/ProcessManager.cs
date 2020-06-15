using System;
using Dech.Hal.Banking.Contracts.Interfaces;
using Microsoft.Extensions.Logging;

namespace Dech.Hal.Banking.Managers
{
    public class ProcessManager : IProcessManager
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger _logger;
        private readonly IServiceDataProtection _dataProtection;

        public ProcessManager(ICustomerService customerService, ILogger<ProcessManager> logger, IServiceDataProtection dataProtection)
        {
            _customerService = customerService;
            _logger = logger;
            _dataProtection = dataProtection;
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

        //private async Task ReadMessagesAsync()
        //{
        //    connection.On<string, string>("ReceiveMessage", (user, message) =>
        //    {
        //        this.Dispatcher.Invoke(() =>
        //        {
        //            var newMessage = $"{user}: {message}";
        //            messagesList.Items.Add(newMessage);
        //        });
        //    });

        //    try
        //    {
        //        await connection.StartAsync();
        //        messagesList.Items.Add("Connection started");
        //        connectButton.IsEnabled = false;
        //        sendButton.IsEnabled = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //}


    }
}
