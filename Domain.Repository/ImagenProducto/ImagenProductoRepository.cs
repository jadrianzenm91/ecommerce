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

namespace Domain.Repository.ImagenProducto
{
    public class ImagenProductoRepository : IImagenProductoRepository
    {
        public List<ImagenProductoEN> SelectByProducto(long idproducto)
        {
            List<ImagenProductoEN> lista = new List<ImagenProductoEN>();
            ImagenProductoEN item = null;
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_IMAGENPRODUCTO_ALL");
            oDatabase.AddInParameter(oDbCommand, "@I_CODIGO_PRODUCTO", DbType.Int64, idproducto);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new ImagenProductoEN();
                    item.I_CODIGO_PRODUCTO = DataConvert.ToInt64(oReader["I_CODIGO_PRODUCTO"]);
                    item.V_URL = DataConvert.ToString(oReader["V_URL"]);
                    item.I_CODIGO_IMAGEN = DataConvert.ToInt64(oReader["I_CODIGO_IMAGEN"]);
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

        public long Insert(ImagenProductoEN item)
        {
            long codigoImagen = 0;
            using (var oReader = DatabaseFactory.CreateDatabase().ExecuteReader(
                    "dbo.USP_INS_IMAGENPRODUCTO",
                item.V_URL,
                item.I_CODIGO_PRODUCTO,
                item.V_USER_CREATE))
            {
                while (oReader.Read())
                {
                    codigoImagen = DataConvert.ToInt64(oReader["I_CODIGO_IMAGEN"]);
                }
            }

            return codigoImagen;
        }

        public void Delete(ImagenProductoEN item)
        {
            DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_DEL_IMAGENPRODUCTO",
                      item.I_CODIGO_PRODUCTO,
                      item.V_USER_UPDATE
                  );
        }
    }
}
