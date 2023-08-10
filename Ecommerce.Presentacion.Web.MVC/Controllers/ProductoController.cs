using Domain.Entities;
using Domain.EntitiesLogic;
using Filters;
using Infrastructure.CrossCutting;
using Manager.Application;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Presentacion.Web.MVC.Controllers
{
    [Authorize(Roles = Constantes.Perfiles.Administrador + "," + Constantes.Perfiles.Empresa)]
    [LoggingFilter]
    [HandlerCustomError]
    public class ProductoController : Controller
    {
        [LoggingFilter]
        [HandlerCustomError]
        public ActionResult Index(string id)
        {
            ViewBag.idproducto= id;
            return View();
        }
       
        [HttpPost]
        public JsonResult setProducto(ProductoEL item)
        {
            IManagerApplication _ManagerApplication = new ManagerApplication();
            item.V_USER_CREATE = User.Identity.Name;
            var response = _ManagerApplication.InsertProduct(item);
            return Json(new
            {
                data = response
            });
            
        }
        [AllowAnonymous]
        public JsonResult listProductos(int idtienda, int idcategoria)
        {
            var result = new List<ProductoEN>();

            IManagerApplication _ManagerApplication = new ManagerApplication();
            ProductoEN item = new ProductoEN();
            item.I_CODIGO_TIENDA = idtienda;
            item._CategoriaEN.I_CODIGO_CATEGORIA = idcategoria;
            item.B_ACTIVE = true;
            result = _ManagerApplication.SelectAllProduct(item);

            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [AllowAnonymous]
        public JsonResult getProducto(string id)
        {
            var result = new ProductoEL();
            var idDecryptKey = Encrypting.DecryptKey(id);

            IManagerApplication _ManagerApplication = new ManagerApplication();
            result = _ManagerApplication.getProductoDetalle(Convert.ToInt64(idDecryptKey));

            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [AllowAnonymous]
        public JsonResult deleteProducto(string id, bool active)
        {
            var item = new ProductoEN();
            var idDecryptKey = Encrypting.DecryptKey(id);
            item.B_ACTIVE = active;
            item.I_CODIGO_PRODUCTO = Convert.ToInt64(idDecryptKey);

            IManagerApplication _ManagerApplication = new ManagerApplication();
            var response = _ManagerApplication.DeleteProduct(item);
            return new JsonResult
            {
                Data = response,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        [HttpPost]
        public ActionResult UploadBeginHTML(HttpPostedFileBase file)
        {
            var path = String.Empty;
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                path = Path.Combine(Server.MapPath("~/UploadTemporal/"), fileName);
                file.SaveAs(path);
            }

            return new JsonResult { Data = path, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        
        [HttpPost]
        public JsonResult Upload()
        {
            JsonResult result = new JsonResult();
            var path = String.Empty;
            //HttpPostedFileBase file = Request.Files[0];
            List<String> paths = new List<String>();
            DateTime dt = DateTime.Now;
            String dtString = dt.ToString("ddMMyyyy_HHmmssfff");
            if (Request.Files[0] != null && Request.Files[0].ContentLength > 0)
            {
                for (int i = 0; i < Request.Files.Count ; i++)
			    {
                    HttpPostedFileBase file = Request.Files[i];
                    var fileName = Path.GetFileName(dtString);
                    path = Path.Combine(Server.MapPath("~/UploadTemporal/"), fileName);
                    file.SaveAs(path);
                    paths.Add(path);
			    }
                
            }
            return result =  Json(new
                {
                    result = paths
                });
        }
    }
}
