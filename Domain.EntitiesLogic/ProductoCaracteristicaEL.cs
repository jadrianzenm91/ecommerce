using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntitiesLogic
{
    public class ProductoCaracteristicaEL : ProductoCaracteristicaEN
    {
        public string V_CARACTERISTICA { get; set; }
        public string V_VALOR { get; set; }
    }
}
