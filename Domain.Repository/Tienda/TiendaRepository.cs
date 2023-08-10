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

namespace Domain.Repository.Tienda
{
    public class TiendaRepository : ITiendaRepository
    {
        public List<TiendaEN> SelectAll(TiendaEN obj)
        {
            List<TiendaEN> listReturn = new List<TiendaEN>();

            TiendaEN item = null;
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_TIENDA_ALL");

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new TiendaEN();
                    item.I_CODIGO_TIENDA = DataConvert.ToInt(oReader["I_CODIGO_TIENDA"]);
                    item.V_TIENDA = DataConvert.ToString(oReader["V_TIENDA"]);
                    item.V_RUC = DataConvert.ToString(oReader["V_RUC"]);
                    item.V_WEB = DataConvert.ToString(oReader["V_WEB"]);
                    item.V_LOGO = DataConvert.ToString(oReader["V_LOGO"]);
                    item.V_FACEBOOK = DataConvert.ToString(oReader["V_FACEBOOK"]);
                    item.V_DIRECCION = DataConvert.ToString(oReader["V_DIRECCION"]);
                    item.V_CARPETAIMAG = DataConvert.ToString(oReader["V_CARPETAIMAG"]);
                    item.V_TELEFONO = DataConvert.ToString(oReader["V_TELEFONO"]);
                    item.V_RAZON_SOCIAL = DataConvert.ToString(oReader["V_RAZON_SOCIAL"]);
                    item.idplan = DataConvert.ToInt(oReader["I_PLAN"]);
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

        public TiendaEN Select(TiendaEN item)
        {
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_TIENDA");
            oDatabase.AddInParameter(oDbCommand, "@I_CODIGO_TIENDA", DbType.String, item.I_CODIGO_TIENDA);
            oDatabase.AddInParameter(oDbCommand, "@V_WEB", DbType.String, item.V_WEB);
            oDatabase.AddInParameter(oDbCommand, "@V_USER", DbType.String, item.V_USER);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new TiendaEN();
                    item.I_CODIGO_TIENDA = DataConvert.ToInt(oReader["I_CODIGO_TIENDA"]);
                    item.V_TIENDA = DataConvert.ToString(oReader["V_TIENDA"]);
                    item.V_RUC = DataConvert.ToString(oReader["V_RUC"]);
                    item.V_WEB = DataConvert.ToString(oReader["V_WEB"]);
                    item.V_LOGO = DataConvert.ToString(oReader["V_LOGO"]);
                    item.V_FACEBOOK = DataConvert.ToString(oReader["V_FACEBOOK"]);
                    item.V_DIRECCION = DataConvert.ToString(oReader["V_DIRECCION"]);
                    item.V_CARPETAIMAG = DataConvert.ToString(oReader["V_CARPETAIMAG"]);
                    item.V_TELEFONO = DataConvert.ToString(oReader["V_TELEFONO"]);
                    item.V_RAZON_SOCIAL = DataConvert.ToString(oReader["V_RAZON_SOCIAL"]);
                    item.idplan = DataConvert.ToInt(oReader["I_PLAN"]);
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

        public long Insert(TiendaEN item)
        {
            long codigotienda = 0;
            
                using (var oReader = DatabaseFactory.CreateDatabase().ExecuteReader(
                    "dbo.USP_INS_TIENDA",
                    item.I_CODIGO_TIENDA,
                    item.V_TIENDA,
                    item.V_RUC,
                    item.V_DIRECCION,
                    item.V_WEB,
                    item.V_RAZON_SOCIAL,
                    item.V_DESCRIPCION,
                    item.V_TELEFONO,
                    item.V_FACEBOOK,
                    item.V_LOGO,
                    item.V_USER
                  ))
                {
                    while (oReader.Read())
                    {
                        codigotienda = DataConvert.ToInt64(oReader["I_CODIGO_TIENDA"]);
                    }
                }

                return codigotienda;
        }

        public void Delete(TiendaEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_DEL_TIENDA",
                      item.I_CODIGO_TIENDA,
                      item.V_USER_UPDATE
                  );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(TiendaEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_UPD_TIENDA",
                    item.I_CODIGO_TIENDA,
                   item.V_TIENDA,
                    item.V_RUC,
                    item.V_DIRECCION,
                    item.B_ACTIVE,
                    item.V_USER_UPDATE
                  );

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DashboardEN SelectDashboard(DashboardEN item)
        {
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_RPT_DASHBOARD");
            oDatabase.AddInParameter(oDbCommand, "@V_USER", DbType.String, item.V_USER);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new DashboardEN();
                    item.I_CODIGO_TIENDA = DataConvert.ToInt(oReader["I_CODIGO_TIENDA"]);
                    item.CANT_PEDIDO_PEND = DataConvert.ToInt(oReader["CANT_PEDIDO_PEND"]);
                    item.CANT_VEND_MES_ACT = DataConvert.ToInt(oReader["CANT_VEND_MES_ACT"]);
                    item.PRODUCTOS_BAJO_STOCK = DataConvert.ToInt(oReader["PRODUCTOS_BAJO_STOCK"]);
                    item.TOT_PEDIDO_PEND = DataConvert.ToDouble(oReader["TOT_PEDIDO_PEND"]);
                    item.TOT_VEND_MES_ACT = DataConvert.ToDouble(oReader["TOT_VEND_MES_ACT"]);
                }
                oReader.Close();
            }
            return item;
        }

