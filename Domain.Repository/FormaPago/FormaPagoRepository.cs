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

namespace Domain.Repository.FormaPago
{
    public class FormaPagoRepository : IFormaPagoRepository
    {
        public List<FormaPagoEN> SelectAll()
        {
            List<FormaPagoEN> listReturn = new List<FormaPagoEN>();

            FormaPagoEN item = null;
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_FORMA_PAGO_ALL");

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new FormaPagoEN();
                    item.I_CODIGO_FORMA_PAGO = DataConvert.ToInt32(oReader["I_CODIGO_FORMA_PAGO"]);
                    item.V_FORMA_PAGO = DataConvert.ToString(oReader["V_FORMA_PAGO"]);
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

        public FormaPagoEN Select(FormaPagoEN item)
        {
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_FORMA_PAGO");
            oDatabase.AddInParameter(oDbCommand, "@I_CODIGO_FORMA_PAGO", DbType.Int32, item.I_CODIGO_FORMA_PAGO);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new FormaPagoEN();
                    item.I_CODIGO_FORMA_PAGO = DataConvert.ToInt32(oReader["I_CODIGO_FORMA_PAGO"]);
                    item.V_FORMA_PAGO = DataConvert.ToString(oReader["V_FORMA_PAGO"]);
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

        public void Insert(FormaPagoEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_INS_FORMA_PAGO",
                    item.V_FORMA_PAGO,
                    item.B_ACTIVE,
                    item.V_USER_CREATE
                  );

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(FormaPagoEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_DEL_FORMA_PAGO",
                      item.I_CODIGO_FORMA_PAGO,
                      item.V_USER_UPDATE
                  );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(FormaPagoEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_UPD_FORMA_PAGO",
                    item.I_CODIGO_FORMA_PAGO,
                    item.V_FORMA_PAGO,
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
