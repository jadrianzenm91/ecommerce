using Domain.Entities;
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

namespace Domain.Repository.CarritoCompra
{
    public class CarritoCompraRepository : ICarritoCompraRepository
    {
        public CarritoCompraEN InsertarCarritoCompra(CarritoCompraEN item, string token)
        {
            using(var oReader = DatabaseFactory.CreateDatabase().ExecuteReader(
                     "dbo.USP_INS_COMPRA",
                     token,
                     item.V_USER_CREATE,
                     item.V_DIRECCION_ENTREGA,
                     item.NOMBRE_CONTACTO,
                     item.CELULAR_CONTACTO,
                     item.I_CODIGO_TIENDA,
                     item.EMAIL,
                     item.I_CODIGO_FORMA_PAGO,
                     item.V_DNI,
                     item.longitude,
                     item.latitude
                   ))
            {
                while (oReader.Read())
                {
                    item.I_CODIGO_COMPRA = DataConvert.ToInt64(oReader["I_CODIGO_COMPRA"]);
                    item.N_TOTAL = DataConvert.ToDecimal(oReader["N_TOTAL"]);
                    item.tiendaEN.V_RAZON_SOCIAL = DataConvert.ToString(oReader["V_RAZON_SOCIAL"]);
                }
            }

            return item;
        }

        public long updateCarritoCompra(CarritoCompraEN item)
        {
            long codigoCompra = 0;
            using (var oReader = DatabaseFactory.CreateDatabase().ExecuteReader(
                     "dbo.USP_UPD_COMPRA",
                     item.I_CODIGO_COMPRA,
                     item.V_TAJETA_PAGO
                   ))
            {
                while (oReader.Read())
                {
                    codigoCompra = DataConvert.ToInt64(oReader["I_CODIGO_COMPRA"]);
                }
            }

            return codigoCompra;
        }
        public void InsertarCarritoCompraDetalle(DetalleCompraEN item)
        {
            DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_INS_DETALLE_COMPRA",
                    item.codigoCompra,
                    item.codigoProducto,
                    item.precio,
                    item.cantidad,
                    item.V_USER_CREATE
                  );
        }
        public DetalleCompraTempEN InsertarCarritoCompraDetalleTemp(DetalleCompraTempEN item)
        {
            using (var oReader = DatabaseFactory.CreateDatabase().ExecuteReader(
                      "dbo.USP_INS_DETALLECOMPRATEMP",
                        item.id,
                        item.token,
                        item.idprod,
                        item.cant
                   ))
            {
                while (oReader.Read())
                {
                    item.id = DataConvert.ToInt64(oReader["idtoken"]);
                    item.cant = DataConvert.ToInt32(oReader["cantProductos"]);
                }
            }

            return item;
        }

