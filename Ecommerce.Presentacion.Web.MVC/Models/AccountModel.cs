using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
   
        public class UserProfile
        {
            [Key]
            public int UserId { get; set; }
            public string UserName { get; set; }
        }

        
        public class LocalPasswordModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password actual")]
            public string OldPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Nuevo Password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar nuevo Password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public class LoginModel
        {
            [Required]
            [Display(Name = "Nombre Usuario")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Clave")]
            public string Password { get; set; }

            [Display(Name = "Recordar clave?")]
            public bool RememberMe { get; set; }
        }
        
        public class RegisterModel
        {
            [Display(Name = "Nombre Usuario")]
            public string UserName { get; set; }

            [StringLength(200, ErrorMessage = "El campo {0} tiene máximo {1} caracteres.")]
            [Required(ErrorMessage = "Este campo es obligatorio")]
            [DataType(DataType.EmailAddress, ErrorMessage = "Correo inválido")]
            [EmailAddress]
            [Display(Name = "E-mail")]
            public string V_EMAIL { get; set; }

            [Display(Name = "Nombres")]
            [Required(ErrorMessage = "Este campo es Obligatorio.")]
            [StringLength(200, ErrorMessage = "El campo {0} tiene máximo {1} caracteres.")]
            public string V_NOMBRES { get; set; }

            [Display(Name = "Apellidos")]
            [Required(ErrorMessage = "Este campo es Obligatorio.")]
            [StringLength(200, ErrorMessage = "El campo {0} tiene máximo {1} caracteres.")]
            public string V_APELLIDOS { get; set; }

            [Display(Name = "DNI/Carnet Ext.")]
            [StringLength(15, ErrorMessage = "El campo {0} tiene máximo {1} caracteres.")]
            public string V_DNI { get; set; }

            [Display(Name = "País")]
            [Required(ErrorMessage = "Este campo es Obligatorio.")]
            public string pais { get; set; }

            [Display(Name = "Celular")]
            [Required(ErrorMessage = "Este campo es Obligatorio.")]
            [StringLength(12, ErrorMessage = "El campo {0} tiene máximo {1} caracteres.")]
            public string V_TELEFONO { get; set; }

            [Display(Name="Perfil")]
            public int RoleId { get; set; }

            [Required(ErrorMessage = "Este campo es Obligatorio.")]
            [StringLength(128, ErrorMessage = "El campo debe tener mínimo {1} caracteres.", MinimumLength = 6)]
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