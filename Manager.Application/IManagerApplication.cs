using Domain.Entities;
using Domain.Entities.EmailMarketing;
using Domain.EntitiesLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Application
{
    public interface IManagerApplication : IDisposable
    {
        #region Product

        List<ProductoEN> BuscarProductos(ProductoEN _ProductEN);
        List<ProductoEN> SelectAllProduct(ProductoEN item);
        ResponseEL InsertProduct(ProductoEL _ProductEL);
        ResponseEL DeleteProduct(ProductoEN _ProductEN);
        void UpdateProduct(ProductoEN _ProductEN);
        ProductoEL getProductoDetalle(long codigoProducto);

        #endregion

        #region ProductoCaracteristica

        List<ProductoCaracteristicaEL> getCaracteristicaPorProducto(long codigoProducto);

        #endregion

        #region Perfil

        List<PerfilEN> SelectAllPerfil();
        PerfilEN SelectPerfilById(int id);

        #endregion

        #region CarritoCompra

        ResponseEL InsertarCarritoCompra(CarritoCompraEL collection, bool produccion);
        ResponseEL InsertarCarritoCompraDetalleTemp(DetalleCompraTempEN item);
        List<CarritoCompraEL> getCarritoCompraAll(TiendaEN item);
        List<DetalleCompraTempEN> getDetalleCompraTemp(DetalleCompraTempEN item);
        CarritoDetalle getCarritoCompraDetalle(int codigoCompra);
        List<CarritoCompraEL> getCarritoCompraUsuarioTienda(CarritoCompraEN obj);
        ResponseEL actualizarEstadoCompra(CarritoCompraEN item);
        string getProductosDetalleHTML(int idcodigocompra);
        #endregion

        #region EmailMarketing

        #region DocumentoHtml

        void InsertDocumentoHtml(DocumentoHtmlEN item);
        DocumentoHtmlEN SelectDocumentoHtml(DocumentoHtmlEN item);

        #endregion

        #endregion

        #region Categorias
        List<CategoriaEN> getCategorias(CategoriaEN item);
        void setCategorias(CategoriaEN item);

        #endregion
        
        #region Tienda
        List<TiendaEN> getTiendaAll(TiendaEN item);
        TiendaEN getTienda(TiendaEN item);
        void insertarTienda(TiendaEN item);
        void sendEmailConfirmacionTienda(string nombre, string urlbase);
        DashboardEN getDashboard(DashboardEN item);
        List<RptTransacciones> SelectRptVentas(RptTransacciones item);
        List<RptFormaPago> SelectRptFormaPago(RptFormaPago item);
        List<RptEstadoCompra> SelectRptEstadoCompra(RptEstadoCompra item);
        List<RptTransacciones> SelectRptVentasDiario(RptTransacciones item);
	    #endregion

        #region Usuario

        long InsertarUsuarioProviderAuth(UsuarioEN item);
        UsuarioEN getUsuario(UsuarioEN item);
        void updateUsuario(UsuarioEN item);
        UsuarioEN getUsuarioTienda(UsuarioEN item);
        tokenUsuarioEN createToken(tokenUsuarioEN item);
        void sendEmailforgetPassword(string email, string url);

        #endregion

        #region Planes
        List<PlanesEN> SelectPlanesAll(PlanesEN item);
        PlanesEN SelectPlanes(PlanesEN item);
        void InsertPlanes(PlanesEN item);
        void DeletePlanes(PlanesEN item);
        void UpdatePlanes(PlanesEN item);
        #endregion

        #region PlanTienda
        List<PlanTiendaEN> SelectPlanTiendaAll(PlanTiendaEN item);
        PlanTiendaEN SelectPlanTienda(PlanTiendaEN item);
        ResponseEL InsertPlanTienda(PlanTiendaEN item);
        void DeletePlanTienda(PlanTiendaEN item);
        ResponseEL UpdatePlanTienda(PlanTiendaEN item, bool produccion, string token_payment);
        #endregion

        #region ParametroValor

        ParametroValorEN SelectParametroValor(ParametroValorEN item);

        #endregion

    }
}
