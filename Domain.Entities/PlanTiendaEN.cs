using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PlanTiendaEN : PlanesEN
    {
        public int idplantienda { get; set; }
        public string idtipo { get; set; }
        public int idtienda { get; set; }
        public DateTime periodoinifac { get; set; }
        public DateTime periodofinfac { get; set; }
        public decimal importe { get; set; }
        public DateTime? fechapago { get; set; }
        public decimal montoplan { get; set; }
        public string email { get; set; }
    }
}
