using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using Domain.EntitiesLogic;
using Newtonsoft.Json;
using Manager.Application;
using Domain.Entities;
using Filters;
using WebMatrix.WebData;
using Infrastructure.CrossCutting;
using System.Web.Security;
using Infraestruture.CrossCutting;

namespace Ecommerce.Presentacion.Web.MVC
{
    [LoggingFilter]
    [HandlerCustomError]
    public class AuthProviderController : ApiController
    {
        [HttpPost]
        public object Post(UsuarioEN item)
        {
            long idUsuario = 0;
            try
            {
                if (!String.IsNullOrEmpty(item.V_EMAIL) && !String.IsNullOrEmpty(item.V_NOMBRES))
                {
                    if(WebSecurity.UserExists(item.V_EMAIL))
                    {
                        FormsAuthentication.SetAuthCookie(item.V_EMAIL, true);
                        return new
                        {
                            success = true,
                            data = Infrastructure.CrossCutting.Encrypting.EncryptKey(WebSecurity.GetUserId(item.V_EMAIL).ToString())
                        };
                        
                    }else {
                        WebSecurity.CreateUserAndAccount(item.V_EMAIL, Constantes.Login.PasswordClienteRedSocial, new
                        {
                            V_NOMBRES = item.V_NOMBRES,
                            V_APELLIDOS = item.V_NOMBRES,
                            V_DNI = String.Empty,
                            V_USER_CREATE = Constantes.Login.UserFirebase,
                            D_DATE_CREATE = DateTime.Now,
                            V_EMAIL = item.V_EMAIL,
                            B_ACTIVE = 1, 
                            V_PHOTO_URL = item.V_PHOTO_URL,
                            V_PROVIDER = item.V_PROVIDER,
                            UID_PROVIDER = item.V_UID_PROVIDER
                        });

                        //asignar rol - usuario
                        var RoleProvider = (SimpleRoleProvider)Roles.Provider;

                        if (!RoleProvider.GetRolesForUser(item.V_EMAIL).Contains(Constantes.Perfiles.Cliente))
                            RoleProvider.AddUsersToRoles(new string[] { item.V_EMAIL }, new string[] { Constantes.Perfiles.Cliente });

                        FormsAuthentication.SetAuthCookie(item.V_EMAIL, true);

                        return new
                        {
                            success = true,
                            data = Infrastructure.CrossCutting.Encrypting.EncryptKey(idUsuario.ToString())
                        };
                    }
                   
                }
                return new
                {
                    success = false,
                    data = "Email y/o Nombres no pueden ser vacíos."
                };

            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    error = ex.StackTrace.ToString()
                };
                
            }

           
        }
    }
    [LoggingFilter]
    [HandlerCustomError]
    public class CategoriasController : ApiController
    {
        [HttpPost]
        public IEnumerable<object> Post(int idtienda)
        {
            IManagerApplication _ManagerApplication = new ManagerApplication();
            var result = _ManagerApplication.getCategorias(new CategoriaEN()
            {
                I_CATEGORIA_PADRE = (int)(Enumeradores.OtherFilters.Todos),
                I_CODIGO_TIENDA = idtienda
            });

            List<Object> nameList = new List<Object>();
            nameList.Add(new { id = Infrastructure.CrossCutting.Enumeradores.OtherFilters.Seleccione,
                               name = Infrastructure.CrossCutting.Constantes.OtherFilters.Seleccione
            });
            foreach (var item in result)
            {
                nameList.Add(new { id = item.I_CODIGO_CATEGORIA, name = item.V_CATEGORIA });
            }


            return nameList;
        }
    }
    [LoggingFilter]
    [HandlerCustomError]
    public class SearchController : ApiController
    {
        [AcceptVerbs("*", "*", "GET", "POST")]
        //Busqueda sensitivo.
        public IEnumerable<object>  GetProducts([FromBody] string text)
        {
            IManagerApplication _ManagerApplication = new ManagerApplication();

            TiendaEN obj = _ManagerApplication.getTienda(new TiendaEN()
            {
                V_WEB = Request.RequestUri.Host
            });
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            string filter = nvc["vFilter"].ToString();

            
            var result = _ManagerApplication.BuscarProductos(new ProductoEN(){
              V_PRODUCTO = filter,
              I_CODIGO_TIENDA = obj.I_CODIGO_TIENDA,
              B_ACTIVE = true
            });

            List<Object> nameList = new List<Object>();
            foreach (var item in result)
            {
                nameList.Add(new { id = item.V_CODIGO_PRODUCTO, name = item.V_PRODUCTO });    
            }
            

            return nameList; 
        }
    }
    [LoggingFilter]
    [NotImplExceptionFilter]
    public class DetalleCompraController : ApiController
    {
        [HttpPost]
        public ResponseEL Post(DetalleCompraTempEN item)
        {
            ResponseEL response = new ResponseEL();
            IManagerApplication _ManagerApplication = new ManagerApplication();
            
            //para registrar bolsa de compra de usuarios no autenticados
            if (!string.IsNullOrEmpty(item.token))
            {
                response = _ManagerApplication.InsertarCarritoCompraDetalleTemp(new DetalleCompraTempEN()
                {
                    id = !string.IsNullOrEmpty(item.idEnc)? Convert.ToInt64(Encrypting.DecryptKey(item.idEnc)) : item.id,
                    token = item.token,
                    idprod = Convert.ToInt64(Encrypting.DecryptKey(item.idprodEnc)),
                    cant = item.cant
                });

            }
            else
            {
                response.message = "token no generado";
                response.success = false;
                response.data = _ManagerApplication.createToken(new tokenUsuarioEN()).token;
            }

            return response;
        }
    }
    [LoggingFilter]
    [NotImplExceptionFilter]
    public class TotalBolsaCompraController : ApiController
    {
        [HttpPost]
        public ResponseEL Post(DetalleCompraTempEN item)
        {
            ResponseEL response = new ResponseEL();
            IManagerApplication _ManagerApplication = new ManagerApplication();

            //para registrar bolsa de compra de usuarios no autenticados
            if (!string.IsNullOrEmpty(item.token))
            {
                var BolsaCompra = _ManagerApplication.getDetalleCompraTemp(new DetalleCompraTempEN()
                {
                    token = item.token
                });
                response.success = true;
                response.data = BolsaCompra.Count > 0 ? (DataConvert.ToInt32(BolsaCompra[0].total * 100)) : 0;
            }
            else
            {
                response.message = "token no generado";
                response.success = false;
                response.data = string.Empty;
            }

            return response;
        }
    }
    

    
}