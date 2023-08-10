using Manager.Application;
using Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Infrastructure.CrossCutting;
using Domain.EntitiesLogic;

namespace Ecommerce.Presentacion.Web.MVC.Controllers
{
    [LoggingFilter]
    [HandlerCustomError]
    public class HomeController : Controller
    {
        IManagerApplication _ManagerApplication = new ManagerApplication();

        public ActionResult index()
        {
            return View();
        }
        public ActionResult nosotros()
        {
            return View();
        }
        public ActionResult servicios()
        {
            return View();
        }
        public ActionResult contacto()
        {
            return View();
        }
    }
}
