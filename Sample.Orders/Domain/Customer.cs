using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hummingbird.Domain;

namespace Sample.Orders.Domain
{
    public class Customer : Entity<int?>
    {
        public string CompanyName { get; protected set; }
        public string ContactName { get; protected set; }
        public string ContactTitle { get; protected set; }
        public string Address { get; protected set; }
        public string City { get; protected set; }
        public string Region { get; protected set; }
        public string PostalCode { get; protected set; }
        public string Country { get; protected set; }
        public string Fax { get; protected set; }

    }
}
