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

namespace Domain.Repository.ParametroValor
{
    public class ParametroValorRepository : IParametroValorRepository
    {

        public List<ParametroValorEN> SelectAll(Entities.ParametroValorEN item)
        {
            throw new NotImplementedException();
        }

        public ParametroValorEN Select(ParametroValorEN item)
        {
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_PARAMETROVALOR");
            oDatabase.AddInParameter(oDbCommand, "@IDPARAMETRO", DbType.Int32, item.idparametro);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new ParametroValorEN();
                    item.idparametro = DataConvert.ToInt32(oReader["IDPARAMETRO"]);
                    item.idparametrovalor = DataConvert.ToInt32(oReader["IDPARAMETROVALOR"]);
                    item.valor = DataConvert.ToString(oReader["VALOR"]);
                    
                }
                oReader.Close();
            }
            return item;
        }

        public void Insert(ParametroValorEN item)
        {
            throw new NotImplementedException();
        }

        public void Update(ParametroValorEN item)
        {
            throw new NotImplementedException();
        }
    }
}
