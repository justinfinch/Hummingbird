using Hummingbird.Data;
using Sample.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Orders.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IQueryableDataProvider<Order> _ordersDataProvider;

        public OrderRepository(IQueryableDataProvider<Order> ordersDataProvider)
        {
            _ordersDataProvider = ordersDataProvider;
        }

        public IEnumerable<Order> GetCustomerOrders(string customerId)
        {
            throw new NotImplementedException();
        }
    }
}
