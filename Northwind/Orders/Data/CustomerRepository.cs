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
        private readonly ICacheProvider<Customer> _customerCacheProvider; 

        public CustomerRepository(IDataProvider<Customer> customerDataProvider, ICacheProvider<Customer> customerCacheProvider)
        {
            _customerDataProvider = customerDataProvider;
            _customerCacheProvider = customerCacheProvider;
        }

        public Customer Get(string id)
        {
            return _customerDataProvider.Find(c => c.Id == id).FirstOrDefault();
        }

        public IList<Customer> GetTop(int count)
        {
            throw new NotImplementedException();
        }
    }
}
