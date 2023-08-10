using Domain.EntitiesLogic;
using Infraestruture.CrossCutting;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.ProductoCaracteristica
{
    public class ProductoCaracteristicaRepository : IProductoCaracteristicaRepository
    {
        public List<ProductoCaracteristicaEL> getCaracteristicaPorProducto(long codigoProducto)
        {
            List<ProductoCaracteristicaEL> lista = new List<ProductoCaracteristicaEL>();
            ProductoCaracteristicaEL item = null;
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_PRODUCTO_CARACTERISTICA");
            oDatabase.AddInParameter(oDbCommand, "@I_CODIGO_PRODUCTO", DbType.Int64, codigoProducto);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new ProductoCaracteristicaEL();
                    item.I_PRODUCTO_CARACTERISTICA = DataConvert.ToInt64(oReader["I_PRODUCTO_CARACTERISTICA"]);
                    item.I_CODIGO_PRODUCTO = DataConvert.ToInt64(oReader["I_CODIGO_PRODUCTO"]);
                    item.I_CODIGO_CARACTERISTICA = DataConvert.ToInt32(oReader["I_CODIGO_CARACTERISTICA"]);
                    item.V_CARACTERISTICA = DataConvert.ToString(oReader["V_CARACTERISTICA"]);
                    item.V_VALOR = DataConvert.ToString(oReader["V_VALOR"]);
                    
                    lista.Add(item);
                }
                oReader.Close();
            }
            return lista;
        }

        public long Insert(ProductoCaracteristicaEL item)
        {
            long codigoProductoCaracteristica = 0;
            using (var oReader = DatabaseFactory.CreateDatabase().ExecuteReader(
                    "dbo.USP_INS_PRODUCTO_CARACTERISTICA",
                    item.I_PRODUCTO_CARACTERISTICA,
                    item.I_CODIGO_PRODUCTO,
                    item.I_CODIGO_CARACTERISTICA,
                    item.V_CARACTERISTICA,
                    item.V_USER_CREATE))
            {
                while (oReader.Read())
                {
                    codigoProductoCaracteristica = DataConvert.ToInt64(oReader["I_PRODUCTO_CARACTERISTICA"]);
                }
            }

            return codigoProductoCaracteristica;

        }
    }
}
