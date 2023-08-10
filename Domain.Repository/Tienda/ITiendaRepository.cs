using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Tienda
{
    public interface ITiendaRepository
    {
        List<TiendaEN> SelectAll(TiendaEN item);
        TiendaEN Select(TiendaEN item);
        long Insert(TiendaEN item);
        void Delete(TiendaEN item);
        void Update(TiendaEN item);
        DashboardEN SelectDashboard(DashboardEN item);
        List<RptTransacciones> SelectRptVentas(RptTransacciones item);
        List<RptFormaPago> SelectRptFormaPago(RptFormaPago item);
        List<RptEstadoCompra> SelectRptEstadoCompra(RptEstadoCompra item);
        List<RptTransacciones> SelectRptVentasDiario(RptTransacciones item);
    }
}
