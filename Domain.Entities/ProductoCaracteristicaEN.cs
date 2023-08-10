using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    
    public class ProductoCaracteristicaEN : BaseEN
    {
        public long? I_PRODUCTO_CARACTERISTICA { get; set; }
        public long? I_CODIGO_PRODUCTO { get; set; }
        public int? I_CODIGO_CARACTERISTICA { get; set; }
    }
}
