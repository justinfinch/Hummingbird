using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Orders.Domain;
using Hummingbird.EntityFramework;

namespace Sample.Orders.DatabaseAccess
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            Ignore(e => e.CurrentObjectState);
            Property(e => e.Version).IsRowVersion();
        }

    }
}
