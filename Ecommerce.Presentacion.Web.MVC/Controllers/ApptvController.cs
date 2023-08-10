using Domain.Entities;
using Domain.EntitiesLogic;
using Infraestruture.CrossCutting;
using Infrastructure.CrossCutting;
using Manager.Application;
using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace Ecommerce.Presentacion.Web.MVC.Controllers
{
    public class ApptvController : Controller
    {
        IManagerApplication _ManagerApplication = new ManagerApplication();
        
        #region Views - GET

       
        public ActionResult index(string name, string text, string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                var _tokenUsuarioEN = _ManagerApplication.createToken(new tokenUsuarioEN());
                return RedirectToAction("index", "Apptv", new { name = name, text = text, token = _tokenUsuarioEN.token });
            }
            var result = new List<ProductoEN>();
            var tiendaEN = _ManagerApplication.getTienda(new TiendaEN()
            {
                V_WEB = name
            });

            ProductoEN item = new ProductoEN();
            item.I_CODIGO_TIENDA = tiendaEN.I_CODIGO_TIENDA;
            item.B_ACTIVE = true;
            item.V_PRODUCTO = text;
            result = _ManagerApplication.SelectAllProduct(item);
            
            ViewBag.idtienda = tiendaEN.I_CODIGO_TIENDA;
            ViewBag.name = name;
            ViewBag.productos = result;
            ViewBag.token = token;
            
            return View();
        }
        public ActionResult productodetalle(string name, string id, string token)
        {
            var idprod = Convert.ToInt64(Encrypting.DecryptKey(id));

            ProductoEL result = _ManagerApplication.getProductoDetalle(idprod);


            var BolsaCompra = _ManagerApplication.getDetalleCompraTemp(new DetalleCompraTempEN()
            {
                token = token,
                idprod = idprod
            });
            ViewBag.idtoken = BolsaCompra.Count > 0 ? Encrypting.EncryptKey(BolsaCompra[0].id.ToString()) : null;
            ViewBag.cantidad = BolsaCompra.Count > 0 ? BolsaCompra[0].cant : 1;
            ViewBag.name = name;
            ViewBag.token = token;

            return View(result);
        }
        public ActionResult bolsacompra(string name, string token)
        {
            var BolsaCompra = _ManagerApplication.getDetalleCompraTemp(new DetalleCompraTempEN()
            {
                token = token
            });
            ViewBag.total = BolsaCompra.Count > 0 ? BolsaCompra[0].total.ToString() : "0.00";
            ViewBag.venta = BolsaCompra.Count > 0 ? (BolsaCompra[0].total * 82 / 100).ToString() : "0.00";
            ViewBag.igv = BolsaCompra.Count > 0 ? (BolsaCompra[0].total * 18 / 100).ToString() : "0.00";
            ViewBag.token = token;
            ViewBag.name = name;

            return View(BolsaCompra);
        }
        public ActionResult login(string name, string token)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "Apptv", new { name = name, token = token });
            }
            ViewBag.name = name;
            ViewBag.token = token;
            return View();
        }

        [Authorize]
        public ActionResult LogOffUser(string name, string token)
        {
            FormsAuthentication.SignOut();
            WebSecurity.Logout();

            return RedirectToAction("index", "Apptv", new { name = name, token = token });
        }
        public ActionResult register(string name, string token)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "Apptv", new { name = name, token = token });
            }

            ViewBag.name = name;
            ViewBag.token = token;
            return View();
        }
        public ActionResult resetPassword(string name, string token, string tokenSecurity)
        {
            ViewBag.name = name;
            ViewBag.token = token;

            int iduser = WebSecurity.GetUserIdFromPasswordResetToken(tokenSecurity);
            if (iduser == -1)
            {
                ViewBag.ResetPasswordType = "warning";
                ViewBag.ResetPasswordMessage = "Algo salió mal, el token ha expirado o no existe.";
                return View("login");
            }
           
           ViewBag.tokenSecurity = tokenSecurity;
            return View("resetPassword");
        }
        public ActionResult misordenes(string name, string token)
        {
            if (Request.IsAuthenticated)
            {
                CarritoCompraEN obj = new CarritoCompraEN();
                obj.I_CODIGO_COMPRA = 0;
                 obj.V_USER = User.Identity.Name;
                 obj.tiendaEN.V_TIENDA = name;

               var result=  _ManagerApplication.getCarritoCompraUsuarioTienda(obj);
               return View(result);
            }
            else
            {
                return RedirectToAction("login", "Apptv", new { name = name, token = token });
            }
        }
        public ActionResult misdatos(string name, string token)
        {
            if (Request.IsAuthenticated)
            {
                UsuarioEN usuario = _ManagerApplication.getUsuario(new UsuarioEN()
                {
                    V_USUARIO = Request.IsAuthenticated ? User.Identity.Name : string.Empty
                });
                RequestUsuarioDatos user = new RequestUsuarioDatos();
                user.nombres = usuario.V_NOMBRES;
                user.apellidos = usuario.V_APELLIDOS;
                user.dni = usuario.V_DNI;
                user.celular = usuario.I_CELULAR;
                user.dpto = usuario.V_DPTO;
                user.distrito = usuario.V_DISTR;
                user.prov = usuario.V_PROV;
                user.email = usuario.V_EMAIL;
                user.direccion = usuario.V_DIRECCION;
                return View(user);
            }
            else
            {
                return RedirectToAction("login", "Apptv", new { name = name, token = token });
            }
        }
        public ActionResult checkout(string name, string token)
        {
            ViewBag.name = name;
            ViewBag.token = token;
            if (Request.IsAuthenticated)
            {
                var BolsaCompra = _ManagerApplication.getDetalleCompraTemp(new DetalleCompraTempEN()
                {
                    token = token
                });
                ViewBag.total = BolsaCompra.Count > 0 ? BolsaCompra[0].total.ToString() : "0.00";
                ViewBag.venta = BolsaCompra.Count > 0 ? (BolsaCompra[0].total * 72 / 100).ToString() : "0.00";
                ViewBag.igv = BolsaCompra.Count > 0 ? (BolsaCompra[0].total * 18 / 100).ToString() : "0.00";
                ViewBag.token = token;

                return View("checkout", BolsaCompra);
            }
            else
            {
                return RedirectToAction("login", "Apptv", new { name = name, token = token });
            }
        }
        public ActionResult comprafinalizado(string token)
        {
                if (string.IsNullOrEmpty(token))
                {
                    return View();
                }
                int idcompra = Convert.ToInt32(Encrypting.DecryptKey(token));
                CarritoDetalle item = _ManagerApplication.getCarritoCompraDetalle(idcompra);
                var usuario = _ManagerApplication.getUsuario(new UsuarioEN() { V_USUARIO = item.carritoCompraEL.usuario });

                string bodyFormat = Util.bodyEmailComprobante();

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
                    item.carritoCompraEL.direccion,
                    item.carritoCompraEL.nombres,
                    item.carritoCompraEL.dni,
                    _ManagerApplication.getProductosDetalleHTML(idcompra),
                    item.carritoCompraEL.razonsocial,//{14}
                    item.carritoCompraEL.estadoCompra,
                    item.carritoCompraEL.mensajeEstadoCompra,
                    Util.getImageEstadoCompra(item.carritoCompraEL.codigoEstadoCompra),
                    item.carritoCompraEL.logotienda,//{18}
                    item.carritoCompraEL.telefonotienda,//{19}
                    item.carritoCompraEL.facebook,//{20}
                    item.carritoCompraEL.emailtienda,//{21}
                    item.carritoCompraEL.webtienda,//{22}
                    item.carritoCompraEL.tienda
                    );
                ViewBag.bodyHtml = body;

                return View();
           
            
        }
        public ActionResult politicasDevolucion(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("index", new { name = name });
            }
            var modelTienda = _ManagerApplication.getTienda(new TiendaEN()
            {
                V_WEB = name
            });

            return View(modelTienda);
        }
        
        #endregion

        #region Ajax

        public ActionResult verPedidosModal(string name, string token)
        {

            var BolsaCompra = _ManagerApplication.getDetalleCompraTemp(new DetalleCompraTempEN()
            {
                token = token
            });
            ViewBag.token = token;
            ViewBag.name = name;
            return PartialView("_ListaPedidosModal", BolsaCompra);
        }
        [Authorize]
        public JsonResult setCarritoCompra(CarritoCompraEL request)
        {
            ResponseEL response = new ResponseEL();
            request.codigoFormaPago = request.token_payment.Equals(Constantes.FormaPago.tokenReserva)? Constantes.FormaPago.Reserva : Constantes.FormaPago.PagoOnline;
            request.usuario = User.Identity.Name;
            response = _ManagerApplication.InsertarCarritoCompra(request, Request.Url.ToString().Contains("localhost"));


            return new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region FormPost

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult resetPassword(ResetUsuarioTienda model, string name, string token, string tokenSecurity)
        {
            ViewBag.name = name;
            ViewBag.token = token;
            if (model.Password.Equals(model.ConfirmPassword))
            {
                if (!WebSecurity.ResetPassword(tokenSecurity, model.Password))
                {
                    ViewBag.ResetPasswordType = "warning";
                    ViewBag.ResetPasswordMessage = "Algo salió mal, intenta recuperar tu contraseña nuevamente.";
                }
                else
                {
                    ViewBag.ResetPasswordType = "success";
                    ViewBag.ResetPasswordMessage = "Se acó su contraseña con éxito.";
                }
            }
            else
            {
                ViewBag.ResetPasswordType = "warning";
                ViewBag.ResetPasswordMessage = "Las contraseñas no coinciden.";

                return View("resetPassword");
            }

           
            return View("login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult forgetPassword(string name, string email, string token)
        {
            if (!WebSecurity.UserExists(email))
            {
                ViewBag.Message = "El email ingresado no existe. Debe registrarse como nuevo usuario. Gracias!";

                return RedirectToAction("login", new { name = name, token = token });
            }
            var tokenSecurity = WebSecurity.GeneratePasswordResetToken(email);
            var urlResetPassword = Request.Url.GetLeftPart(UriPartial.Authority) +
                "/Apptv/resetPassword?name=" + name + "&token=" + token + "&tokenSecurity=" + tokenSecurity;
            _ManagerApplication.sendEmailforgetPassword(email, urlResetPassword);

            return RedirectToAction("login", new { name = name, token = token });
        }

       
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(RegisterUsuarioTienda model, string name, string token)
        {

            if (ModelState.IsValid)
            {
                if (WebSecurity.UserExists(model.V_EMAIL))
                {
                    ViewBag.Message = "El usuario ya existe.";
                    return View("register", model);
                }
                // Attempt to register the user
                try
                {
                    var menbershipProvider =
                    (SimpleMembershipProvider)Membership.Provider;

                    var RoleProvider = (SimpleRoleProvider)Roles.Provider;


                    WebSecurity.CreateUserAndAccount(model.V_EMAIL, model.Password, new
                    {
                        V_NOMBRES = string.Empty,
                        V_APELLIDOS = string.Empty,
                        V_EMAIL = model.V_EMAIL,
                        V_USER_CREATE = model.V_EMAIL,
                        D_DATE_CREATE = DateTime.Now,
                        V_DNI = string.Empty,
                        I_CELULAR = string.Empty,
                        V_PROVIDER = Constantes.Web.PLATAFORMA_WEB,
                        B_ACTIVE = 1
                    }, true);

                    //asignar rol - usuario
                    if (!RoleProvider.GetRolesForUser(model.V_EMAIL).Contains(Constantes.Perfiles.Cliente))
                        RoleProvider.AddUsersToRoles(new string[] { model.V_EMAIL }, new string[] { Constantes.Perfiles.Cliente });

                    WebSecurity.Login(model.V_EMAIL, model.Password);

                    return RedirectToAction("checkout", new { name = name, token = token });
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            ViewBag.Message = "Algunos datos no son correctos. Intente nuevamente.";
            return View("register",model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LoginUser(LoginModel model, string name, string token)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToAction("checkout", "Apptv", new { name = name, token = token });
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "El usuario y/o clave son incorrectos.");
            return View("login",model);
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult search(string name, string text, string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                var _tokenUsuarioEN = _ManagerApplication.createToken(new tokenUsuarioEN());
                return RedirectToAction("index", "Apptv", new { name = name, text = text, token = _tokenUsuarioEN.token });
            }
            var result = new List<ProductoEN>();
            var tiendaEN = _ManagerApplication.getTienda(new TiendaEN()
            {
                V_WEB = name
            });

            ProductoEN item = new ProductoEN();
            item.I_CODIGO_TIENDA = tiendaEN.I_CODIGO_TIENDA;
            item.B_ACTIVE = true;
            item.V_PRODUCTO = text;
            result = _ManagerApplication.SelectAllProduct(item);
            ViewBag.idtienda = item.I_CODIGO_TIENDA;
            ViewBag.text = text;
            ViewBag.name = name;
            ViewBag.productos = result;
            ViewBag.token = token;
            return View("index");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult updateDatosUsuario(RequestUsuarioDatos model, string name, string token)
        {
            //var name = Request.QueryString["name"];
            //var token = Request.QueryString["token"];

            if (ModelState.IsValid)
            {
                _ManagerApplication.updateUsuario(new UsuarioEN()
                {
                    V_USER = User.Identity.Name,
                    V_NOMBRES = model.nombres,
                    V_APELLIDOS = model.apellidos,
                    V_DNI = model.dni,
                    I_CELULAR = model.celular,
                    V_DIRECCION= model.direccion,
                    V_DPTO = model.dpto,
                    V_PROV = model.prov,
                    V_DISTR = model.distrito
                });
                ViewBag.type = "success";
                ViewBag.Message = "Se actualizaron los datos correctamente";
            }
            else
            {
                ViewBag.type = "warning";
                ViewBag.Message = "Algunos datos no son correctos. Intente nuevamente.";
            }
            
            return View("misdatos", model);
        }
        #endregion

        #region Helpers
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
