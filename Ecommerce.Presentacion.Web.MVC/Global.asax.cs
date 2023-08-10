using Infrastructure.CrossCutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;

namespace Ecommerce.Presentacion.Web.MVC
{
    // Nota: para obtener instrucciones sobre cómo habilitar el modo clásico de IIS6 o IIS7, 
    // visite http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            #region Log4Net

            log4net.Config.XmlConfigurator.Configure();

            #endregion
            
            #region Crear_Autenticacion
            /* CONSIDERACIONES:
             * 1. Verificar la versión de la DDL importado con el web.config
             * 2. Verificar la copia local habilitada para las DDL importadas:
             *      System.Web.Security;
             *      WebMatrix.WebData;
             * 3. Se debe inicializar el WebSecurity
             * 4. Sentencia autoCreateTables: false cuando la tabla (USUARIO) ya esta creada, caso contrario igual a true.
             * 5. Sentencia autoCreateTables: false hace que no se cree la tabla dbo.webpages_UsersInRoles automáticamente,
             *    por lo tanto se debe generar un script para crear dos columnas UserId y RoleId y sus Foraneas correspondientes.
             */
            if (!WebSecurity.Initialized)
            {
                //creamos nuestra base de datos
                WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                    "USUARIO",//nombre de la tabla
                    "IdUsuario",
                    "V_USUARIO",
                    autoCreateTables: false);

                var menbershipProvider =
                    (SimpleMembershipProvider)Membership.Provider;

                var RoleProvider = (SimpleRoleProvider)Roles.Provider;

                ///crear un rol admin
                if (!RoleProvider.RoleExists(Infrastructure.CrossCutting.Constantes.Perfiles.Administrador))
                    RoleProvider.CreateRole(Infrastructure.CrossCutting.Constantes.Perfiles.Administrador);

                //crear el usuario
                if (menbershipProvider.GetUser(Infrastructure.CrossCutting.Constantes.Login.User, false) == null)//false = pregunta si importa si el usuario esta conectado
                    WebSecurity.CreateUserAndAccount(Infrastructure.CrossCutting.Constantes.Login.User, Infrastructure.CrossCutting.Constantes.Login.Password, new
                    {
                        V_NOMBRES = "",
                        V_APELLIDOS = Constantes.EMPRESA.RAZON_SOCIAL,
                        V_DNI = Constantes.EMPRESA.RUC,
                        V_EMAIL = Constantes.EMPRESA.EMAIL,
                        V_USER_CREATE = Infrastructure.CrossCutting.Constantes.Login.User,
                        D_DATE_CREATE = DateTime.Now,
                        B_ACTIVE = 1
                    });

                //asignar rol - usuario
                if (!RoleProvider.GetRolesForUser(Infrastructure.CrossCutting.Constantes.Login.User).Contains(Infrastructure.CrossCutting.Constantes.Perfiles.Administrador))
                    RoleProvider.AddUsersToRoles(new string[] { Infrastructure.CrossCutting.Constantes.Login.User }, new string[] { Infrastructure.CrossCutting.Constantes.Perfiles.Administrador });

            }

            #endregion
        }
    }
}