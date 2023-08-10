using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntitiesLogic
{
    public class ProductoEL : ProductoEN
    {
        public ProductoEL()
        {
            this._detalleCompraEN = new DetalleCompraEN();
            
            this._listProductoCaracteristicaEL = new List<ProductoCaracteristicaEL>();
        }
        public DetalleCompraEN _detalleCompraEN { get; set; }
        //public List<ImagenProductoEN> _listImagenProductoEN { get; set; }
        public List<ProductoCaracteristicaEL> _listProductoCaracteristicaEL { get; set; }
        public string V_CATEGORIA { get; set; }
        public string V_TIENDA { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }
}
