using Infrastructure.CrossCutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentacionWEB.Models
{
    public class UserLogin
    {
        public string S_LOGIN;
        public int ID_USER;
        public string S_FULL_NAME;
        public List<VMPermiso> PermissionList;

        public UserLogin()
        {
            PermissionList = new List<VMPermiso>();
        }
    }
    public class VMPermiso
    {
        public string S_OPCION { get; set; }
        public int N_ACCION { get; set; }
        public string S_ACCION { get; set; }

        public static List<VMPermiso> getPermisos()
        {
            List<VMPermiso> Permisos = (List<VMPermiso>)HttpContext.Current.Session[Constantes.SessionKey.Permisos];
            if (Permisos == null)
            {
                Permisos = new List<VMPermiso>();
            }
            return Permisos;
        }
    }
}