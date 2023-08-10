using Domain.Entities;
using Domain.Repository.Caracteristica;
using Infraestruture.CrossCutting;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Caracteristica
{
    public class CaracteristicaRepository : ICaracteristicaRepository
    {
        public List<CaracteristicaEN> SelectAll()
        {
            List<CaracteristicaEN> listReturn = new List<CaracteristicaEN>();

            CaracteristicaEN item = null;
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_CARACTERISTICA_ALL");

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new CaracteristicaEN();
                    item.I_CODIGO_CARACTERISTICA = DataConvert.ToInt32(oReader["I_CODIGO_CARACTERISTICA"]);
                    item.V_CARACTERISTICA = DataConvert.ToString(oReader["V_CARACTERISTICA"]);
                    item.I_CODIGO_CATEGORIA = DataConvert.ToInt(oReader["I_CODIGO_CATEGORIA"]);
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

        public CaracteristicaEN Select(CaracteristicaEN item)
        {
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_CARACTERISTICA");
            oDatabase.AddInParameter(oDbCommand, "@I_CODIGO_CARACTERISTICA", DbType.Int32, item.I_CODIGO_CARACTERISTICA);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new CaracteristicaEN();
                    item.I_CODIGO_CARACTERISTICA = DataConvert.ToInt32(oReader["I_CODIGO_CARACTERISTICA"]);
                    item.V_CARACTERISTICA = DataConvert.ToString(oReader["V_CARACTERISTICA"]);
                    item.I_CODIGO_CATEGORIA = DataConvert.ToInt(oReader["I_CODIGO_CATEGORIA"]);
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

        public void Insert(CaracteristicaEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_INS_CARACTERISTICA",
                    item.V_CARACTERISTICA,
                    item.I_CODIGO_CATEGORIA,
                    item.B_ACTIVE,
                    item.V_USER_CREATE
                  );

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(CaracteristicaEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_DEL_CARACTERISTICA",
                      item.I_CODIGO_CARACTERISTICA,
                      item.V_USER_UPDATE
                  );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(CaracteristicaEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_UPD_CARACTERISTICA",
                    item.I_CODIGO_CARACTERISTICA,
                    item.V_CARACTERISTICA,
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
