using System;
using System.Collections.Generic;
using Dech.Hal.Banking.Contracts;
using Dech.Hal.Banking.Contracts.Interfaces;
using Newtonsoft.Json;

namespace Dech.Hal.Banking.Services
{
    public class CustomerService : ICustomerService
    {


        public IEnumerable<Customer> GetCustomers()
        {
            return GetCustomerList();
        }



        private IEnumerable<Customer> GetCustomerList()
        {

            string customerList = @"[{
            'id': 1,
            'name': 'Erin',
            'last': 'Foster',
            'age': 52,
            'email': 'Erin.Foster@live.com',
            'product': 'Healthcare Card'
          },
          {
            'id': 2,
            'name': 'Simon',
            'last': 'Diaz',
            'age': 48,
            'email': 'Simon.Diaz@microsoft.com',
            'product': 'Visa Card'
          },
          {
            'id': 3,
            'name': 'Vanessa',
            'last': 'Allen',
            'age': 18,
            'email': 'Vanessa.Allen@telus.net',
            'product': 'Rewards Card'
          },
          {
            'id': 4,
            'name': 'Mackenzie',
            'last': 'Perry',
            'age': 52,
            'email': 'Mackenzie.Perry@rogers.ca',
            'product': 'Rewards Card'
          },
          {
            'id': 5,
            'name': 'Sydney',
            'last': 'Moore',
            'age': 57,
            'email': 'Sydney.Moore@att.com',
            'product': 'Healthcare Card'
          },
          {
            'id': 6,
            'name': 'Caitlin',
            'last': 'Daeninck',
            'age': 48,
            'email': 'Caitlin.Daeninck@att.com',
            'product': 'Goal Saving Card'
          },
          {
            'id': 7,
            'name': 'Lucas',
            'last': 'Getzlaff',
            'age': 40,
            'email': 'Lucas.Getzlaff@rogers.ca',
            'product': 'Healthcare Card'
          },
          {
            'id': 8,
            'name': 'Brian',
            'last': 'Martinez',
            'age': 50,
            'email': 'Brian.Martinez@gmail.com',
            'product': 'Visa Card'
          },
          {
            'id': 9,
            'name': 'Kecia',
            'last': 'Sanchez',
            'age': 37,
            'email': 'Kecia.Sanchez@shaw.ca',
            'product': 'Healthcare Card'
          },
          {
            'id': 10,
            'name': 'Alexa',
            'last': 'Ramirez',
            'age': 18,
            'email': 'Alexa.Ramirez@att.com',
            'product': 'Visa Card'
          }]";

            IEnumerable<Customer> customers = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Customer>>(customerList);

            return customers;
        }


    }
}
