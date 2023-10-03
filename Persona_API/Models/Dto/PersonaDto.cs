// importamos dataAnotations para poder validar algunas propiedades 
using System.ComponentModel.DataAnnotations;

namespace Persona_API.Models.Dto
{
    public class PersonaDto
    {
        // propiedades de la tabla persona
        public int Id { get; set; }
        // decimos que es requerido el nombre con una cantidad de carcateres de 40
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime Registrationdate { get; set; }

        public int State { get; set; }
    }
}
