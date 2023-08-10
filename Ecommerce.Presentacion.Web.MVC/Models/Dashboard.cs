using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace Ecommerce.Presentacion.Web.MVC.Models
{
    public class Dashboard
    {
        public List<ProductoEN> ListaProductos { get; set; }
    }
}