        public List<RptTransacciones> SelectRptVentas(RptTransacciones item)
        {
            List<RptTransacciones> lstRptPedidos = new List<RptTransacciones>();

            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_RPT_VENTAS");
            oDatabase.AddInParameter(oDbCommand, "@V_USER", DbType.String, item.V_USER);
            oDatabase.AddInParameter(oDbCommand, "@YEAR", DbType.String, item.anio);
            oDatabase.AddInParameter(oDbCommand, "@I_ESTADOCOMPRA", DbType.String, 3);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new RptTransacciones();
                    item.estadoPedido = DataConvert.ToString(oReader["ESTADOCOMPRA"]);
                    item.anio = DataConvert.ToInt(oReader["ANIO"]);
                    item.mes = DataConvert.ToString(oReader["MES"]);
                    item.total = DataConvert.ToDecimal(oReader["TOTAL"]);
                    lstRptPedidos.Add(item);
                }
                oReader.Close();
            }
            return lstRptPedidos;
        }

        public List<RptFormaPago> SelectRptFormaPago(RptFormaPago item)
        {
            List<RptFormaPago> lstRptFormaPago = new List<RptFormaPago>();

            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_RPT_FORMAPAGO");
            oDatabase.AddInParameter(oDbCommand, "@V_USER", DbType.String, item.V_USER);
            oDatabase.AddInParameter(oDbCommand, "@YEAR", DbType.String, item.anio);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    RptFormaPago obj = new RptFormaPago();
                    obj.cant = DataConvert.ToInt(oReader["cant"]);
                    obj.formapago = DataConvert.ToString(oReader["formapago"]);
                    lstRptFormaPago.Add(obj);
                }
                oReader.Close();
            }
            return lstRptFormaPago;
        }

        public List<RptEstadoCompra> SelectRptEstadoCompra(RptEstadoCompra item)
        {
            List<RptEstadoCompra> lstRptFormaPago = new List<RptEstadoCompra>();

            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_RPT_ESTADOCOMPRA");
            oDatabase.AddInParameter(oDbCommand, "@V_USER", DbType.String, item.V_USER);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    RptEstadoCompra obj = new RptEstadoCompra();
                    obj.estadocompra = DataConvert.ToString(oReader["estadocompra"]);
                    obj.porcentaje = DataConvert.ToInt(oReader["porcentaje"]);
                    obj.idestadocompra = DataConvert.ToInt(oReader["idestadocompra"]);
                    lstRptFormaPago.Add(obj);
                }
                oReader.Close();
            }
            return lstRptFormaPago;
        }
        public List<RptTransacciones> SelectRptVentasDiario(RptTransacciones item)
        {
            List<RptTransacciones> lstRptTransacciones = new List<RptTransacciones>();

            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_RPT_VENTADIARIO");
            oDatabase.AddInParameter(oDbCommand, "@V_USER", DbType.String, item.V_USER);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    RptTransacciones obj = new RptTransacciones();
                    obj.fecha = DataConvert.ToString(oReader["fecha"]);
                    obj.total= DataConvert.ToDecimal(oReader["total"]);
                    lstRptTransacciones.Add(obj);
                }
                oReader.Close();
            }
            return lstRptTransacciones;
        }
    }
}
