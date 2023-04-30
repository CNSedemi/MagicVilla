using MgicVilla_API.Datos;
using MgicVilla_API.Modelos;
using MgicVilla_API.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MgicVilla_API.Controllers
{
    //Genera una ruta: api/Villa
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;

        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db)
        {

            _logger = logger;
            _db = db;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            _logger.LogInformation("Obtener villas"); 

            //Trae todos los datos de la tabla villas y las almacena en una lista
            return Ok(_db.villas.ToList());
        }


        //Se define el endPoint en la url: api/GetVilla/id
        [HttpGet("id:int", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(int id)
        {

            if(id== 0)
            {
                _logger.LogError("Error al crear villa con Id: " + id); 
                return BadRequest();
            }

            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var villa = _db.villas.FirstOrDefault(x => x.Id == id);
            if(villa == null)
            {
                return NotFound(); 
            }
            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CrearVilla([FromBody] VillaDto villaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_db.villas.FirstOrDefault(v => v.Nombre.ToLower() == villaDto.Nombre.ToLower()) != null) {
                ModelState.AddModelError("Nombre Existe", "La villa con el nombre ya existe");
                return BadRequest(ModelState);
            }
            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }
            if (villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            //Genera un Id automaticamente en la lista
            //villaDto.Id = _db.villas.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1; 


            //VillaStore.villaList.Add(villaDto);
            Villa modelo = new()
            {
               
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                ImagenUrl = villaDto.ImagenUrl,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenidad = villaDto.Amenidad,
            };

            //Agregamos modelo a la tabla villas
            _db.villas.Add(modelo); 

            //Guardamos los cambios
            _db.SaveChanges();


            return CreatedAtRoute("GetVilla", new {id=villaDto.Id}, villaDto);  
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = _db.villas.FirstOrDefault(v => v.Id == id);
            if (villa == null) {
                return NotFound();
            }

            //VillaStore.villaList.Remove(villa);
            _db.villas.Remove(villa);

            _db.SaveChanges();

            return NoContent(); 
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto) {
            if (villaDto == null || id != villaDto.Id)
            {
                return BadRequest();
            }

            /*
            var villa = VillaStore.villaList.FirstOrDefault(v=>v.Id==id);
            villa.Nombre= villaDto.Nombre;
            villa.Ocupantes= villaDto.Ocupantes;
            villa.MetrosCuadrados=villaDto.MetrosCuadrados;
            */
            Villa modelo = new()
            {
                Id = villaDto.Id,
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                ImagenUrl = villaDto.ImagenUrl,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenidad = villaDto.Amenidad,
            };

            _db.villas.Update(modelo);
            _db.SaveChanges(); 

            return NoContent();
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> patchDto)
        {
            if (patchDto == null || id != id)
            {
                return BadRequest();
            }

            var villa = _db.villas.AsNoTracking().FirstOrDefault(v => v.Id == id);

            VillaDto villaDto = new VillaDto
            {
                Id=villa.Id,
                Nombre=villa.Nombre,
                Detalle=villa.Detalle,
                ImagenUrl=villa.ImagenUrl,
                Ocupantes=villa.Ocupantes,
                Tarifa=villa.Tarifa,
                MetrosCuadrados=villa.MetrosCuadrados,
                Amenidad=villa.Amenidad

            };
            //patchDto.ApplyTo(villa, ModelState); 

            if(villa==null) return BadRequest();

            patchDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid) { 
                return BadRequest(ModelState);
            }

            Villa modelo = new Villa
            {
                Id = villaDto.Id,
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                ImagenUrl = villaDto.ImagenUrl,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenidad = villaDto.Amenidad

            };

            _db.villas.Update(modelo);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
