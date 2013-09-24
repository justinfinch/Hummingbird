using Sample.Employees.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Employees.DatabaseAccess
{
    public class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            Ignore(e => e.CurrentObjectState);
            Property(e => e.Version).IsRowVersion();
        }
    }
}
