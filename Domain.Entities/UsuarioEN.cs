using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UsuarioEN : BaseEN
    {
        public UsuarioEN()
        {
            this._TiendaEN = new TiendaEN();
        }
        public string V_UID_PROVIDER { get; set; }
        public long IdUsuario { get; set; }
        public string V_USUARIO { get; set; }
        public string V_NOMBRES { get; set; }
        public string V_APELLIDOS { get; set; }
        public string V_DNI { get; set; }
        public string I_CELULAR { get; set; }
        public string V_DIRECCION { get; set; }
        public string V_EMAIL { get; set; }
        public string V_PROVIDER { get; set; }
        public string V_PHOTO_URL { get; set; }
        public string V_DPTO { get; set; }
        public string V_PROV { get; set; }
        public string V_DISTR { get; set; }
        public TiendaEN _TiendaEN { get; set; }
        public string confirmationToken { get; set; }
        public bool? isConfirmation { get; set; }
        
    }
    public class RequestUsuarioDatos
    {
        public string email { get; set; }

        [StringLength(100, ErrorMessage = "El campo {0} tiene máximo {1} caracteres.")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string nombres { get; set; }

        [StringLength(100, ErrorMessage = "El campo {0} tiene máximo {1} caracteres.")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string apellidos { get; set; }

        [StringLength(15, ErrorMessage = "El campo {0} tiene máximo {1} caracteres.")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string dni { get; set; }

        [StringLength(15, ErrorMessage = "El campo {0} tiene máximo {1} caracteres.")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string celular { get; set; }

        [StringLength(200, ErrorMessage = "El campo {0} tiene máximo {1} caracteres.")]
        public string direccion { get; set; }


        [StringLength(150, ErrorMessage = "El campo {0} tiene máximo {1} caracteres.")]
        public string dpto { get; set; }

        [StringLength(150, ErrorMessage = "El campo {0} tiene máximo {1} caracteres.")]
        public string prov { get; set; }

        [StringLength(150, ErrorMessage = "El campo {0} tiene máximo {1} caracteres.")]
        public string distrito { get; set; }


    }
    public class tokenUsuarioEN : BaseEN
    {
        public long id { get; set; }
        public string token { get; set; }
        
    }
    public class RegisterUsuarioTienda
    {
        [StringLength(200, ErrorMessage = "El campo {0} tiene máximo {1} caracteres.")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Correo inválido")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string V_EMAIL { get; set; }

        [Required(ErrorMessage = "Este campo es Obligatorio.")]
        [StringLength(128, ErrorMessage = "El campo debe tener mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Clave")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Clave")]
        [Compare("Password", ErrorMessage = "La clave y la confirmación de clave deben ser iguales.")]
        [Required(ErrorMessage = "Este campo es Obligatorio.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetUsuarioTienda
    {
       
        [Required(ErrorMessage = "Este campo es Obligatorio.")]
        [StringLength(128, ErrorMessage = "El campo debe tener mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Clave")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Clave")]
        [Compare("Password", ErrorMessage = "La clave y la confirmación de clave deben ser iguales.")]
        [Required(ErrorMessage = "Este campo es Obligatorio.")]
        public string ConfirmPassword { get; set; }
    }
}
