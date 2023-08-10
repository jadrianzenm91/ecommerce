using Domain.Entities;
using Domain.EntitiesLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.CarritoCompra
{
    public interface ICarritoCompraRepository
    {
        CarritoCompraEN InsertarCarritoCompra(CarritoCompraEN item, string token);
        long updateCarritoCompra(CarritoCompraEN item);
        void InsertarCarritoCompraDetalle(DetalleCompraEN item);
        DetalleCompraTempEN InsertarCarritoCompraDetalleTemp(DetalleCompraTempEN item);
        CarritoCompraEL getCarritoCompra(int codigoCompra);
        List<CarritoCompraEL> getCarritoCompraUsuarioTienda(CarritoCompraEN obj);
        List<DetalleCompraTempEN> getDetalleCompraTemp(DetalleCompraTempEN item);
        List<CarritoCompraEL> getCarritoCompraAll(TiendaEN item);
        void actualizarEstadoCompra(CarritoCompraEN item);
    }
}
