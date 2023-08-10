using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ecommerce.Presentacion.Web.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "TiendaVirtual", action = "register" }
            );
            #region TiendaVirtual
                routes.MapRoute(
                  name: "urltiendacliente",
                  url: "mi/tienda/{name}",
                  defaults: new { controller = "Apptv", action = "index", name = UrlParameter.Optional }
                );
                routes.MapRoute(
                   name: "confirmacionRegistroTiendaVirtual",
                   url: "{controller}/app/{action}/{token}",
                   defaults: new { controller = "TiendaVirtual", action = "confirmation", token = UrlParameter.Optional }
               );
              
                routes.MapRoute(
                      name: "mostrarComprobanteCompra",
                      url: "{controller}/{action}/{token}",
                      defaults: new { controller = "Apptv", action = "comprafinalizado", token = UrlParameter.Optional }
                  );
            #endregion

            #region Admin
               
                routes.MapRoute(
                   name: "loginAdmin",
                   url: "admin/login",
                   defaults: new { controller = "Admin", action = "Login" }
               );
                routes.MapRoute(
                   name: "datosTiendaVirtual",
                   url: "admin/datostienda",
                   defaults: new { controller = "Admin", action = "DatosTienda", id = UrlParameter.Optional }
               );
                routes.MapRoute(
                    name: "checkoutfactura",
                    url: "admin/prepayment/{action}/{idplantienda}",
                    defaults: new { controller = "Admin", action = "pagarfactura", idplantienda = UrlParameter.Optional }
                );
                routes.MapRoute(
                    name: "procesarpago",
                    url: "{controller}/postpayment/{action}/{idplantienda}/{tokenculqi}/{email}",
                    defaults: new { controller = "Admin", action = "procesarPago", idplantienda = UrlParameter.Optional, tokenculqi = UrlParameter.Optional, email = UrlParameter.Optional }
                );
            #endregion


            
            
        }
    }
}