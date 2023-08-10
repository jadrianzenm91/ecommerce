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

namespace Domain.Repository.CuponDescuento
{
    public class CuponDescuentoRepository : ICuponDescuentoRepository
    {
        public List<CuponDescuentoEN> SelectAll()
        {
            List<CuponDescuentoEN> lista = new List<CuponDescuentoEN>();
            CuponDescuentoEN item = null;
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_CUPON_ALL");

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new CuponDescuentoEN();
                    item.I_CODIGO_CUPON = DataConvert.ToInt32(oReader["I_CODIGO_CUPON"]);
                    item.D_FECHA_VENCIMIENTO = DataConvert.ToString(oReader["D_FECHA_VENCIMIENTO"]);
                    item.I_CODIGO_CATEGORIA = DataConvert.ToInt32(oReader["I_CODIGO_CATEGORIA"]);
                    item.V_CLAVE_CUPON = DataConvert.ToString(oReader["V_CLAVE_CUPON"]);
                    item.B_ACTIVE = DataConvert.ToBool(oReader["B_ACTIVE"]);
                    item.D_DATE_CREATE = DataConvert.ToDateTime(oReader["D_DATE_CREATE"]);
                    item.V_USER_CREATE = DataConvert.ToString(oReader["V_USER_CREATE"]);
                    item.D_DATE_UPDATE = DataConvert.ToDateTimeNull(oReader["D_DATE_UPDATE"]);
                    item.V_USER_UPDATE = DataConvert.ToStringNull(oReader["V_USER_UPDATE"]);
                    lista.Add(item);
                }
                oReader.Close();
            }
            return lista;
        }

        public CuponDescuentoEN Select(CuponDescuentoEN item)
        {
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_CUPON");
            oDatabase.AddInParameter(oDbCommand, "@I_CODIGO_CUPON", DbType.Int32, item.I_CODIGO_CUPON);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new CuponDescuentoEN();
                    item.I_CODIGO_CUPON = DataConvert.ToInt32(oReader["I_CODIGO_CUPON"]);
                    item.D_FECHA_VENCIMIENTO = DataConvert.ToString(oReader["D_FECHA_VENCIMIENTO"]);
                    item.I_CODIGO_CATEGORIA = DataConvert.ToInt32(oReader["I_CODIGO_CATEGORIA"]);
                    item.V_CLAVE_CUPON = DataConvert.ToString(oReader["V_CLAVE_CUPON"]);
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

        public void Insert(CuponDescuentoEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_INS_CUPON",
                    item.V_CLAVE_CUPON,
                    item.I_CODIGO_CATEGORIA,
                    item.D_FECHA_VENCIMIENTO,
                    item.B_ACTIVE,
                    item.V_USER_CREATE
                  );

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(CuponDescuentoEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_DEL_CUPON",
                      item.I_CODIGO_CUPON,
                      item.V_USER_UPDATE
                  );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(CuponDescuentoEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_UPD_CUPON",
                   item.V_CLAVE_CUPON,
                    item.I_CODIGO_CATEGORIA,
                    item.D_FECHA_VENCIMIENTO,
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
