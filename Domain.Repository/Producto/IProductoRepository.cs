using Domain.Entities;
using Domain.EntitiesLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Producto
{
    public interface IProductoRepository
    {
        List<ProductoEN> SelectAll(ProductoEN item);
        List<ProductoEN> BuscarProductos(ProductoEN item);
        long Insert(ProductoEN item);
        void Delete(ProductoEN item);
        void Update(ProductoEN item);
        ProductoEL SelectDetalle(ProductoEN item);
        List<ProductoEL> getProductoDetalleCompra(long codigoCompra);
    }
}
