using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PlanesEN : BaseEN
    {
        [Display(Name = "Cod. Plan:")]
        public int idplan { get; set; }
        [Display(Name = "Precio:")]
        public decimal montoplan { get; set; }
        [Display(Name = "Plan:")]
        public string nombreplan { get; set; }
        [Display(Name = "Descripción Plan:")]
        public string description { get; set; }
    }
}
