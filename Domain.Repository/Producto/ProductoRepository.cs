using Domain.Entities;
using Domain.EntitiesLogic;
using Infraestruture.CrossCutting;
using Infrastructure.CrossCutting;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Producto
{
    public class ProductoRepository : IProductoRepository
    {
        public List<ProductoEN> SelectAll(ProductoEN item)
        {
            List<ProductoEN> lista = new List<ProductoEN>();
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_PRODUCTO_ALL");
            oDatabase.AddInParameter(oDbCommand, "@V_USER", DbType.String, item.V_USER);
            oDatabase.AddInParameter(oDbCommand, "@I_CODIGO_TIENDA", DbType.Int32, item.I_CODIGO_TIENDA);
            oDatabase.AddInParameter(oDbCommand, "@I_CODIGO_CATEGORIA", DbType.Int32, item._CategoriaEN.I_CODIGO_CATEGORIA);
            oDatabase.AddInParameter(oDbCommand, "@V_TEXT", DbType.String, item.V_PRODUCTO);
            oDatabase.AddInParameter(oDbCommand, "@B_ACTIVE", DbType.Boolean, item.B_ACTIVE);
            

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new ProductoEN();
                    item.I_CODIGO_PRODUCTO = DataConvert.ToInt64(oReader["I_CODIGO_PRODUCTO"]);
                    item.V_PRODUCTO = DataConvert.ToString(oReader["V_PRODUCTO"]);
                    item._CategoriaEN.I_CODIGO_CATEGORIA = DataConvert.ToInt(oReader["I_CODIGO_CATEGORIA"]);
                    item._CategoriaEN.V_CATEGORIA = DataConvert.ToString(oReader["V_CATEGORIA"]);
                    item.I_CODIGO_TIENDA = DataConvert.ToInt(oReader["I_CODIGO_TIENDA"]);
                    item.N_PRECIO = DataConvert.ToDecimal(oReader["N_PRECIO"]);
                    item.I_STOCK = DataConvert.ToInt(oReader["I_STOCK"]);
                    item.V_CODIGO_BARRA = DataConvert.ToString(oReader["V_CODIGO_BARRA"]);
                    item.V_URL_PRINCIPAL = DataConvert.ToString(oReader["V_URL_PRINCIPAL"]);
                    item.V_DESCRIPCION = DataConvert.ToString(oReader["V_DESCRIPCION"]);
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

        public List<ProductoEN> BuscarProductos(ProductoEN item)
        {
            List<ProductoEN> lista = new List<ProductoEN>();
            
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_PRODUCTO");
            oDatabase.AddInParameter(oDbCommand, "@I_CODIGO_PRODUCTO", DbType.Int32, item.I_CODIGO_PRODUCTO);
            oDatabase.AddInParameter(oDbCommand, "@I_CODIGO_TIENDA", DbType.Int32, item.I_CODIGO_TIENDA);
            oDatabase.AddInParameter(oDbCommand, "@V_PRODUCTO", DbType.String, item.V_PRODUCTO);
            oDatabase.AddInParameter(oDbCommand, "@B_ACTIVE", DbType.Boolean, item.B_ACTIVE);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    var obj = new ProductoEN();
                    obj.I_CODIGO_PRODUCTO = DataConvert.ToInt64(oReader["I_CODIGO_PRODUCTO"]);
                    obj.V_PRODUCTO = DataConvert.ToString(oReader["V_PRODUCTO"]);
                    obj.I_CODIGO_CATEGORIA = DataConvert.ToInt(oReader["I_CODIGO_CATEGORIA"]);
                    obj.I_CODIGO_TIENDA = DataConvert.ToInt(oReader["I_CODIGO_TIENDA"]);
                    obj.N_PRECIO = DataConvert.ToDecimal(oReader["N_PRECIO"]);
                    obj.I_STOCK = DataConvert.ToInt(oReader["I_STOCK"]);
                    obj.V_CODIGO_BARRA = DataConvert.ToString(oReader["V_CODIGO_BARRA"]);
                    obj.B_ACTIVE = DataConvert.ToBool(oReader["B_ACTIVE"]);
                    obj.D_DATE_CREATE = DataConvert.ToDateTime(oReader["D_DATE_CREATE"]);
                    obj.V_USER_CREATE = DataConvert.ToString(oReader["V_USER_CREATE"]);
                    obj.D_DATE_UPDATE = DataConvert.ToDateTimeNull(oReader["D_DATE_UPDATE"]);
                    obj.V_USER_UPDATE = DataConvert.ToStringNull(oReader["V_USER_UPDATE"]);
                    lista.Add(obj);
                }
                oReader.Close();
            }
            return lista;
        }

        public long Insert(ProductoEN item)
        {
            long codigoProducto = 0;
            using (var oReader = DatabaseFactory.CreateDatabase().ExecuteReader(
                    "dbo.USP_INS_PRODUCTO",
                    item.V_PRODUCTO,
                    item.I_CODIGO_CATEGORIA,
                    item.I_CODIGO_TIENDA,
                    item.N_PRECIO,
                    item.I_STOCK,
                    item.V_CODIGO_BARRA,
                    item.V_USER_CREATE))
            {
                while (oReader.Read())
                {
                    codigoProducto = DataConvert.ToInt64(oReader["I_CODIGO_PRODUCTO"]);
                }
            }

            return codigoProducto;
           
        }

        public void Delete(ProductoEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_DEL_Producto",
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

        public void Update(ProductoEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_UPD_Producto",
                    item.I_CODIGO_PRODUCTO,
                    item.V_PRODUCTO,
                    item.I_CODIGO_CATEGORIA,
                    item.I_CODIGO_TIENDA,
                    item.N_PRECIO,
                    item.I_STOCK,
                    item.V_CODIGO_BARRA,
                    item.V_USER_UPDATE
                  );

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ProductoEL SelectDetalle(ProductoEN item)
        {
            ProductoEL obj = new ProductoEL();
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_PRODUCTO_DETALLE");
            oDatabase.AddInParameter(oDbCommand, "@I_CODIGO_PRODUCTO", DbType.Int32, item.I_CODIGO_PRODUCTO);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new ProductoEL();
                    obj.I_CODIGO_PRODUCTO = DataConvert.ToInt64(oReader["I_CODIGO_PRODUCTO"]);
                    obj.V_PRODUCTO = DataConvert.ToString(oReader["V_PRODUCTO"]);
                    obj.I_CODIGO_CATEGORIA = DataConvert.ToInt(oReader["I_CODIGO_CATEGORIA"]);
                    obj.I_CODIGO_TIENDA = DataConvert.ToInt(oReader["I_CODIGO_TIENDA"]);
                    obj.N_PRECIO = DataConvert.ToDecimal(oReader["N_PRECIO"]);
                    obj.I_STOCK = DataConvert.ToInt(oReader["I_STOCK"]);
                    obj.V_CODIGO_BARRA = DataConvert.ToString(oReader["V_CODIGO_BARRA"]);
                    obj.V_CATEGORIA = DataConvert.ToString(oReader["V_CATEGORIA"]);
                    obj.V_TIENDA = DataConvert.ToString(oReader["V_TIENDA"]);
                }
                oReader.Close();
            }
            return obj;
        }
        public List<ProductoEL> getProductoDetalleCompra(long codigoCompra)
        {
            List<ProductoEL> item = new List<ProductoEL>();
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_DETALLECOMPRA_PRODUCTO");
            oDatabase.AddInParameter(oDbCommand, "@I_CODIGO_COMPRA", DbType.Int32, codigoCompra);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    var obj = new ProductoEL();
                    obj._detalleCompraEN = new DetalleCompraEN();
                    obj._detalleCompraEN.codigoDetalle = DataConvert.ToInt32(oReader["I_CODIGO_DETALLE"]);
                    obj._detalleCompraEN.codigoCompra = DataConvert.ToInt32(oReader["I_CODIGO_COMPRA"]);
                    obj._detalleCompraEN.codigoProducto = DataConvert.ToInt32(oReader["I_CODIGO_PRODUCTO"]);
                    obj._detalleCompraEN.precio = DataConvert.ToDecimal(oReader["N_PRECIO"]);
                    obj._detalleCompraEN.cantidad = DataConvert.ToInt32(oReader["I_CANTIDAD"]);
                    obj._detalleCompraEN.total = DataConvert.ToDecimal(oReader["I_TOTAL"]);
                    obj.I_CODIGO_PRODUCTO = DataConvert.ToInt64(oReader["I_CODIGO_PRODUCTO"]);
                    obj.V_PRODUCTO = DataConvert.ToString(oReader["V_PRODUCTO"]);
                    obj.I_CODIGO_CATEGORIA = DataConvert.ToInt(oReader["I_CODIGO_CATEGORIA"]);
                    obj.I_CODIGO_TIENDA = DataConvert.ToInt(oReader["I_CODIGO_TIENDA"]);
                    obj.N_PRECIO = DataConvert.ToDecimal(oReader["N_PRECIO"]);
                    obj.I_STOCK = DataConvert.ToInt(oReader["I_STOCK"]);
                    obj.V_CODIGO_BARRA = DataConvert.ToString(oReader["V_CODIGO_BARRA"]);
                    obj.V_CATEGORIA = DataConvert.ToString(oReader["V_CATEGORIA"]);
                    obj.V_TIENDA = DataConvert.ToString(oReader["V_TIENDA"]);
                    obj.V_URL_PRINCIPAL = DataConvert.ToString(oReader["V_URL_PRINCIPAL"]);
                    item.Add(obj);
                }
                oReader.Close();
            }
            return item;
        }
    }
}
