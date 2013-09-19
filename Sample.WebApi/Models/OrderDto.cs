using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample.WebApi.Models
{
    public class OrderDto
    {
        public decimal Total { get; set; }
        public int EmployeeNumber { get; set; }
    }
}