using System.ComponentModel.DataAnnotations;

namespace Models.DTO
{
    public class RegistroDto
    {
        [Required(ErrorMessage = "El usuario es requerido")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El password es requerido")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "El password debe ser Minimo 4 y Maximo 10 caracteres")]
        public string Password { get; set; }
    }
}
