using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample.WebApi.Models
{
    public class LineItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Sku { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal ExtendedPrice { get; set; }
    }
}