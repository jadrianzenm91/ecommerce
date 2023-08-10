using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PaymentEN
    {
        public Dictionary<string, object> metadata { get; set; }
        public int amount { get; set; }
        public string description { get; set; }
        public string token_payment { get; set; }
        public string email { get; set; }

    }
    public class PaymentResponseEN
    {
        public string message { get; set; }
        public string tarjeta { get; set; }
        public bool success { get; set; }
    }
}
