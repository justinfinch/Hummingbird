using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Orders.Domain;
using Hummingbird.EntityFramework;

namespace Northwind.Orders.DatabaseAccess
{
    public class CustomerMap : EntityMap<Customer, string> //THIS SUCKS: find a way to get rid of the need to have the key type.  Why dosen't a base mapper work with interfaces??
    {
        public CustomerMap()
            :base(false)
        {
            Ignore(c => c.CreatedBy);
            Ignore(c => c.CreatedDate);
            Ignore(c => c.LastModifiedBy);
            Ignore(c => c.LastModifiedDate);
            Ignore(c => c.Version);

            Property(c => c.Id).HasColumnName("CustomerId");
        }

    }
}
