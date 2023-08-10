using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CaracteristicaEN : BaseEN
    {
        public int I_CODIGO_CARACTERISTICA { get; set; }
        public string V_CARACTERISTICA { get; set; }
        public int I_CODIGO_CATEGORIA { get; set; }
    }
}
