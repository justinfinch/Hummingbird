using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Orders.Domain.Services
{
    public interface ICustomerManager
    {
        Customer UpdateCustomer(Customer customer);
    }
}
