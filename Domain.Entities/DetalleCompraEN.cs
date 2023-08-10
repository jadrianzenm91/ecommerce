using Infrastructure.CrossCutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DetalleCompraEN : BaseEN
    {
        public long codigoDetalle { get; set; }
        public long codigoCompra { get; set; }
        public long codigoProducto { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public decimal total { get; set; }
    }
    public class DetalleCompraTempEN : ProductoEN
    {
        public long? id { get; set; }
        public string token { get; set; }
        public int cant { get; set; }
        public long idprod { get; set; }
        public string idprodEnc { get; set; }
        public string idEnc { get; set; }
        public decimal total { get; set; }
    }
}
