using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hummingbird.Domain;

namespace Sample.Orders.Domain
{
    public class Order : Entity<int?>
    {
        public DateTime PlacedOn { get; protected set; }
        public decimal Total { get; protected set; }
        public int EmployeeNumber { get; protected set; }

        public Order()
        {

        }

        public Order(DateTime placedOn, decimal total, int employeeNumber)
        {
            PlacedOn = placedOn;
            Total = total;
            EmployeeNumber = employeeNumber;

            WasCreated();
        }
    }
}
