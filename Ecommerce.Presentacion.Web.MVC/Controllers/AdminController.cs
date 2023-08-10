using Domain.Entities;
using Domain.EntitiesLogic;
using Ecommerce.Presentacion.Web.MVC.Models;
using Filters;
using Infraestruture.CrossCutting;
using Infrastructure.CrossCutting;
using Manager.Application;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace Ecommerce.Presentacion.Web.MVC.Controllers
{
    [Authorize(Roles = Constantes.Perfiles.Administrador + "," + Constantes.Perfiles.Empresa)]
    [LoggingFilter]
    [HandlerCustomError]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        IManagerApplication _ManagerApplication = new ManagerApplication();

        [AllowAnonymous]
        public ActionResult login()
        {
            return View("Login");
        }

        #region Dashboard
        
        public ActionResult Index()
        {
            TiendaEN item = _ManagerApplication.getTienda(new TiendaEN()
            {
                V_USER = User.Identity.Name
            });
            if (item.I_CODIGO_TIENDA != 0)
            {
                var result = _ManagerApplication.getDashboard(new DashboardEN()
                {
                    V_USER = User.Identity.Name
                });
                result.listRptVentas = _ManagerApplication.SelectRptVentas(new RptTransacciones()
                {
                    V_USER = User.Identity.Name
                });
                result.listRptFormaPago = _ManagerApplication.SelectRptFormaPago(new RptFormaPago()
                {
                    V_USER = User.Identity.Name
                });
                result.listRptEstadoCompra = _ManagerApplication.SelectRptEstadoCompra(new RptEstadoCompra()
                {
                    V_USER = User.Identity.Name
                });
                result.listRptVentasDiarias = _ManagerApplication.SelectRptVentasDiario(new RptTransacciones()
                {
                    V_USER = User.Identity.Name
                });

                return View(result);
            }
            else
            {
                return RedirectToAction("datostienda");
            }

            
        }

        #endregion

        #region Productos

        public ActionResult bandejaProductos()
        {
            var result = new Dashboard();

            IManagerApplication _ManagerApplication = new ManagerApplication();
            ProductoEN item = new ProductoEN();
            item.V_USER = User.Identity.Name;
            result.ListaProductos = _ManagerApplication.SelectAllProduct(item);

            return View("_BandejaProductos", result);
        }
        
        [HttpPost]
        public ActionResult nuevoProducto(int idtienda)
        {
            IManagerApplication _ManagerApplication = new ManagerApplication();
            var result = _ManagerApplication.getCategorias(new CategoriaEN()
            {
                I_CODIGO_TIENDA = idtienda,
                I_CATEGORIA_PADRE = (int)(Enumeradores.OtherFilters.Todos),
            }).Where(x=>x.B_ACTIVE == true);
            List<SelectListItem> ObjList = new List<SelectListItem>()
            {
                new SelectListItem { Text = Infrastructure.CrossCutting.Constantes.OtherFilters.Seleccione + " Categoria", 
                    Value = Infrastructure.CrossCutting.Enumeradores.OtherFilters.Seleccione.ToString(), Selected=true },

            };

            foreach (var item in result)
            {
                ObjList.Add(new SelectListItem
                {
                    Text = item.V_CATEGORIA,
                    Value = item.I_CODIGO_CATEGORIA.ToString()
                });
            }
            
            ViewBag.categorias = ObjList;
            return PartialView("_NuevoProducto");
        }

        #endregion

        #region Ordenes
        
        public ActionResult bandejaOrdenes()
        {
            IManagerApplication _ManagerApplication = new ManagerApplication();
            TiendaEN item = new TiendaEN();
            item.V_USER = User.Identity.Name;
            var result = _ManagerApplication.getCarritoCompraAll(item);

            return PartialView("_BandejaOrdenes", result);
        }

        public ActionResult registroOrden()
        {
            
            return PartialView("regOrden", new CarritoDetalle());
        }

        public ActionResult detallecompra(string idencryp)
        {
            //falta identificar la tienda

            IManagerApplication _ManagerApplication = new ManagerApplication();

            var result = _ManagerApplication.getCarritoCompraDetalle(Convert.ToInt32(Encrypting.DecryptKey(idencryp)));


            return View("DetalleCompra", result);
        }

        public JsonResult setArmarOrden(int idcompra)
        {
            IManagerApplication _ManagerApplication = new ManagerApplication();
            var response = _ManagerApplication.actualizarEstadoCompra(new CarritoCompraEN()
            {
                I_CODIGO_COMPRA = idcompra,
                I_ESTADO_COMPRA = Constantes.EstadoCompra.Confirmado,
                V_USER_CREATE = User.Identity.Name
            });
            return new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult setEntregaEnCamino(int idcompra)
        {
            IManagerApplication _ManagerApplication = new ManagerApplication();
            var response = _ManagerApplication.actualizarEstadoCompra(new CarritoCompraEN()
            {
                I_CODIGO_COMPRA = idcompra,
                I_ESTADO_COMPRA = Constantes.EstadoCompra.EnCaminoEntrega,
                V_USER_CREATE = User.Identity.Name
            });
            return new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult setEntregarOrden(int idcompra)
        {
            IManagerApplication _ManagerApplication = new ManagerApplication();
            var response = _ManagerApplication.actualizarEstadoCompra(new CarritoCompraEN()
            {
                I_CODIGO_COMPRA = idcompra,
                I_ESTADO_COMPRA = Constantes.EstadoCompra.Entregado,
                V_USER_CREATE = User.Identity.Name
            });
            return new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult setAnularOrden(int idcompra)
        {
            IManagerApplication _ManagerApplication = new ManagerApplication();
            var response = _ManagerApplication.actualizarEstadoCompra(new CarritoCompraEN()
            {
                I_CODIGO_COMPRA = idcompra,
                I_ESTADO_COMPRA = Constantes.EstadoCompra.Anulado,
                V_USER_CREATE = User.Identity.Name
            });
            return new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #endregion

        #region Clientes
        
        public ActionResult clientes()
        {
            var lstClientes = _ManagerApplication.getTiendaAll(new TiendaEN());

            return View("Clientes", lstClientes);
        }

        #endregion

        #region DatosTienda
        
        public ActionResult DatosTienda(string id)
        {
            int idtienda = (string.IsNullOrEmpty(id) ? 0 : DataConvert.ToInt32(Encrypting.DecryptKey(id.ToString())));

            var result = _ManagerApplication.getTienda(new TiendaEN()
            {
                I_CODIGO_TIENDA = idtienda,
                V_USER = idtienda==0? User.Identity.Name : string.Empty
            });
            return View(result);
        }

        [Authorize(Roles=Constantes.Perfiles.Administrador)]
        public ActionResult regPlan()
        {
           
            return View("regPlan");
        }
        public ActionResult periodosfacturacion(string id)
        {
            int idtienda = (string.IsNullOrEmpty(id) ? 0 : DataConvert.ToInt32(Encrypting.DecryptKey(id.ToString())));
            var result = _ManagerApplication.SelectPlanTiendaAll(new PlanTiendaEN()
            {
                idtienda = idtienda,
                V_USER = idtienda == 0 ? User.Identity.Name : string.Empty
            });
            
            return View("periodosfacturacion", result);
        }
        public ActionResult suscripcion(string id)
        {
            int idtienda = (string.IsNullOrEmpty(id) ? 0 : DataConvert.ToInt32(Encrypting.DecryptKey(id.ToString())));
            var planTiendaEN = _ManagerApplication.SelectPlanTienda(new PlanTiendaEN()
            {
                idtienda = idtienda
            });
            var modelTienda = _ManagerApplication.getTienda(new TiendaEN()
            {
                I_CODIGO_TIENDA = idtienda
            });

            var ObjList = new List<SelectListItem>()
            {
                new SelectListItem { Text = Constantes.OtherFilters.Seleccione, Value = Enumeradores.OtherFilters.Seleccione.ToString(), Selected=true }
            };
            var result = _ManagerApplication.SelectPlanesAll(new PlanesEN());
            foreach (var item in result)
            {
                ObjList.Add(new SelectListItem{ Text = item.nombreplan + " S/" + item.montoplan,Value = item.idplan.ToString() });
            }

            ViewBag.planes = ObjList;

            var ObjListTipos = new List<SelectListItem>();
            ObjListTipos.Add(new SelectListItem { Text = "Mensual", Value = "M" });
            ObjListTipos.Add(new SelectListItem { Text = "Anual", Value = "A" });
            ObjListTipos.Add(new SelectListItem { Text = "Único", Value = "U" });

            ViewBag.tipo = ObjListTipos;
            ViewBag.idtiendaEncryp = id;
            ViewBag.tienda = modelTienda.V_TIENDA;

            return View(planTiendaEN);
        }

        public ActionResult pagarfactura(string idplantienda)
        {
            int _idplantienda = DataConvert.ToInt32(Encrypting.DecryptKey(idplantienda));
            var plantienda = _ManagerApplication.SelectPlanTienda(new PlanTiendaEN()
            {
                idplantienda = _idplantienda
            });

            ParametroValorEN item = new ParametroValorEN();
            item.idparametro = Request.Url.ToString().Contains("localhost") ? Constantes.Parametro.culqiDev : Constantes.Parametro.culqiPro;
            var result = _ManagerApplication.SelectParametroValor(item);
            ViewBag.pk = result.valor.Split('|')[0];
            ViewBag.monto = DataConvert.ToInt32(plantienda.montoplan*100);
            ViewBag.description = "PAGO DE FACTURACION - " + plantienda.nombreplan;
            ViewBag.idplantienda = idplantienda;

            return View();
        }

        //lo realiza el cliente
        public ActionResult procesarPago(string idplantienda, string tokenculqi, string email)
        {
            var plantienda = _ManagerApplication.SelectPlanTienda(new PlanTiendaEN()
            {
                idplantienda = DataConvert.ToInt32(Encrypting.DecryptKey(idplantienda).ToString())
            });
            plantienda.V_USER = User.Identity.Name;
            plantienda.email = email.Replace("|",".");

            var response = _ManagerApplication.UpdatePlanTienda(plantienda, Request.Url.ToString().Contains("localhost") ? false : true, tokenculqi);
            
            TempData[Constantes.Mensajes.Message] = response;

            return RedirectToAction("periodosfacturacion", "admin");
        }
        #endregion

        #region Categoria

        /// <summary>
        /// Vista de listado de categorias de una tienda según el usuario logueado
        /// </summary>
        /// <returns></returns>
        public ActionResult categoria()
        {
            CategoriaEN model = new CategoriaEN();
            model.V_USER = User.Identity.Name;
            //model.B_ACTIVE = true;
            var result = _ManagerApplication.getCategorias(model);
            return View(result);
        }

        /// <summary>
        /// Vista de registro de categorias
        /// </summary>
        /// <returns></returns>
        public ActionResult regcategoria(CategoriaEN model)
        {
            model.B_ACTIVE = true;
            return View(model);
        }

        /// <summary>
        /// Vista de edicion de categoria
        /// </summary>
        /// <returns></returns>
        public ActionResult editcategoria(string idcategoria)
        {
            if (!string.IsNullOrEmpty(idcategoria))
                RedirectToAction("categoria");

            CategoriaEN model = new CategoriaEN();
            model.V_USER = User.Identity.Name;
            model.I_CODIGO_CATEGORIA = Convert.ToInt32(Encrypting.DecryptKey(idcategoria));
            var result = _ManagerApplication.getCategorias(model).FirstOrDefault();

            return View("regcategoria", result);
        }

        /// <summary>
        /// Vista de registro de subcategorias
        /// </summary>
        /// <param name="idcategoria"></param>
        /// <returns></returns>
        public ActionResult regsubcategoria(string idcategoria)
        {
            
            if (string.IsNullOrEmpty(idcategoria))
                RedirectToAction("categoria");

            CategoriaEN model = new CategoriaEN();
            model.V_USER = User.Identity.Name;
            model.I_CODIGO_CATEGORIA = Convert.ToInt32(Encrypting.DecryptKey(idcategoria));
            var result = _ManagerApplication.getCategorias(model).FirstOrDefault();
            result.I_CATEGORIA_PADRE = Convert.ToInt32(Encrypting.DecryptKey(idcategoria));
            
            return View(result);
        }

        #endregion

        #region ValidarToken-ForgotPassword

        [AllowAnonymous]
        public ActionResult resetPassword(string tokenSecurity)
        {
            ResponseEL response = new ResponseEL();
            int iduser = WebSecurity.GetUserIdFromPasswordResetToken(tokenSecurity);
            if (iduser == -1)
            {
                response.type = Constantes.TypeResult.Validation;
                response.success = false;
                response.message = "Algo salió mal, el token ha expirado o no existe.";
                TempData[Constantes.Mensajes.Message] = response;
                return View("login");
            }

            ViewBag.tokenSecurity = tokenSecurity;
            return View("resetPassword");
        }

        #endregion
        
        #region FormPost

        #region Login
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            WebSecurity.Logout();

            return RedirectToAction("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (!WebSecurity.UserExists(model.UserName))
            {
                ModelState.AddModelError("", "Usuario no existe");
                return View(model);
            }
            if (!WebSecurity.IsConfirmed(model.UserName))
            {
                ModelState.AddModelError("", "La cuenta tiene pendiente de confirmar su registro, revise su correo");
            }
            else if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "El usuario y/o clave son incorrectos");
            }


            // If we got this far, something failed, redisplay form

            return View(model);
        }
        #endregion

        #region registerDatosTienda
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterTienda(TiendaEN model)
        {
            if (ModelState.IsValid)
            {
                model.V_USER = User.Identity.Name;
                _ManagerApplication.insertarTienda(model);
                return RedirectToAction("Index", "Admin");
            }

            return View("datostienda", model);
        }
        #endregion

        #region registerCategoria

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult registerCategoria(CategoriaEN model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.V_USER_CREATE = User.Identity.Name;
                    _ManagerApplication.setCategorias(model);
                    return RedirectToAction("categoria");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            return RedirectToAction("regcategoria", model);
        }

        #endregion

        #region registerPlanTienda

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult registerPlanTienda(PlanTiendaEN model, string idtiendaEncryp)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(idtiendaEncryp))
            {
                model.V_USER = User.Identity.Name;
                model.idtienda =  DataConvert.ToInt32(Encrypting.DecryptKey(idtiendaEncryp));
                var response = _ManagerApplication.InsertPlanTienda(model);
                TempData[Constantes.Mensajes.Message] = response;

            }

            return RedirectToAction("Clientes");
        }

        #endregion

        #region ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult resetPassword(ResetUsuarioTienda model, string tokenSecurity)
        {
            ResponseEL response = new ResponseEL();
            if (ModelState.IsValid)
            {
                if (!WebSecurity.ResetPassword(tokenSecurity, model.Password))
                {
                    response.type = Constantes.TypeResult.Validation;
                    response.success = false;
                    response.message = "Algo salió mal, intenta recuperar tu contraseña nuevamente.";
                    TempData[Constantes.Mensajes.Message] = response;

                }
                else
                {
                    response.type = Constantes.TypeResult.Success;
                    response.success = true;
                    response.message = "Se actualizó su contraseña con éxito.";
                    TempData[Constantes.Mensajes.Message] = response;
                    
                }
            }
            else
            {
                return View("resetPassword");
            }
            
            return View("login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult forgetPassword(string email)
        {
            ResponseEL response = new ResponseEL();
            if (!WebSecurity.UserExists(email))
            {
                response.type = Constantes.TypeResult.Validation;
                response.success = false;
                response.message = "El email ingresado no existe. Debe registrarse como nuevo usuario.";
                TempData[Constantes.Mensajes.Message] = response;

                return RedirectToAction("login");
            }
            var tokenSecurity = WebSecurity.GeneratePasswordResetToken(email);
            var urlResetPassword = Request.Url.GetLeftPart(UriPartial.Authority) +
                "/Admin/resetPassword?"+"&tokenSecurity=" + tokenSecurity;
            _ManagerApplication.sendEmailforgetPassword(email, urlResetPassword);
            
            response.type = Constantes.TypeResult.Success;
            response.success = true;
            response.message = "Se envió un mensaje a su correo ingresado.";
            TempData[Constantes.Mensajes.Message] = response;
            return RedirectToAction("login");
        }

        #endregion
        #endregion
    }
}
