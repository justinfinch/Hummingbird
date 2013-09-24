using Hummingbird.EntityFramework;
using Sample.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Orders.DatabaseAccess
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            Ignore(e => e.CurrentObjectState);
            Property(e => e.Version).IsRowVersion();
        }

    }
}
