using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCoreReference.Models
{
    public class OrderDTO // Data Transfer Object
    {
        public int OrderId { get; set; }
        public decimal? Freight { get; set; } //cena

        public decimal? CenaBrutto { get; set; }

        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
    }
}
