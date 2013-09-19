using Hummingbird.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Employees.Domain
{
    public class Employee : Entity<int>
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public int Number { get; protected set; }
        public int TotalSales { get; protected set; }

        public Employee()
        {

        }

        public Employee(string firstName, string lastName, int number)
        {
            FirstName = firstName;
            LastName = lastName;
            Number = number;

            WasCreated();
        }

        public void IncrementTotalSales(int by)
        {
            TotalSales += by;
            WasModified();
        }

    }
}
