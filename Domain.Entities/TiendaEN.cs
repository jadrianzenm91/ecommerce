using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TiendaEN : BaseEN
    {
        public TiendaEN()
        {
            this._ConfiguracionTiendaEN = new ConfiguracionTiendaEN();
        }
        [Display(Name = "Identificador")]
        public int I_CODIGO_TIENDA { get; set; }

        public string codigoTienda
        {
            get
            {
                if (I_CODIGO_TIENDA == 0)
                {
                    return string.Empty;
                }
                else
                {
                    return string.Concat("C0100", I_CODIGO_TIENDA);
                }
            }
        }

        public int? I_CODIGO_USUARIO { get; set; }
        public string V_DESCRIPCION { get; set; }
        [Display(Name = "URL TiendaVirtual(*)")]
        [StringLength(150, ErrorMessage = "El {0} debe tener mínimo {2} caracteres de longitud sin espacio en vacío. Ejm: ZAPATOSPATTY", MinimumLength = 3)]
        [Required(ErrorMessage = "Este campo es Obligatorio.")]
        public string V_TIENDA { get; set; }

        [Display(Name = "Razon Social/Nombre Negocio(*)")]
        [StringLength(500, ErrorMessage = "El {0} debe tener {1} caracteres de longitud máximo.", MinimumLength = 5)]
        [Required(ErrorMessage = "Este campo es Obligatorio.")]
        public string V_RAZON_SOCIAL { get; set; }

         [Display(Name = "RUC(*)")]
         [StringLength(15, ErrorMessage = "El {0} debe tener {1} caracteres de longitud máximo.", MinimumLength = 8)]
         [Required(ErrorMessage = "Este campo es Obligatorio.")]
        public string V_RUC { get; set; }

        [Display(Name = "Dirección(*)")]
        [StringLength(200, ErrorMessage = "El {0} debe tener {1} caracteres de longitud máximo.")]
        [Required(ErrorMessage = "Este campo es Obligatorio.")]
        public string V_DIRECCION { get; set; }

        [Display(Name = "Página web")]
        [StringLength(250, ErrorMessage = "El {0} debe tener {1} caracteres de longitud máximo.")]
        public string V_WEB { get; set; }

        public string V_CARPETAIMAG { get; set; }

        [Display(Name = "Whatsapp")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Telofono inválido. Ejm: +51949543777")]
        public string V_TELEFONO { get; set; }

        [Display(Name = "Logotipo(*)")]
        [StringLength(3000, ErrorMessage = "El {0} debe tener {1} caracteres de longitud máximo.")]
        [Required(ErrorMessage = "Este campo es Obligatorio.")]
        public string V_LOGO { get; set; }

        [Display(Name = "Facebook")]
        [StringLength(250, ErrorMessage = "El {0} debe tener {1} caracteres de longitud máximo. Ejm: @sistemasadrianzen")]
        public string V_FACEBOOK { get; set; }

        public int idplan { get; set; }

        public ConfiguracionTiendaEN _ConfiguracionTiendaEN { get; set; }

    }
    public class ConfiguracionTiendaEN
    {
        public int diasplazodevolucion { get; set; }
        public string rangoatencion { get; set; }
        public int diasplazoatencion { get; set; }

    }
    public class DashboardEN : BaseEN
    {
        public int I_CODIGO_TIENDA { get; set; }
        public int CANT_PEDIDO_PEND { get; set; }
        public double TOT_PEDIDO_PEND { get; set; }
        public int PRODUCTOS_BAJO_STOCK { get; set; }
        public int CANT_VEND_MES_ACT { get; set; }
        public double TOT_VEND_MES_ACT { get; set; }
        public List<RptTransacciones> listRptVentas { get; set; }
        public List<RptTransacciones> listRptPedidosPendientes { get; set; }
        public List<RptFormaPago> listRptFormaPago { get; set; }
        public List<RptEstadoCompra> listRptEstadoCompra { get; set; }
        public List<RptTransacciones> listRptVentasDiarias { get; set; }
    }

    public class RptTransacciones : BaseEN
    {
        public int? anio { get; set; }
        public string mes { get; set; }
        public string estadoPedido { get; set; }
        public decimal total { get; set; }
        public string fecha { get; set; }
    }
    public class RptFormaPago : BaseEN
    {
        public int? anio { get; set; }
        public string mes { get; set; }
        public int cant { get; set; }
        public string formapago { get; set; }
    }
    public class RptEstadoCompra : BaseEN
    {
        public int porcentaje { get; set; }
        public int idestadocompra { get; set; }
        public string estadocompra { get; set; }
    }
}
