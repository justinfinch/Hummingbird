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

        public IEnumerable<Order> Search(DateTime? placedOn, decimal? orderTotal)
        {
            var query = ExpressionHelper.True<Order>();

            if(placedOn.HasValue)
                query = query.And(p => p.PlacedOn == placedOn.Value);
            
            if(orderTotal.HasValue)
                query = query.Or(p => p.Total == orderTotal.Value);

            return _ordersDataProvider.Find(query);
        }

        public IEnumerable<Order> GetCustomerOrders(string customerId)
        {
            throw new NotImplementedException();
        }

        public Order Save(Order order)
        {
            return _ordersDataProvider.Save(order);
        }

        public IEnumerable<Order> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
