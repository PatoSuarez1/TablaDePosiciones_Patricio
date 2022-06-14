using System.ComponentModel.DataAnnotations;

namespace TablaDePosiciones_Patricio.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [Display(Name = "Nombre de Usuario")]
        public string UserName{ get; set; }
        [Required(ErrorMessage = "El Email es obligatorio.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }
}
