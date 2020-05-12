using System;
using System.Collections.Generic;

namespace Dech.Hal.Banking.Contracts.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetCustomers();
    }
}
