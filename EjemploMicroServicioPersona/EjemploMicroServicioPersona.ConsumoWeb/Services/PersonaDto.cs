using System.ComponentModel.DataAnnotations;

namespace EjemploMicroServicioPersona.ConsumoWeb.Services
{
    public class PersonaDto
    {
        [Required(ErrorMessage = "El ID de la persona es requerido")]
        public string PersonaId { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es requerido")]
        [MinLength(5, ErrorMessage = "El nombre no puede tener menos de 5 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de persona es requerido")]
        [Range(1, 3, ErrorMessage = "El tipo de persona debe ser 1, 2 o 3")]
        public byte Tipo { get; set; }

        [Required(ErrorMessage = "El género es requerido")]
        [MaxLength(10, ErrorMessage = "El género no puede tener más de 10 caracteres")]
        public string Gender { get; set; } = string.Empty;

        // Password solo en creación
        [MaxLength(100)]
        [MinLength(8)]
        public string? Password { get; set; }
    }
}
