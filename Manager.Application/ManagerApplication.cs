using Domain.Entities;
using Domain.Entities.EmailMarketing;
using Domain.EntitiesLogic;
using Domain.Repository.CarritoCompra;
using Domain.Repository.Categoria;
using Domain.Repository.EmailMarketing.DocumentoHtml;
using Domain.Repository.ImagenProducto;
using Domain.Repository.ParametroValor;
using Domain.Repository.Perfil;
using Domain.Repository.Planes;
using Domain.Repository.PlanTienda;
using Domain.Repository.Producto;
using Domain.Repository.ProductoCaracteristica;
using Domain.Repository.Tienda;
using Domain.Repository.Usuario;
using Infraestruture.CrossCutting;
using Infrastructure.CrossCutting;
using Manager.Master;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Manager.Application
{
    public class ManagerApplication : IManagerApplication
    {
        readonly IProductoRepository _IProductoRepository;
        readonly IDocumentoHtmlRepository _IDocumentoHtmlRepository;
        readonly IProductoCaracteristicaRepository _IProductoCaracteristicaRepository;
        readonly IPerfilRepository _IPerfilRepository;
        readonly ICarritoCompraRepository _ICarritoCompraRepository;
        readonly IUsuarioRepository _IUsuarioRepository;
        readonly ICategoriaRepository _ICategoriaRepository;
        readonly IImagenProductoRepository _IImagenProductoRepository;
        readonly ITiendaRepository _ITiendaRepository;
        readonly IPlanesRepository _IPlanesRepository;
        readonly IPlanTiendaRepository _IPlanTiendaRepository;
        readonly IParametroValorRepository _IParametroValorRepository;

        public ManagerApplication()
        {
            _IProductoRepository = new ProductoRepository();
            _IDocumentoHtmlRepository = new DocumentoHtmlRepository();
            _IProductoCaracteristicaRepository = new ProductoCaracteristicaRepository();
            _IPerfilRepository = new PerfilRepository();
            _ICarritoCompraRepository = new CarritoCompraRepository();
            _IUsuarioRepository = new UsuarioRepository();
            _ICategoriaRepository = new CategoriaRepository();
            _IImagenProductoRepository = new ImagenProductoRepository();
            _ITiendaRepository = new TiendaRepository();
            _IPlanesRepository = new PlanesRepository();
            _IPlanTiendaRepository = new PlanTiendaRepository();
            _IParametroValorRepository = new ParametroValorRepository();
        }

        #region Producto
        public List<ProductoEN> BuscarProductos(ProductoEN _ProductEN)
        {
            return _IProductoRepository.BuscarProductos(_ProductEN);
        }
        public List<ProductoEN> SelectAllProduct(ProductoEN item)
        {
            return _IProductoRepository.SelectAll(item);
        }
        public ResponseEL InsertProduct(ProductoEL item)
        {
            ResponseEL result = new ResponseEL();
            try
            {
                using (var scope = new TransactionScope())
                {
                    
                    if (item.I_CODIGO_PRODUCTO > 0)
                    {
                        UpdateProduct(item);
                    }
                    else
                    {
                        item.I_CODIGO_PRODUCTO = _IProductoRepository.Insert(item);
                    }
                    _IImagenProductoRepository.Delete(new ImagenProductoEN()
                    {
                        I_CODIGO_PRODUCTO = item.I_CODIGO_PRODUCTO
                    });
                    foreach (var item2 in item._listImagenProductoEN)
                    {
                        item2.V_USER_CREATE = item.V_USER_CREATE;
                        item2.I_CODIGO_PRODUCTO = item.I_CODIGO_PRODUCTO;
                        _IImagenProductoRepository.Insert(item2);
                    }
                    foreach (var item3 in item._listProductoCaracteristicaEL)
                    {
                        item3.V_USER_CREATE = item.V_USER_CREATE;
                        item3.I_CODIGO_PRODUCTO = item.I_CODIGO_PRODUCTO;
                        _IProductoCaracteristicaRepository.Insert(item3);
                    }
                    scope.Complete();
                    result.message = Constantes.Mensajes.Success;
                    result.success = true;
                }
            }
            catch (Exception ex)
            {
                result.stacktrace = "Message: " + ex.Message.ToString() + ". StackTrace:" + ex.StackTrace.ToString();
                result.message = Constantes.Mensajes.Error;
                result.success = false;
            }
            return result;
        }
        public ResponseEL DeleteProduct(ProductoEN _ProductEN)
        {
            ResponseEL result = new ResponseEL();
            try
            {
                _IProductoRepository.Delete(_ProductEN);
                result.message = Constantes.Mensajes.Success;
                result.success = true;
            }
            catch (Exception ex)
            {
                result.stacktrace = "Message: " + ex.Message.ToString() + ". StackTrace:" + ex.StackTrace.ToString();
                result.message = Constantes.Mensajes.Error;
                result.success = false;
            }
            return result;
            
        }
        public void UpdateProduct(ProductoEN _ProductEN)
        {
            _IProductoRepository.Update(_ProductEN);
        }

        public ProductoEL getProductoDetalle(long codigoProducto)
        {
            ProductoEL _result = new ProductoEL();
            
            _result = _IProductoRepository.SelectDetalle(new ProductoEL()
            {
                I_CODIGO_PRODUCTO = codigoProducto
            });
            _result._listProductoCaracteristicaEL = getCaracteristicaPorProducto(codigoProducto);
            _result._listImagenProductoEN = _IImagenProductoRepository.SelectByProducto(codigoProducto);
            return _result;
        }

        #endregion

        #region ProductoCaracteristica

        public List<ProductoCaracteristicaEL> getCaracteristicaPorProducto(long codigoProducto)
        {
            return _IProductoCaracteristicaRepository.getCaracteristicaPorProducto(codigoProducto);
        }

        #endregion

        #region Perfil

        public List<PerfilEN> SelectAllPerfil()
        {
            return _IPerfilRepository.SelectAllPerfil();
        }

        public PerfilEN SelectPerfilById(int id)
        {
            return _IPerfilRepository.SelectPerfilById(id);
        }

        #endregion

        #region CarritoCompra

        public ResponseEL InsertarCarritoCompra(CarritoCompraEL collection, bool produccion)
        {
            ResponseEL response = new ResponseEL();
            CarritoCompraEN _carritoCompraEN = new CarritoCompraEN();
            try 
	        {	            
                using (var scope = new TransactionScope())
                {
                    _carritoCompraEN = _ICarritoCompraRepository.InsertarCarritoCompra(new CarritoCompraEN()
                            {
                                NOMBRE_CONTACTO = collection.nombres,
                                CELULAR_CONTACTO = collection.celular,
                                EMAIL = collection.email,
                                V_DIRECCION_ENTREGA = collection.direccion,
                                V_USER_CREATE = collection.usuario,
                                I_CODIGO_TIENDA = collection.idtienda,
                                I_CODIGO_FORMA_PAGO = collection.codigoFormaPago,
                                V_DNI = collection.dni,
                                latitude = collection.latitude,
                                longitude = collection.longitude
                            }, collection.token);
                    _carritoCompraEN.codigoCompraEncry = Encrypting.EncryptKey(_carritoCompraEN.I_CODIGO_COMPRA.ToString());
                    response.data = _carritoCompraEN;
                        if (collection.codigoFormaPago.Equals(Constantes.FormaPago.PagoOnline))
                        {
                            #region PaymentCard
                          
                            Dictionary<string, object> metadata = new Dictionary<string, object>
			                {
				                {"order_id", _carritoCompraEN.I_CODIGO_COMPRA},
                                {"client_name", collection.nombres},
                                {"total_amount", _carritoCompraEN.N_TOTAL}
			                };
                            
                            PaymentEN _PaymentEN = new PaymentEN();
                            _PaymentEN.description = _carritoCompraEN.tiendaEN.V_RAZON_SOCIAL;
                            _PaymentEN.amount = (int)(_carritoCompraEN.N_TOTAL * 100);
                            _PaymentEN.email = collection.email;
                            _PaymentEN.metadata = metadata;
                            _PaymentEN.token_payment = collection.token_payment;

                            PaymentResponseEN _PaymentResponseEN = this.procesarPagoCulqi(produccion, _PaymentEN);
                            if (_PaymentResponseEN.success)
                            {
                                _ICarritoCompraRepository.updateCarritoCompra(new CarritoCompraEN()
                                {
                                    I_CODIGO_COMPRA = _carritoCompraEN.I_CODIGO_COMPRA,
                                    V_TAJETA_PAGO = _PaymentResponseEN.tarjeta
                                });
                                response.message = _PaymentResponseEN.message;
                                response.success = true;
                                response.type = Constantes.TypeResult.Success;
                            }
                            else
                            {
                                response.message = _PaymentResponseEN.message;
                                response.type = Constantes.TypeResult.Validation;
                                response.success = false;
                                scope.Dispose();
                            }
                            #endregion

                        }
                        else
                        {
                            response.message = Constantes.Mensajes.ReservaExitosa;
                            response.success = true;
                            response.type = Constantes.TypeResult.Success;
                        }
                        scope.Complete();
                }
                sendEmailCarritoCompra(_carritoCompraEN.I_CODIGO_COMPRA);
                
	        }
	        catch (Exception ex)
	        {
                response.message = Constantes.Mensajes.ErrorServer;
		        response.stacktrace = ex.StackTrace;
                response.success = false;
                response.type = Constantes.TypeResult.Exception;
	        }

            return response;
        }
        public ResponseEL InsertarCarritoCompraDetalleTemp(DetalleCompraTempEN item)
        {
            ResponseEL response = new ResponseEL();
            try
            {
                item = _ICarritoCompraRepository.InsertarCarritoCompraDetalleTemp(item);
                item.idEnc = Encrypting.EncryptKey(item.id.ToString());
                response.message = string.Empty;
                response.success = true;
                response.type = Constantes.TypeResult.Success;
                response.data = item;
            }
            catch (Exception ex)
            {
                response.stacktrace = ex.StackTrace;
                response.message = Constantes.Mensajes.ErrorServer;
                response.success = true;
                response.type = Constantes.TypeResult.Success;
                response.data = item;
            }
            return response;
        }
        public List<DetalleCompraTempEN> getDetalleCompraTemp(DetalleCompraTempEN item)
        {
            var result = _ICarritoCompraRepository.getDetalleCompraTemp(item);
            foreach (var i in result)
	        {
                i.idprodEnc = Encrypting.EncryptKey(i.idprod.ToString());
	        }
            return result;
        } 
        public List<CarritoCompraEL> getCarritoCompraAll(TiendaEN item)
        {
            var _result = new List<CarritoCompraEL>();
            _result = _ICarritoCompraRepository.getCarritoCompraAll(item);
            return _result;
        }
        public CarritoDetalle getCarritoCompraDetalle(int codigoCompra)
        {
            CarritoDetalle _CarritoDetalle = new CarritoDetalle();

            _CarritoDetalle.carritoCompraEL = _ICarritoCompraRepository.getCarritoCompra(codigoCompra);
            _CarritoDetalle.listProductos  = _IProductoRepository.getProductoDetalleCompra(codigoCompra);

            foreach (var item in _CarritoDetalle.listProductos)
            {
                item._listProductoCaracteristicaEL = getCaracteristicaPorProducto(item.I_CODIGO_PRODUCTO);
                item._listImagenProductoEN = _IImagenProductoRepository.SelectByProducto(item.I_CODIGO_PRODUCTO);
            }

            return _CarritoDetalle;
        }
        public List<CarritoCompraEL> getCarritoCompraUsuarioTienda(CarritoCompraEN obj)
        {
            List<CarritoCompraEL> _ListCarritoDetalle = new List<CarritoCompraEL>();

            _ListCarritoDetalle = _ICarritoCompraRepository.getCarritoCompraUsuarioTienda(obj);
            foreach (var item in _ListCarritoDetalle)
            {
                item.listProductos = _IProductoRepository.getProductoDetalleCompra(item.codigoCompra);
                foreach (var item2 in item.listProductos)
                {
                    item2._listImagenProductoEN = _IImagenProductoRepository.SelectByProducto(item2.I_CODIGO_PRODUCTO);
                }
            }

            return _ListCarritoDetalle;
        }
        public ResponseEL actualizarEstadoCompra(CarritoCompraEN item)
        {
            ResponseEL result = new ResponseEL();
            using (var scope = new TransactionScope())
            {
                try
                {
                    _ICarritoCompraRepository.actualizarEstadoCompra(item);
                    sendEmailCarritoCompra(item.I_CODIGO_COMPRA);
                    result.data = item;
                    result.success = true;
                    result.message = Constantes.Mensajes.Success;
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    result.data = item;
                    result.stacktrace = "Message: " + ex.Message.ToString() + ". StackTrace:" + ex.StackTrace.ToString();
                    result.message = Constantes.Mensajes.Error;
                    result.success = false;
                    scope.Dispose();
                }
            }
            return result;
        }

        public void sendEmailCarritoCompra(long idcodigocompra)
        {
            string bodyFormat = Util.bodyEmailComprobante();
            CarritoDetalle item = this.getCarritoCompraDetalle((int)idcodigocompra);
            var usuario = this.getUsuario(new UsuarioEN() { V_USUARIO = item.carritoCompraEL.usuario });

            string body = String.Format(bodyFormat, 
                usuario.V_NOMBRES, 
                usuario.V_NOMBRES + ' ' + usuario.V_APELLIDOS,
                item.carritoCompraEL.codigoCompra, 
                usuario.V_DNI,
                item.carritoCompraEL.fechaCompra,
                item.carritoCompraEL.codigoFormaPago.Equals(Constantes.FormaPago.PagoOnline) ? "Tarjeta: " + item.carritoCompraEL.tarjeta : Constantes.FormaPago.PagoContraEntrega,
                DataConvert.ObjectDecimalToStringFormatMiles(item.carritoCompraEL.total),
                DataConvert.ObjectDecimalToStringFormatMiles(item.carritoCompraEL.costoEnvio),
                DataConvert.ObjectDecimalToStringFormatMiles(item.carritoCompraEL.total),
                item.carritoCompraEL.fechaEntregaAprox,
                item.carritoCompraEL.direccion.ToUpper(),//{10}
                item.carritoCompraEL.nombres,
                item.carritoCompraEL.dni,
                getProductosDetalleHTML((int)idcodigocompra),
                item.carritoCompraEL.razonsocial,//{14}
                item.carritoCompraEL.estadoCompra,
                item.carritoCompraEL.mensajeEstadoCompra,
                Util.getImageEstadoCompra(item.carritoCompraEL.codigoEstadoCompra),
                item.carritoCompraEL.logotienda,//{18}
                item.carritoCompraEL.telefonotienda,//{19}
                item.carritoCompraEL.facebook,//{20}
                item.carritoCompraEL.emailtienda,//{21}
                item.carritoCompraEL.webtienda,//{22}
                item.carritoCompraEL.tienda//{23}
                );
            Util.sendEmailHtml(item.carritoCompraEL.email, body, "Estado de su compra");
        }
        
        public string getProductosDetalleHTML(int idcodigocompra)
        {
            var detalleProductos = _IProductoRepository.getProductoDetalleCompra(idcodigocompra);
            string detalleHTML = string.Empty;
            foreach (var item in detalleProductos)
            {
                string tr = @"<tr>
                                    <td style=""width:6.875rem;margin:0px;padding:0.8rem 0;color:#333333;text-align:center;color:#333333;font-size:13px;line-height:15px;font-weight:normal;vertical-align:middle;border-bottom:solid 1px #d3d3d3""><img src=""{0}"" style=""vertical-align:midddle;outline:none;text-decoration:none;border:none;width:110px"" class=""CToWUd""></td>
                                    <td style=""width:17.75rem;margin:0px;padding:0.8rem;color:#333333;text-align:left;color:#333333;font-size:13px;line-height:15px;font-weight:normal;vertical-align:middle;border-bottom:solid 1px #d3d3d3"">{1}</td>
                                    <td style=""width:4.125rem;margin:0px;padding:0.8rem 0;color:#333333;text-align:center;color:#333333;font-size:13px;line-height:15px;font-weight:normal;vertical-align:middle;border-bottom:solid 1px #d3d3d3"">S/ {2}</td>
                                    <td style=""width:4.125rem;margin:0px;padding:0.8rem 0;color:#333333;text-align:center;color:#333333;font-size:13px;line-height:15px;font-weight:normal;vertical-align:middle;border-bottom:solid 1px #d3d3d3"">{3}</td>
                                    <td style=""width:6.25rem;margin:0px;padding:0.8rem 0;color:#333333;text-align:center;color:#333333;font-size:13px;line-height:15px;font-weight:normal;vertical-align:middle;border-bottom:solid 1px #d3d3d3""> <strong>S/ {4}</strong></td>
                               </tr>";
                detalleHTML += String.Format(tr, 
                        item.V_URL_PRINCIPAL, 
                        item.V_PRODUCTO,
                        DataConvert.ObjectDecimalToStringFormatMiles(item.N_PRECIO),
                        item._detalleCompraEN.cantidad,
                        DataConvert.ObjectDecimalToStringFormatMiles(item._detalleCompraEN.total));
            }
            
            return detalleHTML;
        }

        
        #endregion

        #region Usuario

        public long InsertarUsuarioProviderAuth(UsuarioEN item)
        {
            long IdUsuario = 0;
            using (var scope = new TransactionScope())
            {
                item.V_USER_CREATE = Constantes.Login.UserCreate.ToString();
                IdUsuario = _IUsuarioRepository.InsertProviderAuth(item);

                scope.Complete();
            }

            return IdUsuario;

        }

        public UsuarioEN getUsuario(UsuarioEN item)
        {
            return _IUsuarioRepository.Select(item);
        }

        public UsuarioEN getUsuarioTienda(UsuarioEN item)
        {
            return _IUsuarioRepository.getUsuarioTienda(item);
        }

        public tokenUsuarioEN createToken(tokenUsuarioEN item)
        {
            return _IUsuarioRepository.createToken(item);
        }

        public void sendEmailforgetPassword(string email, string url)
        {
            #region bodyHTML

            string bodyHTML =
                @"<div bgcolor=""#f5f6f6"">
       
      <table width=""100%"" align=""center"" bgcolor=""#f5f6f6"" cellpadding=""0"" cellspacing=""0"" border=""0"">
         <tbody><tr>
            <td valign=""top"" width=""100%"">
               <table align=""center"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                  <tbody><tr>
                     <td width=""100%"">
                         
                        <table bgcolor=""#f5f6f6"" width=""600"" align=""center"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                           <tbody><tr>
                              <td width=""100%"" height=""40""> &nbsp;</td>
                           </tr>
                        </tbody></table>
                           
                     </td>
                  </tr>
               </tbody></table>
            </td>
         </tr>
      </tbody></table>
        
      <table width=""100%"" align=""center"" bgcolor=""#f5f6f6"" cellpadding=""0"" cellspacing=""0"" border=""0"">
         
      </table>
        
      <table width=""100%"" align=""center"" bgcolor=""#f5f6f6"" cellpadding=""0"" cellspacing=""0"" border=""0"">
         <tbody><tr>
            <td valign=""top"" width=""100%"">
               <table align=""center"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                  <tbody><tr>
                     <td width=""100%"">
                        <table align=""center"" width=""620"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                           <tbody><tr>
                              <td align=""center"" style=""margin:0;font-size:14px;padding:8px;color:#677b82;font-family:'PT Sans',Helvetica,Arial,sans-serif;line-height:0""> <span> <img src=""https://firebasestorage.googleapis.com/v0/b/e-commerce-c6d82.appspot.com/o/logo%2Flogo-sisadri-small.png?alt=media&token=fc497a03-eba5-4caf-ae69-1731b1f233b7"" border=""0"" style=""display:block"" class=""CToWUd""> </span> </td>
                           </tr>
                        </tbody></table>
                         
                        <table bgcolor=""#ffffff"" width=""600"" align=""center"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                           <tbody><tr>
                              <td width=""100%"" height=""30""> &nbsp;</td>
                           </tr>
                        </tbody></table>
                         
                     </td>
                  </tr>
               </tbody></table>
            </td>
         </tr>
      </tbody></table>
        
      <table width=""100%"" align=""center"" bgcolor=""#f5f6f6"" cellpadding=""0"" cellspacing=""0"" border=""0"">
         <tbody><tr>
            <td valign=""top"" width=""100%"">
               <table align=""center"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                  <tbody><tr>
                     <td width=""100%"">
                        <table width=""600"" bgcolor=""#ffffff"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                           <tbody><tr>
                              <td width=""100%"">
                                 <table width=""600"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                                    <tbody><tr>
                                       <td width=""30""> </td>
                                       <td width=""540"">
                                          <table align=""left"" width=""540"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""border-collapse:collapse"">
                                              
                                             <tbody><tr>
                                                <td align=""center"" style=""padding:0px;padding-bottom:50px;font-family:Arial,sans-serif;color:#000;font-size:25px;line-height:40px;width:100%"">
                                                   <span>¡Hola {0}!</span>
                                                </td>
                                             </tr>
                                             <tr>
                                                <td align=""center"" style=""padding:0px;padding-bottom:30px;font-family:Arial,sans-serif;color:#767474;font-style:italic;font-size:20px;line-height:40px"">
                                                   <span>Solicitaste reestablecer tu contraseña</span>
                                                </td>
                                             </tr>
                                             <tr>
                                                <td align=""center"" style=""padding:0px;padding-bottom:50px;font-family:Arial,sans-serif;color:#000;font-size:16px;line-height:40px"">
                                                   <span>Para <span class=""il"">resetear</span> <span class=""il"">tu</span> <span class=""il"">contraseña</span>, haz <a href=""{1}"" style=""color:blue;text-decoration:none;font-weight:bold"" target=""_blank"">click aquí</a></span>
                                                </td>
                                             </tr>
                                             <tr>
                                             </tr><tr style=""font-family:Arial,sans-serif;font-size:12px;color:#000"">
                                                <td style=""display:block"">
                                                <br>
                                                  <div style=""padding-bottom:6px"">
                                                    <br>
                                                    <br>
                                                    ¿Tienes dudas?, comunícate con nosotros mediante:
                                                  </div>
                                                  <table cellpadding=""0"" cellspacing=""0"" border=""0"">
                                                    <tbody>
                                                      <tr>
                                                        <td>
                                                          <img src=""https://firebasestorage.googleapis.com/v0/b/e-commerce-c6d82.appspot.com/o/logo%2Ficon-planeta.png?alt=media&token=334f30e0-46a4-4974-977e-708687519e17"" width=""12"" alt="""" class=""CToWUd"">
                                                        </td>
                                                        <td style=""padding-left:8px"">
                                                            Formulario web: clic <a href=""http://sisadri.pe"" style=""font-style:normal;text-decoration:none;color:#000"" target=""_blank"" data-saferedirecturl=""https://www.google.com/url?q=http://email.yachay.pe/c/eJw9jUEOgyAQRU8jSzIDAmXBoqb1HuMI1UbFWBJjT19WzX-bt3l_DB5TIjEHBQrAKItOozYSZd_Co-86Y-v07ambFi7iiS65RzGFqBR5DziwbZ1mcC4N1oxJGcToGcQSplL2T6Pvjeor53nKf6A6560QlyyO8KbxmGn7xm31WH9eK82L5Lz-AH-DLxU&amp;source=gmail&amp;ust=1593405258418000&amp;usg=AFQjCNHrOnNjfi7qfwoobzdH9XU0YIUg8w"">aquí</a>
                                                        </td>
                                                      </tr>
                                                    </tbody>
                                                  </table>
                                                  <table>
                                                    <tbody>
                                                      <tr>
                                                        <td>
                                                          <img src=""https://firebasestorage.googleapis.com/v0/b/e-commerce-c6d82.appspot.com/o/logo%2Ficon-telefono.png?alt=media&token=806c4f1a-4605-42a5-b533-0e6fad5c376c"" width=""12"" alt="""" class=""CToWUd"">
                                                        </td>
                                                        <td style=""padding-left:8px"">
                                                            Central telefónica: <span style=""color:#000;font-weight:bold"">702 2000</span>
                                                        </td>
                                                      </tr>
                                                    </tbody>
                                                  </table>
                                                  <br>
                                                
                                             </td></tr>
                                              <tr><td align=""left"" style=""margin:0;font-size:10px;color:#c4c4c4;font-family:Arial,sans-serif;line-height:12px;display:block"">
                                                 <br>
                                             <span>Le agradecemos tener en cuenta que esta es una comunicación automática de SISADRI, por tal motivo le pedimos no responder a esta dirección electrónica.
                                             <br>La libre distribución de este correo electrónico está autorizada por tratarse de propósitos de información.
                                             </span> 
                                             </td>
                                          </tr>
                                              
                                          </tbody></table>
                                       </td>
                                       <td width=""30""> </td>
                                    </tr>
                                 </tbody></table>
                              </td>
                           </tr>
                        </tbody></table>
                         
                        <table bgcolor=""#ffffff"" width=""600"" align=""center"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                           <tbody><tr>
                              <td width=""100%"" height=""40""> &nbsp;</td>
                           </tr>
                        </tbody></table>
                         
                     </td>
                  </tr>
               </tbody></table>
            </td>
         </tr>
      </tbody></table>
        
        
      <table width=""100%"" align=""center"" bgcolor=""#f5f6f6"" cellpadding=""0"" cellspacing=""0"" border=""0"">
         <tbody><tr>
            <td align=""center"" valign=""top"" width=""100%"">
               <table align=""center"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                  <tbody><tr>
                     <td width=""100%"">
                        <table width=""600"" bgcolor=""#f5f6f6"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                           <tbody><tr>
                              <td width=""100%"">
                                 <table width=""600"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                                    <tbody><tr>
                                       <td width=""540"">
                                          <table align=""left"" width=""540"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""border-collapse:collapse"">
                                             <tbody><tr>
                                                <td align=""center"" valign=""top"" style=""padding:0px"">
                                                   <table border=""0"" align=""center"" cellpadding=""0"" cellspacing=""0"" bgcolor=""#ff5b00"" style=""margin:0"">
                                                      <tbody><tr>
                                                         <td align=""center"" valign=""middle"" bgcolor=""#f5f6f6"" style=""padding:0px;padding-bottom:30px;font-size:13px;font-style:normal;color:#677b82;font-family:'PT Sans',Helvetica,Arial,sans-serif;line-height:0""> <img src=""https://ci5.googleusercontent.com/proxy/nNHzPtolq4WOq6Ndq_v4M6EqR517UpTC5qZPIp0VSjwCStxMidK3vfve1pCAUOuBu_HxlUU5otn2HEPIHWfhRajuUIw-oPtsrCm-dcq6FxJtfA=s0-d-e1-ft#https://www.yachay.pe/img/mailling/renovacion/footer-teaser.png"" width=""600"" height=""10"" border=""0"" style=""display:block"" class=""CToWUd""> </td>
                                                      </tr>
                                                   </tbody></table>
                                                </td>
                                             </tr>
                                          </tbody></table>
                                       </td>
                                    </tr>
                                 </tbody></table>
                              </td>
                           </tr>
                        </tbody></table>
                     </td>
                  </tr>
               </tbody></table>
            </td>
         </tr>
      </tbody></table>
      
      
   <img width=""1px"" height=""1px"" alt="""" src=""https://ci5.googleusercontent.com/proxy/uhujGXM7_rq52x1CO7Wymmz-ClTqVSregL8ZxmW4Z62r9u75KrG6HT-Pp4itsQLB_gsWxGgdi1FQLLBTP2UO4VP1VdoYTuwhV1Rnox3yU0WmNbpf8llVQbPZavRkXLnqfvU52gORm9oiiFkN17udYKp6CSxRKbp2srefhVb9Twb5zrnVPeYHUEjRM7RCcpDCm--hX46AFNh2fMfPbkhjvH_TE5H_Aw8tQR23kPuXgHdzaB9gTv9NaQjq0vz2BKJCrxdD=s0-d-e1-ft#http://email.yachay.pe/o/eJwNyzEOwyAMQNHTlBFhbDAMLFGbexgIDVVJq2zp6Yv-9IZfU4TWRPVkjTXGWQ-MgE6DXsnc12VxfobhgTcyl5RdLv3d1J7YIRE3n7m2HAvbLMWHCWGKIZA600vq2eX4bceIMPfnkP7W5TP-NVAhhw"" class=""CToWUd""></div>";
            #endregion

            string body = String.Format(@bodyHTML, email, url);
            Util.sendEmailHtml(email, body, "Recuperación de Contraseña");
        }

        public void updateUsuario(UsuarioEN item)
        {
            _IUsuarioRepository.Update(item);
        }

        #endregion
        
        #region EMailMarketing

        #region DocumentoHtml

        public void InsertDocumentoHtml(DocumentoHtmlEN item)
        {
            _IDocumentoHtmlRepository.InsertDocumentoHtml(item);
        }
        public DocumentoHtmlEN SelectDocumentoHtml(DocumentoHtmlEN item)
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion

        #region Categorias

        public List<CategoriaEN> getCategorias(CategoriaEN item)
        {
            return _ICategoriaRepository.SelectAll(item);
        }

        public void setCategorias(CategoriaEN item)
        {
            _ICategoriaRepository.Insert(item);
        }

        #endregion

        #region Tienda

        public List<TiendaEN> getTiendaAll(TiendaEN item)
        {
            return _ITiendaRepository.SelectAll(item);
        }
        public TiendaEN getTienda(TiendaEN item)
        {
            return _ITiendaRepository.Select(item);
        }
        public void insertarTienda(TiendaEN item)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var idtienda = _ITiendaRepository.Insert(item);
                    if (item.I_CODIGO_TIENDA == 0)//para tienda nuevas se registra un plan FREE
                    {
                        PlanTiendaEN _PlanTiendaEN = new PlanTiendaEN();
                        _PlanTiendaEN.idplantienda = 0;
                        _PlanTiendaEN.idtienda = (int)idtienda;
                        _PlanTiendaEN.V_USER = item.V_USER;

                        //inscripcíón
                        _PlanTiendaEN.idtipo = "U";
                        _PlanTiendaEN.idplan = Constantes.Conceptos.inscripcion;
                        this.InsertPlanTienda(_PlanTiendaEN);
                        //1er plan mensual
                        _PlanTiendaEN.idtipo = "M";
                        _PlanTiendaEN.idplan = Constantes.Conceptos.free;
                        this.InsertPlanTienda(_PlanTiendaEN);
                        //1er plan mensual
                        _PlanTiendaEN.idtipo = "M";
                        _PlanTiendaEN.idplan = Constantes.Conceptos.planbase;
                        this.InsertPlanTienda(_PlanTiendaEN);
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            

        }
        public void sendEmailConfirmacionTienda(string nombre, string urlbase)
        {
            #region bodyHTML

            string bodyHTML =
                @"<div bgcolor=""#f5f6f6"">
       
      <table width=""100%"" align=""center"" bgcolor=""#f5f6f6"" cellpadding=""0"" cellspacing=""0"" border=""0"">
         <tbody><tr>
            <td valign=""top"" width=""100%"">
               <table align=""center"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                  <tbody><tr>
                     <td width=""100%"">
                         
                        <table bgcolor=""#f5f6f6"" width=""600"" align=""center"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                           <tbody><tr>
                              <td width=""100%"" height=""40""> &nbsp;</td>
                           </tr>
                        </tbody></table>
                           
                     </td>
                  </tr>
               </tbody></table>
            </td>
         </tr>
      </tbody></table>
        
      <table width=""100%"" align=""center"" bgcolor=""#f5f6f6"" cellpadding=""0"" cellspacing=""0"" border=""0"">
         
      </table>
        
      <table width=""100%"" align=""center"" bgcolor=""#f5f6f6"" cellpadding=""0"" cellspacing=""0"" border=""0"">
         <tbody><tr>
            <td valign=""top"" width=""100%"">
               <table align=""center"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                  <tbody><tr>
                     <td width=""100%"">
                        <table align=""center"" width=""620"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                           <tbody><tr>
                              <td align=""center"" style=""margin:0;font-size:14px;padding:8px;color:#677b82;font-family:'PT Sans',Helvetica,Arial,sans-serif;line-height:0""> <span> <img src=""https://firebasestorage.googleapis.com/v0/b/e-commerce-c6d82.appspot.com/o/logo%2Flogo-sisadri-small.png?alt=media&token=fc497a03-eba5-4caf-ae69-1731b1f233b7"" border=""0"" style=""display:block"" class=""CToWUd""> </span> </td>
                           </tr>
                        </tbody></table>
                         
                        <table bgcolor=""#ffffff"" width=""600"" align=""center"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                           <tbody><tr>
                              <td width=""100%"" height=""30""> &nbsp;</td>
                           </tr>
                        </tbody></table>
                         
                     </td>
                  </tr>
               </tbody></table>
            </td>
         </tr>
      </tbody></table>
        
      <table width=""100%"" align=""center"" bgcolor=""#f5f6f6"" cellpadding=""0"" cellspacing=""0"" border=""0"">
         <tbody><tr>
            <td valign=""top"" width=""100%"">
               <table align=""center"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                  <tbody><tr>
                     <td width=""100%"">
                        <table width=""600"" bgcolor=""#ffffff"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                           <tbody><tr>
                              <td width=""100%"">
                                 <table width=""600"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                                    <tbody><tr>
                                       <td width=""30""> </td>
                                       <td width=""540"">
                                          <table align=""left"" width=""540"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""border-collapse:collapse"">
                                              
                                             <tbody><tr>
                                                <td align=""center"" style=""padding:0px;padding-bottom:50px;font-family:Arial,sans-serif;color:#000;font-size:25px;line-height:40px;width:100%"">
                                                   <span>¡Hola {0}!</span>
                                                </td>
                                             </tr>
                                             <tr>
                                                <td align=""center"" style=""padding:0px;padding-bottom:30px;font-family:Arial,sans-serif;color:#767474;font-style:italic;font-size:20px;line-height:40px"">
                                                   <span>Gracias por confiar en nosotros</span>
                                                </td>
                                             </tr>
                                             <tr>
                                                <td align=""center"" style=""padding:0px;padding-bottom:50px;font-family:Arial,sans-serif;color:#000;font-size:16px;line-height:40px"">
                                                   <span>Para <span class=""il"">confirmar</span> <span class=""il"">tu</span> <span class=""il"">registro</span>, haz <a href=""{1}"" style=""color:blue;text-decoration:none;font-weight:bold"" target=""_blank"">click aquí</a></span>
                                                </td>
                                             </tr>
                                             <tr>
                                             </tr><tr style=""font-family:Arial,sans-serif;font-size:12px;color:#000"">
                                                <td style=""display:block"">
                                                <br>
                                                  <div style=""padding-bottom:6px"">
                                                    <br>
                                                    <br>
                                                    ¿Tienes dudas?, comunícate con nosotros mediante:
                                                  </div>
                                                  <table cellpadding=""0"" cellspacing=""0"" border=""0"">
                                                    <tbody>
                                                      <tr>
                                                        <td>
                                                          <img src=""https://firebasestorage.googleapis.com/v0/b/e-commerce-c6d82.appspot.com/o/logo%2Ficon-planeta.png?alt=media&token=334f30e0-46a4-4974-977e-708687519e17"" width=""12"" alt="""" class=""CToWUd"">
                                                        </td>
                                                        <td style=""padding-left:8px"">
                                                            Formulario web: clic <a href=""http://sisadri.pe"" style=""font-style:normal;text-decoration:none;color:#000"" target=""_blank"" data-saferedirecturl=""https://www.google.com/url?q=http://email.yachay.pe/c/eJw9jUEOgyAQRU8jSzIDAmXBoqb1HuMI1UbFWBJjT19WzX-bt3l_DB5TIjEHBQrAKItOozYSZd_Co-86Y-v07ambFi7iiS65RzGFqBR5DziwbZ1mcC4N1oxJGcToGcQSplL2T6Pvjeor53nKf6A6560QlyyO8KbxmGn7xm31WH9eK82L5Lz-AH-DLxU&amp;source=gmail&amp;ust=1593405258418000&amp;usg=AFQjCNHrOnNjfi7qfwoobzdH9XU0YIUg8w"">aquí</a>
                                                        </td>
                                                      </tr>
                                                    </tbody>
                                                  </table>
                                                  <table>
                                                    <tbody>
                                                      <tr>
                                                        <td>
                                                          <img src=""https://firebasestorage.googleapis.com/v0/b/e-commerce-c6d82.appspot.com/o/logo%2Ficon-telefono.png?alt=media&token=806c4f1a-4605-42a5-b533-0e6fad5c376c"" width=""12"" alt="""" class=""CToWUd"">
                                                        </td>
                                                        <td style=""padding-left:8px"">
                                                            Central telefónica: <span style=""color:#000;font-weight:bold"">702 2000</span>
                                                        </td>
                                                      </tr>
                                                    </tbody>
                                                  </table>
                                                  <br>
                                                
                                             </td></tr>
                                              <tr><td align=""left"" style=""margin:0;font-size:10px;color:#c4c4c4;font-family:Arial,sans-serif;line-height:12px;display:block"">
                                                 <br>
                                             <span>Le agradecemos tener en cuenta que esta es una comunicación automática de SISADRI, por tal motivo le pedimos no responder a esta dirección electrónica.
                                             <br>La libre distribución de este correo electrónico está autorizada por tratarse de propósitos de información.
                                             </span> 
                                             </td>
                                          </tr>
                                              
                                          </tbody></table>
                                       </td>
                                       <td width=""30""> </td>
                                    </tr>
                                 </tbody></table>
                              </td>
                           </tr>
                        </tbody></table>
                         
                        <table bgcolor=""#ffffff"" width=""600"" align=""center"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                           <tbody><tr>
                              <td width=""100%"" height=""40""> &nbsp;</td>
                           </tr>
                        </tbody></table>
                         
                     </td>
                  </tr>
               </tbody></table>
            </td>
         </tr>
      </tbody></table>
        
        
      <table width=""100%"" align=""center"" bgcolor=""#f5f6f6"" cellpadding=""0"" cellspacing=""0"" border=""0"">
         <tbody><tr>
            <td align=""center"" valign=""top"" width=""100%"">
               <table align=""center"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                  <tbody><tr>
                     <td width=""100%"">
                        <table width=""600"" bgcolor=""#f5f6f6"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                           <tbody><tr>
                              <td width=""100%"">
                                 <table width=""600"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                                    <tbody><tr>
                                       <td width=""540"">
                                          <table align=""left"" width=""540"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""border-collapse:collapse"">
                                             <tbody><tr>
                                                <td align=""center"" valign=""top"" style=""padding:0px"">
                                                   <table border=""0"" align=""center"" cellpadding=""0"" cellspacing=""0"" bgcolor=""#ff5b00"" style=""margin:0"">
                                                      <tbody><tr>
                                                         <td align=""center"" valign=""middle"" bgcolor=""#f5f6f6"" style=""padding:0px;padding-bottom:30px;font-size:13px;font-style:normal;color:#677b82;font-family:'PT Sans',Helvetica,Arial,sans-serif;line-height:0""> <img src=""https://ci5.googleusercontent.com/proxy/nNHzPtolq4WOq6Ndq_v4M6EqR517UpTC5qZPIp0VSjwCStxMidK3vfve1pCAUOuBu_HxlUU5otn2HEPIHWfhRajuUIw-oPtsrCm-dcq6FxJtfA=s0-d-e1-ft#https://www.yachay.pe/img/mailling/renovacion/footer-teaser.png"" width=""600"" height=""10"" border=""0"" style=""display:block"" class=""CToWUd""> </td>
                                                      </tr>
                                                   </tbody></table>
                                                </td>
                                             </tr>
                                          </tbody></table>
                                       </td>
                                    </tr>
                                 </tbody></table>
                              </td>
                           </tr>
                        </tbody></table>
                     </td>
                  </tr>
               </tbody></table>
            </td>
         </tr>
      </tbody></table>
      
      
   <img width=""1px"" height=""1px"" alt="""" src=""https://ci5.googleusercontent.com/proxy/uhujGXM7_rq52x1CO7Wymmz-ClTqVSregL8ZxmW4Z62r9u75KrG6HT-Pp4itsQLB_gsWxGgdi1FQLLBTP2UO4VP1VdoYTuwhV1Rnox3yU0WmNbpf8llVQbPZavRkXLnqfvU52gORm9oiiFkN17udYKp6CSxRKbp2srefhVb9Twb5zrnVPeYHUEjRM7RCcpDCm--hX46AFNh2fMfPbkhjvH_TE5H_Aw8tQR23kPuXgHdzaB9gTv9NaQjq0vz2BKJCrxdD=s0-d-e1-ft#http://email.yachay.pe/o/eJwNyzEOwyAMQNHTlBFhbDAMLFGbexgIDVVJq2zp6Yv-9IZfU4TWRPVkjTXGWQ-MgE6DXsnc12VxfobhgTcyl5RdLv3d1J7YIRE3n7m2HAvbLMWHCWGKIZA600vq2eX4bceIMPfnkP7W5TP-NVAhhw"" class=""CToWUd""></div>";
            #endregion

            UsuarioEN _usuario = _IUsuarioRepository.confirmationToken(new UsuarioEN()
            {
                V_USUARIO = nombre
            });
            string body = String.Format(@bodyHTML, _usuario.V_NOMBRES, urlbase + "/TiendaVirtual/app/confirmation/" + _usuario.confirmationToken);
            Util.sendEmailHtml(_usuario.V_EMAIL, body, "Confirmación de su registro");
        }
        public DashboardEN getDashboard(DashboardEN item)
        {
            return _ITiendaRepository.SelectDashboard(item);
        }
        public List<RptTransacciones> SelectRptVentas(RptTransacciones item)
        {
            return _ITiendaRepository.SelectRptVentas(item);
        }
        public List<RptFormaPago> SelectRptFormaPago(RptFormaPago item)
        {
            return _ITiendaRepository.SelectRptFormaPago(item);
        }
        public List<RptEstadoCompra> SelectRptEstadoCompra(RptEstadoCompra item)
        {
            return _ITiendaRepository.SelectRptEstadoCompra(item);
        }
        public List<RptTransacciones> SelectRptVentasDiario(RptTransacciones item)
        {
            return _ITiendaRepository.SelectRptVentasDiario(item);
        }
        #endregion
        
        #region Planes

        public List<PlanesEN> SelectPlanesAll(PlanesEN item)
        {
            return _IPlanesRepository.SelectAll(item);
        }

        public PlanesEN SelectPlanes(PlanesEN item)
        {
            throw new NotImplementedException();
        }

        public void InsertPlanes(PlanesEN item)
        {
            throw new NotImplementedException();
        }

        public void DeletePlanes(PlanesEN item)
        {
            throw new NotImplementedException();
        }

        public void UpdatePlanes(PlanesEN item)
        {
            throw new NotImplementedException();
        }

	    #endregion

        #region PlanTienda

        public List<PlanTiendaEN> SelectPlanTiendaAll(PlanTiendaEN item)
        {
            return _IPlanTiendaRepository.SelectAll(item);
        }

        public PlanTiendaEN SelectPlanTienda(PlanTiendaEN item)
        {
            return _IPlanTiendaRepository.Select(item);
        }

        public ResponseEL InsertPlanTienda(PlanTiendaEN item)
        {
            ResponseEL response = new ResponseEL();
            try
            {
                _IPlanTiendaRepository.Insert(item);
                response.message = Constantes.Mensajes.Success;
                response.type = Constantes.TypeResult.Success;
                response.success = true;

            }
            catch (Exception ex)
            {
                response.stacktrace = ex.StackTrace;
                response.message = Constantes.Mensajes.ErrorServer;
                response.type = Constantes.TypeResult.Exception;
                response.success = false;
            }
            return response;
        }

        public void DeletePlanTienda(PlanTiendaEN item)
        {
            throw new NotImplementedException();
        }

        public ResponseEL UpdatePlanTienda(PlanTiendaEN item, bool produccion, string token_payment)
        {
            ResponseEL response = new ResponseEL();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _IPlanTiendaRepository.Update(item);

                    Dictionary<string, object> metadata = new Dictionary<string, object>
			        {
				        {"plantienda_id", item.idplantienda},
                        {"total_amount", (int)(item.montoplan*100)}
			        };

                    PaymentEN _PaymentEN = new PaymentEN();
                    _PaymentEN.description = item.nombreplan;
                    _PaymentEN.amount = (int)(item.montoplan * 100);
                    _PaymentEN.email = item.email;
                    _PaymentEN.metadata = metadata;
                    _PaymentEN.token_payment = token_payment;

                    PaymentResponseEN _PaymentResponseEN = this.procesarPagoCulqi(produccion, _PaymentEN);
                    if (_PaymentResponseEN.success)
                    {
                        response.message = _PaymentResponseEN.message;
                        response.success = true;
                        response.type = Constantes.TypeResult.Success;
                        
                        scope.Complete();
                    }
                    else
                    {
                        response.message = _PaymentResponseEN.message;
                        response.type = Constantes.TypeResult.Validation;
                        response.success = false;
                        scope.Dispose();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                response.stacktrace = ex.StackTrace;
                response.message = Constantes.Mensajes.ErrorServer;
                response.type = Constantes.TypeResult.Exception;
                response.success = false;
            }
            return response;
        }

        #endregion

        #region ParametroValor

        public ParametroValorEN SelectParametroValor(ParametroValorEN item)
        {
            return _IParametroValorRepository.Select(item);
        }

        #endregion

        #region PAGO-CULQI

        public PaymentResponseEN procesarPagoCulqi(bool produccion, PaymentEN item)
        {
            PaymentResponseEN _PaymentResponseEN = new PaymentResponseEN();

            IParametroValorRepository _IParametroValorRepository = new ParametroValorRepository();
            ParametroValorEN _ParametroValorEN = new ParametroValorEN();
            _ParametroValorEN.idparametro = produccion ? Constantes.Parametro.culqiPro : Constantes.Parametro.culqiDev;
            _ParametroValorEN = this.SelectParametroValor(_ParametroValorEN);

            var _PaymentCard = new PaymentCard(_ParametroValorEN.valor.Split('|')[0], _ParametroValorEN.valor.Split('|')[1]);
            

            _IParametroValorRepository.Select(_ParametroValorEN);

                Dictionary<string, object> map = new Dictionary<string, object>
			    {	
				    {"amount", item.amount},
				    {"capture", true},
				    {"currency_code", "PEN"},
				    {"description", item.description},
				    {"email", item.email},
				    {"installments", 0},
				    {"metadata", item.metadata},
				    {"source_id", item.token_payment}
			    };
                string response_createcharge = _PaymentCard.CreateCharge(map);
                var json_object = JObject.Parse(response_createcharge);
                if (((string)json_object["object"]).Equals("error"))
                {
                    _PaymentResponseEN.message = (string)json_object["user_message"];
                    _PaymentResponseEN.success = false;
                }
                else
                {
                    _PaymentResponseEN.tarjeta = ((dynamic)json_object["source"]).iin.card_brand + " " +
                                                    ((dynamic)json_object["source"]).iin.card_type + " "+
                                                    ((dynamic)json_object["source"]).iin.issuer.name;
                    _PaymentResponseEN.message = Constantes.Mensajes.PagoExitoso;
                    _PaymentResponseEN.success = true;
                }


            return _PaymentResponseEN;
        }

        #endregion



        public void Dispose()
        {
            throw new NotImplementedException();
        }

        
    }
}
