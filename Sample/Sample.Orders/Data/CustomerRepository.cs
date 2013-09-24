using System;
using System.Collections.Generic;
using System.Linq;
using Hummingbird.Data;
using Sample.Orders.Domain;

namespace Sample.Orders.Data
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly IQueryableDataProvider<Customer> _customerDataProvider;
        private readonly IQueryableDataProvider<Order> _orderData;

        public CustomerRepository(IQueryableDataProvider<Customer> customerDataProvider, IQueryableDataProvider<Order> orderData)
        {
            _customerDataProvider = customerDataProvider;
            _orderData = orderData;
        }

        public Customer Get(int id)
        {
            return _customerDataProvider.Find(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<Customer> GetTop(int count)
        {
            return _customerDataProvider.Query().Take(count).ToList();
        }

        //public IPage<Customer> GetPage(int size = 10, int offset = 0)
        //{
        //    return _customerDataProvider.FindPage(c => c.IsActive && c.BirthDate < Today, size, offest);
        //}

        public IEnumerable<Customer> GetFavoriteCustomers()
        {
            return _customerDataProvider.Execute("CustomersGetFavorite", new { 
                City = "San Francisco",
                Region = "CA"
            });
        }
    }
}
