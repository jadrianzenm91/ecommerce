using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Presentacion.Web.MVC.Controllers
{
    public class CompraController : Controller
    {
        //
        // GET: /Buy/

        public ActionResult Index(string id)
        {
            return View();
        }

    }
}
