using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCoreReference.Models
{
    public class CustomerOrdersDTO
    {
        public string CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string CustomerCity { get; set; }

        public IEnumerable<OrderDTO> Orders { get; set; }

    }


}
