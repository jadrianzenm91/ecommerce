using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PromocionEN : BaseEN
    {
        public int I_CODIGO_PROMOCION { get; set; }
        public string D_FECHA_VENCIMIENTO { get; set; }
        public decimal N_PRECIO { get; set; }
        public long I_CODIGO_PRODUCTO { get; set; }
    }
}
