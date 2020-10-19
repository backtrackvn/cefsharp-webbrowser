using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPGOPOS.src.models
{
    class SalesorderItem
    {
        public string id { get; set; }
        public string item_id { get; set; }
        public string item_no { get; set; }
        public string item_name { get; set; }
        public double list_price { get; set; }
        public double quantity { get; set; }
        public double net_amount { get; set; }
        public double amount { get; set; }
        public double discount { get; set; }
        public string note { get; set; }
        public double tax_amount { get; set; }
        public double tax_percent { get; set; }
    }
}
