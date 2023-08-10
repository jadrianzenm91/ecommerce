using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting
{
    public class Constantes
    {
        public static class Mensajes
        {
            public const string Message = "Message";
            public const string VentaExitosa = "Venta exitosa. Se realizó la operación satisfactoriamente.";
            public const string PagoExitoso = "Se realizó el pago correctamente.";
            public const string ReservaExitosa = "Reserva exitosa. Se realizó la operación satisfactoriamente.";
            public const string Success = "La transacción fue ejecutada con éxito!.";
            public const string Error = "Error en la ejecución";
            public const string ErrorServer = "Error en el servidor de aplicaciones";
        }
        public static class Paginacion
        {
            public const string Primero = "Primero";
            public const string Anterior = "Anterior";
            public const string Siguiente = "Siguiente";
            public const string Ultimo = "Ultimo";

            public const int FilasxPagina_Normal = 10;
            public const int FilasxPagina_Detalle = 5;

            public const int paginaDefault = 1;

        }
        public static class Conceptos
        {
            public const int free = 1;
            public const int planbase = 2;
            public const int inscripcion = 5;
            public const int dominioHostingSSL= 3;
        }
        public static class Perfiles
        {
            public const string Administrador = "ADMINISTRADOR";
            public const string Cliente = "CLIENTE";
            public const string Empresa = "EMPRESA";
            public const string Empresa_Administrador = "EMPRESA, ADMINISTRADOR";
        }
        public static class EMPRESA
        {
            public const string RAZON_SOCIAL = "Sistemas Adrianzen EIRL";
            public const string RUC = "20600714903";
            public const string EMAIL = "sistemasadrianzen@outlook.com";
            public const string Password = "J0n4th4n";
            public const string UserCreate = "SISADRI-SCRIPT";
            public const string piePagina = "Copyright © SISADRI EIRL - PERÚ 2015";
            public const string SiglaEmpresa = "SISADRI";
            public const string descriptor = "SISADRI líder en Tecnología de Información en apoyo a la sociedad.";
            public const string title = "Tienda Virtual";
            public const string titleAdmin = "Panel de Administracón - Tienda Virtual para Emprendedores";
        }
        public static class Login
        {
            public const string Admin = "ADMIN";
            public const string SesionExpirada = "SE";
            public const string User = "sisadri";
            public const string UserFirebase = "FIREBASE";
            public const string Password = "J0n4th4n";
            public const string PasswordClienteRedSocial = "s1st3m4s4dr14nz3n";
            public const string UserCreate = "SISADRI-SCRIPT";
            
        }
        public static class Parametro
        {
            public const int culqiPro = 1;
            public const int culqiDev = 2;
        }
        public static class Web
        {
            public const string PLATAFORMA_WEB = "PLATAFORMA_WEB";

        }
        public static class Formaters
        {
            public const string FormatDate = "dd/MM/yyyy";
            public const string FormatDecimalNumber = "#0,#.00";
            public const string StringFormatToNumber = "{0:0,0.00}";
            public const string datePatt = @"yyyyMd_hhmmss";//para importaciones de archivos
            public const string FormatDateTime = "dd/MM/yyyy HH:mm";
        }

        public class SessionKey
        {
            public const string Usuario = "usuario";
            public const string Permisos = "permisos";
            public const string IdUsuario = "-1";
            public const string NombreCompleto = "nombre apellidos";
        }

        public static class AppSettingsKey
        {
            public const string ServerSra = "ServerSra";
            public const string ReportingService = "ReportingService";
            public const string RootReporte = "RootReporte";
            public const string UserCredencial = "UserCredencial";
            public const string PasswordCredencial = "PasswordCredencial";
            public const string DominioCredencial = "DominioCredencial";
        }

        public static class OtherFilters
        {
            public const string Todos = "Todos";
            public const string Seleccione = "-- Seleccione --";
            public const string Otros = "Otros";
        }

        public static class TypeResult
        {
            public const string Success = "success";
            public const string Validation = "warning";
            public const string Exception = "danger";
        }
        
        public static class FiltrosSistema
        {
            public const Int32 Todos = 0;
        }
        public static class PermitirDenegar
        {
            public const string Permitido = "Permitido";
            public const string Denegado = "Denegado";
        }
        public static class Accion
        {
            public const string Editar = "EDIT";
            public const string Eliminar = "DELETE";
            public const string Create = "CREATE";
        }
        public static class EstadoCompra
        {
            public const int EnValidacion = 1;
            public const int Confirmado = 6;
            public const int EnCaminoEntrega = 2;
            public const int Entregado = 3;
            public const int EnReclamo = 4;
            public const int Anulado = 5;

        }
        public static class FormaPago
        {
            public const int PagoOnline = 2;
            public const int Reserva = 1;
            public const string PagoContraEntrega = "Pago contra Entrega";
            public const string tokenReserva = "TOKRSV";
        }
    }

    public class Enumeradores
    {
        public enum OtherFilters
        {
            Todos = 0,
            Seleccione = -1
        }

    }
}
