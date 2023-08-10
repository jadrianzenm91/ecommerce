using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Filters
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        protected static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var mensaje = string.Format("Controller: {0}, Action {1}, Fecha y Hora: {2}, Usuario: {3}",
                            filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                            filterContext.ActionDescriptor.ActionName,
                            DateTime.Now.ToLongDateString(),
                            HttpContext.Current.User.Identity.Name);


            log.Debug(mensaje);
        }
    }
}