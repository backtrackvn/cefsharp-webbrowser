using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPGOPOS.src.models
{
    class PaymentMethod
    {
        public string method { get; set; }
        public string name { get; set; }
        public double amount { get; set; }
        public string currency_id { get; set; }
    }
}
