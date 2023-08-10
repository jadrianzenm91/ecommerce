using Domain.Entities;
using Domain.EntitiesLogic;
using Filters;
using Infrastructure.CrossCutting;
using Manager.Application;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Ecommerce.Presentacion.Web.MVC.Controllers
{
    [LoggingFilter]
    [HandlerCustomError]
    public class PedidosController : Controller
    {
        /*
        IManagerApplication _ManagerApplication = new ManagerApplication();
        //
        // GET: /Pedidos/
        public ActionResult Index(CarritoCompraTemporalEL item)
        {
            return PartialView(item);
        }
        
        public ActionResult MiBolsaCompra(string name, string token)
        {
            var BolsaCompra = _ManagerApplication.getDetalleCompraTemp(new DetalleCompraTempEN()
            {
                token = token
            });
            ViewBag.total = BolsaCompra.Count > 0 ? BolsaCompra[0].total.ToString() : "0.00";
            ViewBag.venta = BolsaCompra.Count > 0 ? (BolsaCompra[0].total * 72/100).ToString(): "0.00";
            ViewBag.igv = BolsaCompra.Count > 0 ? (BolsaCompra[0].total * 18 / 100).ToString() : "0.00";
            ViewBag.token = token;
            
            return View("MiBolsa", BolsaCompra);
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

                return View("Checkout", BolsaCompra);
            }
            else
            {
                return RedirectToAction("LoginUser", "Account", new { name = name, token = token });
            }
        }
        [Authorize]
        public JsonResult setCarritoCompra(CarritoCompraEL request)
        {
            ResponseEL response = new ResponseEL();

            request.usuario = User.Identity.Name;
            response = _ManagerApplication.InsertarCarritoCompra(request);
            

            return new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        */
    }
}
