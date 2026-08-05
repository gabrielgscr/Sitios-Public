using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EjemploMicroServicioPersona.Entities
{
    public class Persona
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El ID de la persona es requerido")]
        public string PersonaId { get; set; } = null!;

        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre es requerido")]
        [MinLength(5, ErrorMessage = "El nombre no puede tener menos de 5 caracteres")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El tipo de persona es requerido")]
        [Range(1, 3, ErrorMessage = "El tipo de persona debe ser 1, 2 o 3")]
        public byte Tipo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El género es requerido")]
        [MaxLength(10, ErrorMessage = "El género no puede tener más de 10 caracteres")]
        public string Gender { get; set; } = string.Empty;

        // Campo requerido en BD (100), no se devuelve en listados porque el repositorio no lo selecciona
        [MaxLength(100, ErrorMessage = "La contraseña no puede tener más de 100 caracteres")]
        [MinLength(8, ErrorMessage = "La contraseña no puede tener menos de 8 caracteres")]
        public string? Password { get; set; }
    }
}
