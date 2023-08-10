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

namespace Domain.Repository.PlanTienda
{
    public class PlanTiendaRepository : IPlanTiendaRepository
    {

        public List<PlanTiendaEN> SelectAll(PlanTiendaEN item)
        {
            var lslPlanTienda = new List<PlanTiendaEN>();
             Database oDatabase = DatabaseFactory.CreateDatabase();
             DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_PLANTIENDA_ALL");
             oDatabase.AddInParameter(oDbCommand, "@idtienda", DbType.String, item.idtienda);
             oDatabase.AddInParameter(oDbCommand, "@V_USER", DbType.String, item.V_USER);
             
            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    PlanTiendaEN obj = new PlanTiendaEN();
                    obj.idplantienda = DataConvert.ToInt(oReader["I_PLANTIENDA"]);
                    obj.idplan = DataConvert.ToInt(oReader["I_PLAN"]);
                    obj.montoplan = DataConvert.ToDecimal(oReader["MONTO_PLAN"]);
                    obj.periodoinifac = DataConvert.ToDateTime(oReader["PERIODOINI_FAC"]);
                    obj.periodofinfac = DataConvert.ToDateTime(oReader["PERIODOFIN_FAC"]);
                    obj.fechapago = DataConvert.ToDateTimeNull(oReader["FECHA_PAGO"]);
                    obj.importe = DataConvert.ToDecimal(oReader["IMPORTE"]);
                    obj.idtipo = DataConvert.ToString(oReader["I_TIPO"]);
                    obj.nombreplan = DataConvert.ToString(oReader["NOMPLAN"]);

                    lslPlanTienda.Add(obj);
                }
                oReader.Close();
            }
            return lslPlanTienda;
        }

        public PlanTiendaEN Select(PlanTiendaEN item)
        {
            PlanTiendaEN obj = new PlanTiendaEN();
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_PLANTIENDA");
            oDatabase.AddInParameter(oDbCommand, "@IDPLANTIENDA", DbType.String, item.idplantienda);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    obj.idplantienda = DataConvert.ToInt(oReader["I_PLANTIENDA"]);
                    obj.idplan = DataConvert.ToInt(oReader["I_PLAN"]);
                    obj.montoplan = DataConvert.ToDecimal(oReader["MONTO_PLAN"]);
                    obj.periodoinifac = DataConvert.ToDateTime(oReader["PERIODOINI_FAC"]);
                    obj.periodofinfac = DataConvert.ToDateTime(oReader["PERIODOFIN_FAC"]);
                    obj.fechapago = DataConvert.ToDateTimeNull(oReader["FECHA_PAGO"]);
                    obj.importe = DataConvert.ToDecimal(oReader["IMPORTE"]);
                    obj.idtipo = DataConvert.ToString(oReader["I_TIPO"]);
                    obj.nombreplan = DataConvert.ToString(oReader["NOMPLAN"]);
                }
                oReader.Close();
            }
            return obj;
        }

        public void Insert(PlanTiendaEN item)
        {
            DatabaseFactory.CreateDatabase().ExecuteScalar(
                "dbo.[USP_INS_PLANTIENDA]",
                item.idplantienda,
                item.idplan,
                item.idtipo,
                item.idtienda,
                item.V_USER
                );
        }

        public void Delete(PlanTiendaEN item)
        {
            throw new NotImplementedException();
        }

        public void Update(PlanTiendaEN item)
        {
            DatabaseFactory.CreateDatabase().ExecuteScalar(
            "dbo.USP_UPD_PLANTIENDA",
            item.idplantienda,
            item.V_USER
            );

        }
    }
}
