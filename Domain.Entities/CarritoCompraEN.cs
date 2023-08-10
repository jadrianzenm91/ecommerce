using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CarritoCompraEN: BaseEN
    {
        public CarritoCompraEN()
        {
            this._UsuarioEN = new UsuarioEN();
            this.tiendaEN = new TiendaEN();
        }
        public long I_CODIGO_COMPRA { get; set; }
        public long I_CODIGO_USUARIO { get; set; }
        public UsuarioEN _UsuarioEN { get; set; }
        public string NOMBRE_CONTACTO { get; set; }
        public long CELULAR_CONTACTO { get; set; }
        public string EMAIL { get; set; }
        public string V_DNI { get; set; }
        public string V_DIRECCION_ENTREGA { get; set; }
        public string D_FECHA_COMPRA { get; set; }
        public int I_CODIGO_FORMA_PAGO { get; set; }
                public string V_TAJETA_PAGO { get; set; }
                public double N_COSTO_ENVIO { get; set; }
                public string D_FECHAENTREGAESTI { get; set; }
                public double N_SUBTOTAL { get; set; }
                public decimal N_TOTAL { get; set; }
                public string latitude { get; set; }
                public string longitude { get; set; }
        public int I_CODIGO_TIENDA { get; set; }
        public int I_ESTADO_COMPRA { get; set; }
        public string codigoCompraEncry { get; set; }
        public List<DetalleCompraEN> _DetalleCompraEN { get; set; }
        public TiendaEN tiendaEN { get; set; }
    }
    
}
