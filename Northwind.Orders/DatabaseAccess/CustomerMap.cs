using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Orders.Domain;

namespace Northwind.Orders.DatabaseAccess
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            Ignore(c => c.CreatedBy);
            Ignore(c => c.CreatedDate);
            Ignore(c => c.LastModifiedBy);
            Ignore(c => c.LastModifiedDate);
            Ignore(c => c.Version);
            Ignore(c => c.CurrentObjectState);

            Property(c => c.Id).HasColumnName("CustomerId");
        }
    }
}
