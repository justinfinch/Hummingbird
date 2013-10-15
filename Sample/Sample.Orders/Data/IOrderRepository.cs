using Sample.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Orders.Data
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetCustomerOrders(string customerId);
        Order Save(Order order);
        IEnumerable<Order> GetAll();
        IEnumerable<Order> Search(DateTime? placedOn, decimal? orderTotal);
    }
}
