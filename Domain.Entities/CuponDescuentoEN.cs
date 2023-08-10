using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CuponDescuentoEN : BaseEN
    {
        public int I_CODIGO_CUPON { get; set; }
        public string D_FECHA_VENCIMIENTO { get; set; }
        public int I_CODIGO_CATEGORIA { get; set; }
        public string V_CLAVE_CUPON { get; set; }
    }
}