        public List<DetalleCompraTempEN> getDetalleCompraTemp(DetalleCompraTempEN item)
        {
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_DETALLECOMPRATEMP");
            oDatabase.AddInParameter(oDbCommand, "@ID", DbType.Int32, item.id);
            oDatabase.AddInParameter(oDbCommand, "@TOKEN", DbType.String, item.token);
            oDatabase.AddInParameter(oDbCommand, "@I_CODIGO_PRODUCTO", DbType.Int64, item.idprod);
            List<DetalleCompraTempEN> lst = new List<DetalleCompraTempEN>();
           
            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    var obj = new DetalleCompraTempEN();
                    obj.id = DataConvert.ToInt32(oReader["ID"]);
                    obj.token = DataConvert.ToString(oReader["TOKEN"]);
                    obj.idprod = DataConvert.ToInt64(oReader["I_CODIGO_PRODUCTO"]);
                    obj.cant = DataConvert.ToInt32(oReader["I_CANTIDAD"]);
                    obj.total = DataConvert.ToDecimal(oReader["I_TOTAL"]);
                    obj.V_PRODUCTO = DataConvert.ToString(oReader["V_PRODUCTO"]);
                    obj.V_URL_PRINCIPAL = DataConvert.ToString(oReader["V_URL_PRINCIPAL"]);
                    obj.N_PRECIO = DataConvert.ToDecimal(oReader["N_PRECIO"]);
                    lst.Add(obj);

                }
                oReader.Close();
            }
            return lst;
        }
        public CarritoCompraEL getCarritoCompra(int codigoCompra)
        {
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_CARRITOCOMPRA_GET");
            oDatabase.AddInParameter(oDbCommand, "@I_CODIGO_COMPRA", DbType.String, codigoCompra);

            var item = new CarritoCompraEL();

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    
                    item.codigoCompra = DataConvert.ToInt32(oReader["I_CODIGO_COMPRA"]);
                    item.fechaCompra = DataConvert.ToDateTimeNull(oReader["D_FECHA_COMPRA"]);
                    item.total = DataConvert.ToDecimal(oReader["N_TOTAL"]);
                    item.usuario = DataConvert.ToString(oReader["V_USUARIO"]);
                    item.codigoFormaPago = DataConvert.ToInt(oReader["I_CODIGO_FORMA_PAGO"]);
                    item.formaPago = DataConvert.ToString(oReader["V_FORMA_PAGO"]);
                    item.direccion = DataConvert.ToString(oReader["V_DIRECCION_ENTREGA"]);
                    item.nombres = DataConvert.ToString(oReader["NOMBRE_CONTACTO"]);
                    item.celular = DataConvert.ToInt32(oReader["CELULAR_CONTACTO"]);
                    item.codigoEstadoCompra = DataConvert.ToInt32Null(oReader["I_CODIGO_ESTADO_COMPRA"]);
                    item.estadoCompra = DataConvert.ToString(oReader["V_ESTADO_COMPRA"]);
                    item.email = DataConvert.ToString(oReader["V_CORREO_CONTACTO"]);
                    item.dni = DataConvert.ToString(oReader["V_DNI_CONTACTO"]);
                    item.costoEnvio = DataConvert.ToString(oReader["N_COSTENVIO"]);
                    item.fechaEntregaAprox = DataConvert.ToString(oReader["D_FECHAENTREGAEST"]);
                    item.longitude = DataConvert.ToString(oReader["V_LONGITUDE"]);
                    item.latitude= DataConvert.ToString(oReader["V_LATITUDE"]);
                    item.tarjeta = DataConvert.ToString(oReader["V_TARJETA_PAGO"]);
                    item.tienda = DataConvert.ToString(oReader["V_TIENDA"]);
                    item.razonsocial = DataConvert.ToString(oReader["V_RAZON_SOCIAL"]);
                    item.mensajeEstadoCompra = DataConvert.ToString(oReader["V_MENSAJE"]);
                    item.logotienda = DataConvert.ToString(oReader["V_LOGO"]);
                    item.facebook = DataConvert.ToString(oReader["V_FACEBOOK"]);
                    item.telefonotienda = DataConvert.ToString(oReader["V_TELEFONO"]);
                    item.webtienda = DataConvert.ToString(oReader["V_WEB"]);
                    item.emailtienda = DataConvert.ToString(oReader["V_EMAIL_EMPRESA"]);
                }
                oReader.Close();
            }
            return item;
        }

        public List<CarritoCompraEL> getCarritoCompraUsuarioTienda(CarritoCompraEN obj)
        {
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_CARRITOCOMPRA_GET");
            oDatabase.AddInParameter(oDbCommand, "@I_CODIGO_COMPRA", DbType.String, obj.I_CODIGO_COMPRA);
            oDatabase.AddInParameter(oDbCommand, "@V_TIENDA", DbType.String, obj.tiendaEN.V_TIENDA);
            oDatabase.AddInParameter(oDbCommand, "@V_USER", DbType.String, obj.V_USER);

            List<CarritoCompraEL> result = new List<CarritoCompraEL>();

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    var item = new CarritoCompraEL();
                    item.codigoCompra = DataConvert.ToInt32(oReader["I_CODIGO_COMPRA"]);
                    item.fechaCompra = DataConvert.ToDateTimeNull(oReader["D_FECHA_COMPRA"]);
                    item.total = DataConvert.ToDecimal(oReader["N_TOTAL"]);
                    item.usuario = DataConvert.ToString(oReader["V_USUARIO"]);
                    item.codigoFormaPago = DataConvert.ToInt(oReader["I_CODIGO_FORMA_PAGO"]);
                    item.formaPago = DataConvert.ToString(oReader["V_FORMA_PAGO"]);
                    item.direccion = DataConvert.ToString(oReader["V_DIRECCION_ENTREGA"]);
                    item.nombres = DataConvert.ToString(oReader["NOMBRE_CONTACTO"]);
                    item.celular = DataConvert.ToInt32(oReader["CELULAR_CONTACTO"]);
                    item.codigoEstadoCompra = DataConvert.ToInt32Null(oReader["I_CODIGO_ESTADO_COMPRA"]);
                    item.estadoCompra = DataConvert.ToString(oReader["V_ESTADO_COMPRA"]);
                    item.email = DataConvert.ToString(oReader["V_CORREO_CONTACTO"]);
                    item.dni = DataConvert.ToString(oReader["V_DNI_CONTACTO"]);
                    item.costoEnvio = DataConvert.ToString(oReader["N_COSTENVIO"]);
                    item.fechaEntregaAprox = DataConvert.ToString(oReader["D_FECHAENTREGAEST"]);
                    item.longitude = DataConvert.ToString(oReader["V_LONGITUDE"]);
                    item.latitude = DataConvert.ToString(oReader["V_LATITUDE"]);
                    item.tarjeta = DataConvert.ToString(oReader["V_TARJETA_PAGO"]);
                    item.tienda = DataConvert.ToString(oReader["V_TIENDA"]);
                    item.razonsocial = DataConvert.ToString(oReader["V_RAZON_SOCIAL"]);
                    item.mensajeEstadoCompra = DataConvert.ToString(oReader["V_MENSAJE"]);
                    item.logotienda = DataConvert.ToString(oReader["V_LOGO"]);
                    item.facebook = DataConvert.ToString(oReader["V_FACEBOOK"]);
                    item.telefonotienda = DataConvert.ToString(oReader["V_TELEFONO"]);
                    item.webtienda = DataConvert.ToString(oReader["V_WEB"]);
                    result.Add(item);
                }
                oReader.Close();
            }
            return result;
        }

        public List<CarritoCompraEL> getCarritoCompraAll(TiendaEN obj)
        {
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_COMPRA_PEDIDOS");
            oDatabase.AddInParameter(oDbCommand, "@V_USER", DbType.String, obj.V_USER);
            oDatabase.AddInParameter(oDbCommand, "@I_CODIGO_TIENDA", DbType.Int32, obj.I_CODIGO_TIENDA);
            oDatabase.AddInParameter(oDbCommand, "@I_CODIGO_USUARIO", DbType.String, obj.I_CODIGO_USUARIO);

            var list = new List<CarritoCompraEL>();

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    var item = new CarritoCompraEL();
                    item.codigoCompra= DataConvert.ToInt32(oReader["I_CODIGO_COMPRA"]);
                    item.fechaCompra = DataConvert.ToDateTimeNull(oReader["D_FECHA_COMPRA"]);
                    item.total = DataConvert.ToDecimal(oReader["N_TOTAL"]);
                    item.codigoUsuario = DataConvert.ToString(oReader["I_CODIGO_USUARIO"]);
                    item.codigoFormaPago = DataConvert.ToInt(oReader["I_CODIGO_FORMA_PAGO"]);
                    item.formaPago = DataConvert.ToString(oReader["V_FORMA_PAGO"]);
                    item.direccion = DataConvert.ToString(oReader["V_DIRECCION_ENTREGA"]);
                    item.nombres = DataConvert.ToString(oReader["NOMBRE_CONTACTO"]);
                    item.celular = DataConvert.ToInt32(oReader["CELULAR_CONTACTO"]);
                    item.codigoEstadoCompra = DataConvert.ToInt32Null(oReader["I_CODIGO_ESTADO_COMPRA"]);
                    item.estadoCompra = DataConvert.ToString(oReader["V_ESTADO_COMPRA"]);
                    item.email = DataConvert.ToString(oReader["V_CORREO_CONTACTO"]);
                    item.urlPhotoUsuario = DataConvert.ToString(oReader["V_PHOTO_URL"]);
                    item.usuario = DataConvert.ToString(oReader["V_NOMBRES"]);
                    list.Add(item);
                }
                oReader.Close();
            }
            return list;
        }

        public void Update(CarritoCompraEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_UPD_COMPRA",
                    item.I_CODIGO_COMPRA,
                    item.N_TOTAL,
                    item.I_CODIGO_FORMA_PAGO,
                    item.I_CODIGO_USUARIO,
                    item.B_ACTIVE,
                    item.V_USER_UPDATE
                  );

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void actualizarEstadoCompra(CarritoCompraEN item)
        {
            DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_UPD_ESTADOCOMPRA",
                    item.I_CODIGO_COMPRA,
                    item.V_USER_CREATE,
                    item.I_ESTADO_COMPRA
                  );
        }
    }
}
