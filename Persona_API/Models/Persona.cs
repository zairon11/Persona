using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persona_API.Models
{
    public class Persona
    {
        // propiedades de la tabla persona
        // propiedades de la tabla persona

        // le decimos que el id sea llave primaria y que esto se incremente de 1 en 1
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        // decimos que es requerido el nombre
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime Registrationdate { get; set; }

        public int State { get; set; }

    }
}
