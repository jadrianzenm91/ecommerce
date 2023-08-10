using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CategoriaEN : BaseEN
    {
        public int I_CODIGO_CATEGORIA { get; set; }

        [Display(Name = "Categoría")]
        [StringLength(150, ErrorMessage = "El {0} debe tener mínimo {2} caracteres de longitud. Ejm: GASEOSAS, CALZADOS, TECNOLOGIA, TORTAS, ETC.", MinimumLength = 4)]
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        public string V_CATEGORIA { get; set; }
        
        
        public string V_URL { get; set; }
        public int I_CATEGORIA_PADRE { get; set; }
        public int I_CODIGO_TIENDA { get; set; }
        public string V_CATEGORIA_PADRE { get; set; }
        public string V_TIENDA { get; set; }
    }
}
