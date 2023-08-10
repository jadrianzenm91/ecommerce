using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntitiesLogic
{
    public class ResponseEL
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string stacktrace { get; set; }
        public string type { get; set; }
        public object data { get; set; }
        
    }
}
