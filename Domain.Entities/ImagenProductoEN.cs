using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [DataContract]
    public class ImagenProductoEN : BaseEN
    {
        public long? I_CODIGO_IMAGEN { get; set; }
        public long? I_CODIGO_PRODUCTO { get; set; }
        public string V_URL { get; set; }
        
    }
}
