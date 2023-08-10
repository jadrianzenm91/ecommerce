using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Infrastructure.CrossCutting;
using Filters;
using Models;
using Manager.Application;
using Domain.Entities;
using System.Net.Mail;
using System.Net;

namespace Ecommerce.Presentacion.Web.MVC.Controllers
{
    [LoggingFilter]
    [HandlerCustomError]
    public class AccountController : Controller
    {
        IManagerApplication _IManagerApplication = new ManagerApplication();
        #region LOGIN
       
        //
        // GET: /Account/Login

        //[AllowAnonymous]
        //public ActionResult Login(string returnUrl)
        //{
        //    ViewBag.ReturnUrl = returnUrl;
        //    return View();
        //}
        /*
        /// <summary>
        /// Vista de login para el cliente que realiza la compra.
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult LoginUser(string name, string token)
        {
            ViewBag.name = name;
            ViewBag.token = token; 
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LoginUser(LoginModel model, string returnUrl, string name, string token)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToAction("checkout", "Pedidos", new { name = name, token = token });
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "El usuario y/o clave son incorrectos.");
            return View(model);
        }
        */
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[AllowAnonymous]
        //public ActionResult LogOffUser(string name, string token)
        //{
        //    FormsAuthentication.SignOut();
        //    WebSecurity.Logout();

        //    return RedirectToAction("Index", "Home", new { name = name, token = token });
        //}
      
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(LoginModel model, string returnUrl)
        //{
        //    if (!WebSecurity.IsConfirmed(model.UserName))
        //    {
        //        ModelState.AddModelError("", "La cuenta tiene pendiente de confirmar su registro, revise su correo.");
        //    }
        //    else if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
        //    {
        //        return RedirectToAction("Index", "Admin");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "El usuario y/o clave son incorrectos.");
        //    }
            

        //    // If we got this far, something failed, redisplay form
            
        //    return View(model);
        //}

        //
        // POST: /Account/LogOff

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[AllowAnonymous]
        //public ActionResult LogOff()
        //{
        //    FormsAuthentication.SignOut();
        //    WebSecurity.Logout();
            
        //    return RedirectToAction("Login", "Account");
        //}
        #endregion

        #region TIENDA
        //
        // GET: /Account/Register

        //[AllowAnonymous]
        //public ActionResult RegisterTienda(string seccion)
        //{
        //    ViewBag.seccion = seccion;
        //    return View();
        //}
        //public ActionResult ConfirmAccount(string token)
        //{
        //    var menbershipProvider =
        //            (SimpleMembershipProvider)Membership.Provider;

        //    WebSecurity.ConfirmAccount(token);

        //    return RedirectToAction("RegisterTienda", new { seccion = "22" });
        //}
       
        /// <summary>
        /// Registro de usuario que desea adquirir su tienda virtual.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult RegisterUserTienda(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        model.UserName = model.V_EMAIL;
        //        // Attempt to register the user
        //        try
        //        {
        //            var menbershipProvider =
        //            (SimpleMembershipProvider)Membership.Provider;

        //            var RoleProvider = (SimpleRoleProvider)Roles.Provider;

        //            var result = WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new
        //            {
        //                V_NOMBRES = model.V_NOMBRES,
        //                V_APELLIDOS = model.V_APELLIDOS,
        //                I_CELULAR = model.V_TELEFONO,
        //                V_EMAIL = model.V_EMAIL,
        //                V_USER_CREATE = model.UserName,
        //                D_DATE_CREATE = DateTime.Now,
        //                V_PROVIDER = Constantes.Web.PLATAFORMA_WEB,
        //                B_ACTIVE = 1
        //            }, true);

        //            //asignar rol - usuario
        //            if (!RoleProvider.GetRolesForUser(model.UserName).Contains(Constantes.Perfiles.Empresa))
        //                RoleProvider.AddUsersToRoles(new string[] { model.UserName }, new string[] { Constantes.Perfiles.Empresa });

        //            WebSecurity.Login(model.UserName, model.Password);
        //            _IManagerApplication.sendEmailConfirmacionTienda(model.UserName, Request.Url.GetLeftPart(UriPartial.Authority));
        //            return RedirectToAction("RegisterTienda", new { seccion = "11" });
        //        }
        //        catch (MembershipCreateUserException e)
        //        {
        //            ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
        //        }
        //    }

        //    return RedirectToAction("RegisterTienda");
        //}

        
        #endregion
        /*
        #region USUARIO-COMPRADOR
        //
        // GET: /Account/RegisterUser
        /// <summary>
        /// Registro de un cliente que visito una tienda virtual.
        /// </summary>
        /// <param name="name">Nombre de la tienda</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult RegisterUser(string name, string token)
        {
            ViewBag.name = name;
            ViewBag.token = token; 
            return View();
        }

        /// <summary>
        /// Registro de usuario que desea comprar en tienda virtual.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(RegisterModel model, string name, string token)
        {
           
            if (ModelState.IsValid)
            {
                
                model.UserName = model.V_EMAIL;
                // Attempt to register the user
                try
                {
                    var menbershipProvider =
                    (SimpleMembershipProvider)Membership.Provider;

                    var RoleProvider = (SimpleRoleProvider)Roles.Provider;

                    
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new
                    {
                        V_NOMBRES = model.V_NOMBRES,
                        V_APELLIDOS = model.V_APELLIDOS,
                        V_EMAIL = model.V_EMAIL,
                        V_USER_CREATE = model.UserName,
                        D_DATE_CREATE = DateTime.Now,
                        
                        V_PROVIDER = Constantes.Web.PLATAFORMA_WEB,
                        B_ACTIVE = 1
                    });

                    //asignar rol - usuario
                    if (!RoleProvider.GetRolesForUser(model.UserName).Contains(Constantes.Perfiles.Cliente))
                        RoleProvider.AddUsersToRoles(new string[] { model.UserName }, new string[] { Constantes.Perfiles.Cliente });

                    WebSecurity.Login(model.UserName, model.Password);
                    
                    return RedirectToAction("Index", "Home", new { name = name, token = token });
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            return RedirectToAction("LoginUser", "Account", new { name = name, token = token });
        }

        #endregion
         * */

        #region MANAGER
        
        //
        // GET: /Account/Manage

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                        //actualizar la clave del usuario mediando "Acceso a Datos".
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "El password actual es incorrecto o el nuevo password es inválido.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", String.Format("Unable to create local account. An account with the name \"{0}\" may already exist.", User.Identity.Name));
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #endregion

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

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
