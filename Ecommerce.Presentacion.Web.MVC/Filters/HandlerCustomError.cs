using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace Filters
{
    /// <summary>
    /// Exclusivos para acciones de MVC
    /// </summary>
    public class HandlerCustomError : HandleErrorAttribute
    {
        protected static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnException(ExceptionContext filterContext)
        {
            var mensaje = string.Format("Controller: {0}, Action {1}, Exception: {2}, Hora: {3}",
                           filterContext.Controller,
                           filterContext.RouteData.Values["action"].ToString(),
                           filterContext.Exception,
                           DateTime.Now.ToLongTimeString());

            log.Error(mensaje);

            var result = new ViewResult { ViewName = "Error" };

            result.ViewBag.ErrorDescription = filterContext.Exception.Message;

            filterContext.Result = result;

            filterContext.ExceptionHandled = true;
        }
    }
    /// <summary>
    /// Exclusivos para metodos web API
    /// </summary>
    public class NotImplExceptionFilterAttribute : ExceptionFilterAttribute
    {
        protected static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnException(HttpActionExecutedContext context)
        {
            log.Error("ERROR", context.Exception);
        }
    }
}