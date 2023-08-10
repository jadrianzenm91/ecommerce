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

namespace Domain.Repository.Promocion
{
    public class PromocionRepository : IPromocionRepository
    {
        public List<PromocionEN> SelectAll()
        {
            List<PromocionEN> listReturn = new List<PromocionEN>();

            PromocionEN item = null;
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_PROMOCION_ALL");

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new PromocionEN();
                    item.I_CODIGO_PROMOCION = DataConvert.ToInt(oReader["I_CODIGO_PROMOCION"]);
                    item.D_FECHA_VENCIMIENTO = DataConvert.ToString(oReader["D_FECHA_VENCIMIENTO"]);
                    item.N_PRECIO = DataConvert.ToDecimal(oReader["N_PRECIO"]);
                    item.I_CODIGO_PRODUCTO = DataConvert.ToInt64(oReader["I_CODIGO_PRODUCTO"]);
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

        public PromocionEN Select(PromocionEN item)
        {
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_PROMOCION");
            oDatabase.AddInParameter(oDbCommand, "@I_CODIGO_PROMOCION", DbType.Int32, item.I_CODIGO_PROMOCION);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new PromocionEN();
                    item.I_CODIGO_PROMOCION = DataConvert.ToInt(oReader["I_CODIGO_PROMOCION"]);
                    item.D_FECHA_VENCIMIENTO = DataConvert.ToString(oReader["D_FECHA_VENCIMIENTO"]);
                    item.N_PRECIO = DataConvert.ToDecimal(oReader["N_PRECIO"]);
                    item.I_CODIGO_PRODUCTO = DataConvert.ToInt64(oReader["I_CODIGO_PRODUCTO"]);
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

        public void Insert(PromocionEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_INS_PROMOCION",
                    item.D_FECHA_VENCIMIENTO,
                    item.N_PRECIO,
                    item.I_CODIGO_PRODUCTO,
                    item.B_ACTIVE,
                    item.V_USER_CREATE
                  );

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(PromocionEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_DEL_PROMOCION",
                      item.I_CODIGO_PROMOCION,
                      item.V_USER_UPDATE
                  );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(PromocionEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_UPD_PROMOCION",
                    item.I_CODIGO_PROMOCION,
                    item.D_FECHA_VENCIMIENTO,
                    item.N_PRECIO,
                    item.I_CODIGO_PRODUCTO,
                    item.B_ACTIVE,
                    item.V_USER_UPDATE
                  );

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
