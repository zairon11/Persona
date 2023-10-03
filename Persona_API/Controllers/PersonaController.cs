using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Persona_API.Dats;
using Persona_API.Models;
// importamos la clase personadto de la clase models
using Persona_API.Models.Dto;

namespace Persona_API.Controllers
{
    // RUTA DE LA API CUANDO SE ESTA HACIENDO LAS PRUEBAS --- EN LA URL APARECERA API/PERSONA
    [Route("api/[controller]")]
    // INDICA QUE ESTA CLASE ES UN CONTROLADOR DE TIPO API 
    [ApiController]


    public class PersonaController : ControllerBase
    {

        private readonly ILogger<PersonaController> _logger;
        private readonly AplicationDbContex _db;

        public PersonaController(ILogger<PersonaController> logger, AplicationDbContex db)
        {
            _logger = logger;
            _db = db;
        }
        // DENTRO DE ESTE CONTROLADOR VAMOS A MANEJAR LAS DIFERENTES PETICIONES O VERBOS HTTP QUE VAMOS HACER A LA API

        // VERBO GET -- obtener todos los datos de la tabla--
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PersonaDto>> GetPersona()
        {
            // aqui vamos a mostrar los datos de la tabla persona
            return Ok(_db.Personas.ToList());
        }


        // VERBO GET  -- obtener un solo dato de la tabla Persona como parametro en la url se tiene que escribir el id que se quiere obtener 
        [HttpGet("{id:int}", Name ="Getvilla")]
        // vamos a documentar cada uno de los codigos de estados que vamos a tener para una peticion 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // con actionresult estamso diciendo a la api el modelo de la tabla que vamos a usar
        public ActionResult<PersonaDto> GetPersona(int id)
        {
            // verificamos cuando el la persona haca una peticion con el id 0
            if (id == 0)
            {
                // si el usuario busca el id  le mostramos un codigo de estado 400
                return BadRequest();
            }

            //--cambiamos--
            //var persona = PersonaDatos.PersonaList.FirstOrDefault(v => v.Id == id);
            var persona = _db.Personas.FirstOrDefault(v => v.Id == id);
            // validamos si es null entonces le mostramos un codigo 404
            if (persona == null)
            {
                return NotFound();
            }

            // y si el id es correcto nos muestra el codigo 200
            return Ok(persona);
        }

        //----------------------VERBO POST------------------ AGREGAR NUEVOS DATOS A LA TABLA PERSONA ---
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // frombody va a traer toda la data de PersonaDto y le vamos a asignar un nombre de personadto
        public ActionResult<PersonaDto> Crearpersona([FromBody] PersonaDto personaDto)
        {
       
            // como en nuestra modelo de la carpeta DTO usamos algunas validaciones aqui vamos a usarlo tambien 
            // decimos si la peticion del usuario esta enviando algo vacion que el servidor respoinda con el codigo 400 diciendo que el 
            // campo es requrido para que se guarde en la DB
            if (!ModelState.IsValid)
            {
                return BadRequest(personaDto);
            }

            // si no se esta enviando ningun dato 
            if (personaDto== null)
            {
                return BadRequest();
            }

        
            if (personaDto.Id > 0 )
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            //--correcion---
            // si esta todo correcto entonces se ingresa a la tabla, aqui incrementamos el id en mas 1 cada 
            // vez que el usuario va ingresando un nuevo dato 
            Persona modelo = new()
            {
                Id = personaDto.Id,
                Name = personaDto.Name,
                Description = personaDto.Description,
                Registrationdate = personaDto.Registrationdate,
                State = personaDto.State,
            };

            // agregamos los datos a la DB personas
            _db.Personas.Add(modelo);
            // guardamos en la DB
            _db.SaveChanges();


            return CreatedAtRoute("Getvilla", new {id = personaDto.Id}.id, personaDto );
        }

        //----- VERBO DELETE---- ELIMINAR DATOS DE LA TABLA ---
        // decimos el metodo y mediante que id vamos a eliminar el dato de la tabla 
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Eliminarpersona (int id)
        {
            // si el id que esta buscando para eliminar es cero le va a mostrar el siguiente codigo de error 400
            if (id == 0)
            {
                return BadRequest();
            }

            var  persona = _db.Personas.FirstOrDefault(v => v.Id == id);

            // si el id que se quiere eliminar no existe en db 
            if (persona == null)
            {
                return NotFound();
            }

            //---correccion--
            _db.Personas.Remove(persona);
            _db.SaveChanges();
            // no retornamos nada de esta funcion porque ya eliminamos el dato
            return NoContent();

        }

        // -------------VERBO PUT ---- VAMOS A ACTUALIZAR DATOS DE LA TABLA PERSONA----
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public  IActionResult Actualizarpersona ( int id, [FromBody] PersonaDto personaDto)
        {
            //realizamos algunas validaciones 
            if(personaDto == null || id != personaDto.Id)
            {
                return BadRequest();
            }

            //--correccion---
            // en caso que tdoo este bien se va actualizar los datos de la tabla
            /* var persona = PersonaDatos.PersonaList.FirstOrDefault(v => v.Id == id);
             personaDto.Name = personaDto.Name;
             personaDto.Description= personaDto.Description;
            */

            Persona modelo = new()
            {
                Id = personaDto.Id,
                Name = personaDto.Name,
                Description = personaDto.Description,
                Registrationdate = personaDto.Registrationdate,
                State = personaDto.State,
            };

            // agregamos y guardamos a la DB
            _db.Personas.Add(modelo);
            _db.SaveChanges();
            return NoContent();

        }

        // -------------VERBO PATCH ---- VAMOS A ACTUALIZAR UN SOLO DATO DE LA TABLA PERSONA----
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Actualizarunapersona(int id, JsonPatchDocument<PersonaDto> pathDto)
        {
            //realizamos algunas validaciones cuando el dato que se envia es null o el id es 0
            if (pathDto == null || id == 0)
            {
                return BadRequest();
            }

            //---correccion---
           // var persona = PersonaDatos.PersonaList.FirstOrDefault(v => v.Id == id);
           var persona = _db.Personas.FirstOrDefault(v => v.Id == id);
            PersonaDto personaDto = new()
            {
                Id = persona.Id,
                Name = persona.Name,
                Description = persona.Description,
                Registrationdate = persona.Registrationdate,
                State = persona.State,
            };

            if (persona == null) return BadRequest(); 
            pathDto.ApplyTo(personaDto, ModelState);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Persona modelo = new()
            {
                Id = personaDto.Id,
                Name = personaDto.Name,
                Description = personaDto.Description,
                Registrationdate = personaDto.Registrationdate,
                State = persona.State,
            };

            _db.Personas.Update(modelo);
            _db.SaveChanges();


            return NoContent();

        }
    }
}
