using Domain.EntitiesLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.ProductoCaracteristica
{
    public interface IProductoCaracteristicaRepository
    {
        List<ProductoCaracteristicaEL> getCaracteristicaPorProducto(long codigoProducto);
        long Insert(ProductoCaracteristicaEL item);
    }
}
