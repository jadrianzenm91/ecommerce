using Infrastructure.CrossCutting;
using Manager.Application;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace Ecommerce.Presentacion.Web.MVC.Controllers
{
    public class TiendaVirtualController : Controller
    {
        public ActionResult index()
        {
            return View();
        }
        public ActionResult register(string seccion)
        {
            ViewBag.seccion = seccion;
            return View();
        }
        public ActionResult pricing()
        {
            return View();
        }
        public ActionResult confirmation(string token)
        {
            var menbershipProvider =
                  (SimpleMembershipProvider)Membership.Provider;

            WebSecurity.ConfirmAccount(token);

            return View();

        }
        public ActionResult dominio()
        {
            return View();
        }
        #region FormPost
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUserTienda(RegisterModel model)
        {
            IManagerApplication _IManagerApplication = new ManagerApplication();

            if (ModelState.IsValid)
            {
                model.UserName = model.V_EMAIL;
                // Attempt to register the user
                try
                {
                    var menbershipProvider =
                    (SimpleMembershipProvider)Membership.Provider;

                    var RoleProvider = (SimpleRoleProvider)Roles.Provider;

                    var result = WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new
                    {
                        V_NOMBRES = model.V_NOMBRES,
                        V_APELLIDOS = model.V_APELLIDOS,
                        I_CELULAR = model.pais + model.V_TELEFONO,
                        V_EMAIL = model.V_EMAIL,
                        V_USER_CREATE = model.UserName,
                        D_DATE_CREATE = DateTime.Now,
                        V_PROVIDER = Constantes.Web.PLATAFORMA_WEB,
                        B_ACTIVE = 1
                    }, true);

                    //asignar rol - usuario
                    if (!RoleProvider.GetRolesForUser(model.UserName).Contains(Constantes.Perfiles.Empresa))
                        RoleProvider.AddUsersToRoles(new string[] { model.UserName }, new string[] { Constantes.Perfiles.Empresa });

                    WebSecurity.Login(model.UserName, model.Password);
                    _IManagerApplication.sendEmailConfirmacionTienda(model.UserName, Request.Url.GetLeftPart(UriPartial.Authority));
                    return RedirectToAction("register", new { seccion = "11" });
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            return View("register", model);
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
