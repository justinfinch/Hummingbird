using System;
using System.Collections.Generic;
using System.Linq;
using Hummingbird.Data;
using Northwind.Orders.Domain;

namespace Northwind.Orders.Data
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly IDataProvider<Customer> _customerDataProvider;

        public CustomerRepository(IDataProvider<Customer> customerDataProvider)
        {
            _customerDataProvider = customerDataProvider;
        }

        public Customer Get(string id)
        {
            return _customerDataProvider.Find(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<Customer> GetTop(int count)
        {
            return _customerDataProvider.Find(c => c.Id == c.Id).Take(count);
        }
    }
}
