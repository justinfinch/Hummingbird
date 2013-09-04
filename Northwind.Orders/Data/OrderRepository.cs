using Hummingbird.Data;
using Northwind.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Orders.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDataProvider<Order> _ordersDataProvider;

        public OrderRepository(IDataProvider<Order> ordersDataProvider)
        {
            _ordersDataProvider = ordersDataProvider;
        }

        public IEnumerable<Order> GetCustomerOrders(string customerId)
        {
            throw new NotImplementedException();
        }
    }
}
