﻿using Sample.DatabaseAccess;
using Sample.Employees.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Employees.DatabaseAccess
{
    public class EmployeesContext : SampleBaseContext<EmployeesContext>
    {
        public IDbSet<Employee> Employees { get; set; }

        public EmployeesContext()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
