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

namespace Domain.Repository.Categoria
{
    public class CategoriaRepository : ICategoriaRepository
    {
        public List<CategoriaEN> SelectAll(CategoriaEN item)
        {
            List<CategoriaEN> listReturn = new List<CategoriaEN>();

            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_CATEGORIA_ALL");
            oDatabase.AddInParameter(oDbCommand, "@V_USER", DbType.String, item.V_USER);
            oDatabase.AddInParameter(oDbCommand, "@I_CODIGO_CATEGORIA", DbType.Int32, item.I_CODIGO_CATEGORIA);
            oDatabase.AddInParameter(oDbCommand, "@I_CATEGORIA_PADRE", DbType.Int32, item.I_CATEGORIA_PADRE);
            oDatabase.AddInParameter(oDbCommand, "@IDTIENDA", DbType.Int32, item.I_CODIGO_TIENDA);
            oDatabase.AddInParameter(oDbCommand, "@B_ACTIVE", DbType.Boolean, item.B_ACTIVE);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new CategoriaEN();
                    item.I_CODIGO_CATEGORIA = DataConvert.ToInt32(oReader["I_CODIGO_CATEGORIA"]);
                    item.V_CATEGORIA = DataConvert.ToString(oReader["V_CATEGORIA"]);
                    item.I_CODIGO_TIENDA = DataConvert.ToInt32(oReader["I_CODIGO_TIENDA"]);
                    item.V_URL = DataConvert.ToString(oReader["V_URL"]);
                    item.I_CATEGORIA_PADRE = DataConvert.ToInt32(oReader["I_CATEGORIA_PADRE"]);
                    item.V_CATEGORIA_PADRE = DataConvert.ToString(oReader["V_CATEGORIA_PADRE"]);
                    item.B_ACTIVE = DataConvert.ToBool(oReader["B_ACTIVE"]);
                    item.V_TIENDA = DataConvert.ToString(oReader["V_TIENDA"]);
                    item.D_DATE_CREATE = DataConvert.ToDateTime(oReader["D_DATE_CREATE"]);
                    item.D_DATE_UPDATE = DataConvert.ToDateTimeNull(oReader["D_DATE_UPDATE"]);

                    listReturn.Add(item);
                }
                oReader.Close();
            }
            return listReturn;
        }

        public CategoriaEN Select(CategoriaEN item)
        {
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_CATEGORIA");
            oDatabase.AddInParameter(oDbCommand, "@I_CODIGO_CATEGORIA", DbType.Int32, item.I_CODIGO_CATEGORIA);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new CategoriaEN();
                    item.I_CODIGO_CATEGORIA = DataConvert.ToInt32(oReader["I_CODIGO_CATEGORIA"]);
                    item.V_CATEGORIA = DataConvert.ToString(oReader["V_CATEGORIA"]);
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

        public void Insert(CategoriaEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_INS_CATEGORIA",
                    item.I_CODIGO_CATEGORIA,
                    item.V_CATEGORIA,
                    item.I_CATEGORIA_PADRE,
                    item.V_USER_CREATE,
                    item.B_ACTIVE
                  );

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(CategoriaEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_DEL_CATEGORIA",
                      item.I_CODIGO_CATEGORIA,
                      item.V_USER_UPDATE
                  );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(CategoriaEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_UPD_CATEGORIA",
                    item.I_CODIGO_CATEGORIA,
                    item.V_CATEGORIA,
                    item.B_ACTIVE,
                    item.V_USER_CREATE
                  );

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
