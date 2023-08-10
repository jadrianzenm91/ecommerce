using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductoEN: BaseEN
    {
        public ProductoEN()
        {
           this._CategoriaEN = new CategoriaEN();
           this._listImagenProductoEN = new List<ImagenProductoEN>();
        }

        public long I_CODIGO_PRODUCTO { get; set; }
        public int I_CODIGO_CATEGORIA { get; set; }
        public string V_PRODUCTO { get; set; }
        public int I_CODIGO_TIENDA { get; set; }
        public decimal N_PRECIO { get; set; }
        public int I_STOCK { get; set; }
        public string V_CODIGO_BARRA { get; set; }
        public string V_DESCRIPCION { get; set; }
        public string V_URL_PRINCIPAL { get; set; }
        public string V_CODIGO_PRODUCTO
        {
            get
            {
                if (I_CODIGO_PRODUCTO == 0)
                {
                    return string.Empty;
                }
                else
                {
                    return Infrastructure.CrossCutting.Encrypting.EncryptKey(I_CODIGO_PRODUCTO.ToString());
                }
            }
        }
        public List<ImagenProductoEN> _listImagenProductoEN { get; set; }
        public CategoriaEN _CategoriaEN { get; set; }
    }
}
