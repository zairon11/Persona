// importamos la clase personadto de la clase models
using Persona_API.Models.Dto;

namespace Persona_API.Dats
{
    public class PersonaDatos
    {
        public static List<PersonaDto> PersonaList = new List<PersonaDto>
        {
            new PersonaDto{Id = 1, Name = "Yoel", Description = "hi my name is yoel"},
            new PersonaDto{Id = 2, Name = "Julia", Description = "hi my name is julia"}

        };
     }
}
