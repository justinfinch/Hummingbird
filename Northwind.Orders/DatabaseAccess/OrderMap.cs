using Hummingbird.EntityFramework;
using Northwind.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Orders.DatabaseAccess
{
    public class OrderMap : EntityMap<Order, int?> //THIS SUCKS: find a way to get rid of the need to have the key type.  Why dosen't a base mapper work with interfaces??
    {
        public OrderMap()
            : base(false)
        {
            Ignore(c => c.CreatedBy);
            Ignore(c => c.CreatedDate);
            Ignore(c => c.LastModifiedBy);
            Ignore(c => c.LastModifiedDate);
            Ignore(c => c.Version);

            Property(c => c.Id).HasColumnName("OrderID");
        }

    }
}
