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

namespace Domain.Repository.Perfil
{
    public class PerfilRepository : IPerfilRepository
    {
        public List<PerfilEN> SelectAllPerfil()
        {
            List<PerfilEN> listReturn = new List<PerfilEN>();

            PerfilEN objeto = null;
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_PerfilAll");

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    objeto = new PerfilEN();
                    objeto.RoleId = DataConvert.ToInt(oReader["RoleId"]);
                    objeto.RoleName = DataConvert.ToString(oReader["RoleName"]);
                    
                    listReturn.Add(objeto);
                }
                oReader.Close();
            }
            return listReturn;
        }

        public PerfilEN SelectPerfilById(int id)
        {
            PerfilEN objeto = null;
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_PerfilById");
            oDatabase.AddInParameter(oDbCommand, "@RoleId", DbType.Int32, id);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    objeto = new PerfilEN();
                    objeto.RoleId = DataConvert.ToInt(oReader["RoleId"]);
                    objeto.RoleName = DataConvert.ToString(oReader["RoleName"]);
                }
                oReader.Close();
            }
            return objeto;
        }

        public void Dispose()
        {
        }
    }
}
