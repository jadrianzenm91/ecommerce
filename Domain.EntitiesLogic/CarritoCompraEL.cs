using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntitiesLogic
{
    public class CarritoCompraTemporalEL
    {
        public List<DetalleProducto> listDetalleProducto { get; set; }   
    }
    public class CarritoDetalle
    {
        public CarritoDetalle()
        {
            this.carritoCompraEL = new CarritoCompraEL();
            this.listProductos = new List<ProductoEL>();
        }
        public CarritoCompraEL carritoCompraEL { get; set; }
        public List<ProductoEL> listProductos { get; set; }
    }
    public class CarritoCompraEL
    {
        public string token { get; set; }
        public string token_payment { get; set; }
        public List<ProductoEL> listProductos { get; set; }
        public long codigoCompra { get; set; }
        public DateTime? fechaCompra { get; set; }
        public string formaPago { get; set; }
        public int codigoFormaPago { get; set; }
        public int? codigoEstadoCompra { get; set; }
        public string estadoCompra { get; set; }
        public string codigoUsuario { get; set; }
        public string nombres { get; set; }
        public string dni { get; set; }
        public string email { get; set; }
        public int celular { get; set; }
        public string direccion { get; set; }
        public decimal total { get; set; }
        public string provider { get; set; }
        public string usuario { get; set; }
        public string urlPhotoUsuario { get; set; }
        public int idtienda { get; set; }
        public string tienda { get; set; }
        public string razonsocial { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string fechaEntregaAprox { get; set; }
        public string costoEnvio { get; set; }
        public string tarjeta { get; set; }
        public string mensajeEstadoCompra { get; set; }
        public string logotienda { get; set; }
        public string facebook { get; set; }
        public string telefonotienda { get; set; }
        public string webtienda { get; set; }
        public string emailtienda { get; set; }
        public long I_CODIGO_USUARIO
        {
            get
            {
                if (codigoUsuario == null)
                {
                    return 0;
                }
                else
                {
                    try
                    {
                        return codigoUsuario == null ? 0 :
                            Convert.ToInt64(Infrastructure.CrossCutting.Encrypting.DecryptKey(codigoUsuario));
                    }
                    catch
                    {
                        return 0;
                    }
                    
                }
            }
        }
        
    }

    public class DetalleProducto
    {
        public string idproducto { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public long I_CODIGO_PRODUCTO
        {
            get
            {
                if (idproducto == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt64(Infrastructure.CrossCutting.Encrypting.DecryptKey(idproducto));
                }
            }
        }
    }
}
