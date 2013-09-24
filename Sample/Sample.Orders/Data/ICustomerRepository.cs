using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Orders.Domain;

namespace Sample.Orders.Data
{
    public interface ICustomerRepository
    {
        Customer Get(int customerId);
        IEnumerable<Customer> GetTop(int count);
        IEnumerable<Customer> GetFavoriteCustomers();
    }
}
