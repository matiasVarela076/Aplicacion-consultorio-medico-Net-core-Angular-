using System.ComponentModel.DataAnnotations;

namespace Models.DTO
{
    public class EspecialidadDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "El nombre debe ser Minimo 1 Maximo 60 caracteres")]
        public string NombreEspecialidad { get; set; }

        [Required(ErrorMessage = "La descripcion es requerido")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "La descripción debe ser Minimo 1 Maximo 100 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El estado es requerido")]
        public int Estado { get; set; }
    }
}
