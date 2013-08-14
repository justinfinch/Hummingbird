using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Orders.Domain;

namespace Northwind.Orders.Data
{
    public interface ICustomerRepository
    {
        Customer Get(string customerId);
        IEnumerable<Customer> GetTop(int count);
    }
}
