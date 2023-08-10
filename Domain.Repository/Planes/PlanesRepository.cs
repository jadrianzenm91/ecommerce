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

namespace Domain.Repository.Planes
{
    public class PlanesRepository : IPlanesRepository
    {

        public List<PlanesEN> SelectAll(PlanesEN item)
        {
            List<PlanesEN> listReturn = new List<PlanesEN>();

            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_PLANES_ALL");
            
            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new PlanesEN();
                    item.idplan = DataConvert.ToInt32(oReader["I_PLAN"]);
                    item.montoplan = DataConvert.ToDecimal(oReader["MONTO_PLAN"]);
                    item.nombreplan = DataConvert.ToString(oReader["NOMBRE"]);
                    item.description = DataConvert.ToString(oReader["DESCRIPCION"]);
                    //item.B_ACTIVE = DataConvert.ToBoolNull(oReader["ESTADO"]);

                    listReturn.Add(item);
                }
                oReader.Close();
            }
            return listReturn;
        }

        public PlanesEN Select(PlanesEN item)
        {
            throw new NotImplementedException();
        }

        public void Insert(PlanesEN item)
        {
            throw new NotImplementedException();
        }

        public void Delete(PlanesEN item)
        {
            throw new NotImplementedException();
        }

        public void Update(PlanesEN item)
        {
            throw new NotImplementedException();
        }
    }
}
