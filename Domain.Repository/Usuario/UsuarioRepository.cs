using Domain.Entities;
using Infraestruture.CrossCutting;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Usuario
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public List<UsuarioEN> SelectAll()
        {
            List<UsuarioEN> listReturn = new List<UsuarioEN>();

            UsuarioEN item = null;
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_USUARIO_ALL");

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new UsuarioEN();
                    item.IdUsuario = DataConvert.ToInt(oReader["IdUsuario"]);
                    item.V_NOMBRES = DataConvert.ToString(oReader["V_NOMBRES"]);
                    item.V_APELLIDOS = DataConvert.ToString(oReader["V_APELLIDOS"]);
                    item.V_DNI = DataConvert.ToString(oReader["V_DNI"]);
                    item.V_EMAIL = DataConvert.ToString(oReader["V_EMAIL"]);
                    item.I_CELULAR = DataConvert.ToString(oReader["I_CELULAR"]);
                    item.V_DIRECCION = DataConvert.ToString(oReader["V_DIRECCION"]);
                    item.B_ACTIVE = DataConvert.ToBool(oReader["B_ACTIVE"]);
                    item.D_DATE_CREATE = DataConvert.ToDateTime(oReader["D_DATE_CREATE"]);
                    item.V_USER_CREATE = DataConvert.ToString(oReader["V_USER_CREATE"]);
                    item.D_DATE_UPDATE = DataConvert.ToDateTimeNull(oReader["D_DATE_UPDATE"]);
                    item.V_USER_UPDATE = DataConvert.ToStringNull(oReader["V_USER_UPDATE"]);
                    listReturn.Add(item);
                }
                oReader.Close();
            }
            return listReturn;
        }

        public UsuarioEN Select(UsuarioEN item)
        {
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_USUARIO");
            oDatabase.AddInParameter(oDbCommand, "@V_USUARIO", DbType.String, item.V_USUARIO);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new UsuarioEN();
                    item.IdUsuario = DataConvert.ToInt(oReader["IdUsuario"]);
                    item.V_NOMBRES = DataConvert.ToString(oReader["V_NOMBRES"]);
                    item.V_APELLIDOS = DataConvert.ToString(oReader["V_APELLIDOS"]);
                    item.V_DNI = DataConvert.ToString(oReader["V_DNI"]);
                    item.V_EMAIL = DataConvert.ToString(oReader["V_EMAIL"]);
                    item.I_CELULAR = DataConvert.ToString(oReader["I_CELULAR"]);
                    item.V_DIRECCION = DataConvert.ToString(oReader["V_DIRECCION"]);
                    item.V_DPTO = DataConvert.ToString(oReader["V_DPTO"]);
                    item.V_PROV = DataConvert.ToString(oReader["V_PROV"]);
                    item.V_DISTR = DataConvert.ToString(oReader["V_DISTR"]);
                    item.B_ACTIVE = DataConvert.ToBool(oReader["B_ACTIVE"]);
                    item.D_DATE_CREATE = DataConvert.ToDateTime(oReader["D_DATE_CREATE"]);
                    item.V_USER_CREATE = DataConvert.ToString(oReader["V_USER_CREATE"]);
                    item.D_DATE_UPDATE = DataConvert.ToDateTimeNull(oReader["D_DATE_UPDATE"]);
                    item.V_USER_UPDATE = DataConvert.ToStringNull(oReader["V_USER_UPDATE"]);
                }
                oReader.Close();
            }
            return item;
        }
        public UsuarioEN getUsuarioTienda(UsuarioEN item)
        {
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_USUARIOTIENDA");
            oDatabase.AddInParameter(oDbCommand, "@V_USUARIO", DbType.String, item.V_USUARIO);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new UsuarioEN();
                    item.IdUsuario = DataConvert.ToInt(oReader["IdUsuario"]);
                    item.V_NOMBRES = DataConvert.ToString(oReader["V_NOMBRES"]);
                    item.V_APELLIDOS = DataConvert.ToString(oReader["V_APELLIDOS"]);
                    item.V_DNI = DataConvert.ToString(oReader["V_DNI"]);
                    item.V_EMAIL = DataConvert.ToString(oReader["V_EMAIL"]);
                    item.I_CELULAR = DataConvert.ToString(oReader["I_CELULAR"]);
                    item.V_DIRECCION = DataConvert.ToString(oReader["V_DIRECCION"]);
                    item.B_ACTIVE = DataConvert.ToBool(oReader["B_ACTIVE"]);
                    item._TiendaEN.I_CODIGO_TIENDA = DataConvert.ToInt(oReader["I_CODIGO_TIENDA"]);
                    item._TiendaEN.V_TIENDA = DataConvert.ToString(oReader["V_TIENDA"]);
                    item._TiendaEN.V_CARPETAIMAG = DataConvert.ToString(oReader["V_CARPETAIMAG"]);
                    item.D_DATE_CREATE = DataConvert.ToDateTime(oReader["D_DATE_CREATE"]);
                    item.V_USER_CREATE = DataConvert.ToString(oReader["V_USER_CREATE"]);
                    item.D_DATE_UPDATE = DataConvert.ToDateTimeNull(oReader["D_DATE_UPDATE"]);
                    item.V_USER_UPDATE = DataConvert.ToStringNull(oReader["V_USER_UPDATE"]);
                }
                oReader.Close();
            }
            return item;
        }
        public void Insert(UsuarioEN item)
        {
            long IdUsuario = 0;

            try
            {
                using (var oReader = DatabaseFactory.CreateDatabase().ExecuteReader(
                    "dbo.USP_INS_USUARIO",
                        item.IdUsuario,
                        item.V_NOMBRES,
                        item.V_APELLIDOS,
                        item.V_DNI,
                        item.V_EMAIL,
                        item.I_CELULAR,
                        item.V_DIRECCION,
                        item.B_ACTIVE,
                        item.V_USER_UPDATE)
                       )
                {
                    while (oReader.Read())
                    {
                        IdUsuario = DataConvert.ToInt64(oReader["IdUsuario"]);
                    }    
                        
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public tokenUsuarioEN createToken(tokenUsuarioEN item)
        {
            tokenUsuarioEN _tokenUsuarioEN = new tokenUsuarioEN();

            using (var oReader = DatabaseFactory.CreateDatabase().ExecuteReader(
                "dbo.USP_INS_TOKEN")
                    )
            {
                while (oReader.Read())
                {
                    _tokenUsuarioEN.token = DataConvert.ToString(oReader["token"]);
                }

            }
            return _tokenUsuarioEN;
        }

        public void Delete(UsuarioEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_DEL_USUARIO",
                      item.IdUsuario,
                      item.V_USER_UPDATE
                  );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(UsuarioEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_UPD_USUARIO",
                    item.V_USER,
                    item.V_NOMBRES,
                    item.V_APELLIDOS,
                    item.V_DNI,
                    item.I_CELULAR,
                    item.V_DPTO,
                    item.V_PROV,
                    item.V_DISTR,
                    item.V_DIRECCION
                  );

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public long InsertProviderAuth(UsuarioEN item)
        {
            long IdUsuario = 0;
            try
            {
                using (var oReader = DatabaseFactory.CreateDatabase().ExecuteReader(
                
                    "dbo.USP_INS_PROVIDER_AUTH",
                    item.V_UID_PROVIDER,
                    item.V_NOMBRES,
                    item.V_EMAIL,
                    item.V_PROVIDER,
                    item.V_PHOTO_URL,
                    item.V_USER_CREATE
                  ))
               {
                    while (oReader.Read())
                    {
                        IdUsuario = DataConvert.ToInt64(oReader["IdUsuario"]);
                    }    
                        
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return IdUsuario;
        }

        public UsuarioEN confirmationToken(UsuarioEN item)
        {
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_CONFIRMATIONTOKEN");
            oDatabase.AddInParameter(oDbCommand, "@V_USUARIO", DbType.String, item.V_USUARIO);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new UsuarioEN();
                    item.IdUsuario = DataConvert.ToInt(oReader["IdUsuario"]);
                    item.V_NOMBRES = DataConvert.ToString(oReader["V_NOMBRES"]);
                    item.V_EMAIL = DataConvert.ToString(oReader["V_EMAIL"]);
                    item.confirmationToken = DataConvert.ToStringNull(oReader["confirmationToken"]);
                    item.isConfirmation = DataConvert.ToBoolNull(oReader["IsConfirmed"]);
                }
                oReader.Close();
            }
            return item;
        }
    }
}
