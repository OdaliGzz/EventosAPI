using EventosAPI.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventosAPI.Controllers
{
    [ApiController]
    [Route("evento")]
    public class EventoController: ControllerBase
    {
        public readonly ApplicationDbContext _context;

        public EventoController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]//Para obtener lista de eventos sin tener una base de datos
        public ActionResult<List<Evento>> Get() {
            return new List<Evento>(){
                new Evento {Id = 1, Nombre = "EventoEnero", Descripcion = "Evento de dia", Fecha = DateTime.Now, Ubicacion = "Monterrey", Capacidad = 100},
                new Evento {Id = 2, Nombre = "EventoFebrero", Descripcion = "Evento al aire libre", Fecha = DateTime.Now, Ubicacion = "San Nicolas", Capacidad = 2000}
            };
        }

        //Guardar
        [HttpPost]
        public async Task<ActionResult> Post(Evento evento)
        {
            _context.Add(evento); //Guardado hasta el contexto
            await _context.SaveChangesAsync();
            return Ok();
        }

        //Solucion con rutas, usar / para aliminar palabra anterior
        [HttpGet("lista")]
        //Obtener lista de eventos
        public async Task<ActionResult<List<Evento>>> GetAll()
        {
            return await _context.Eventos.Include(x => x.Usuarios).ToListAsync();
        }

        //Actualizar
        //string para nombre
        [HttpPut("{id:int}")]
        //Validacion para saber si existe
        public async Task<ActionResult> Put(Evento evento, int id)
        {
            if (evento.Id != id)
            {
                return BadRequest("El id no coincide con el de la URL");
            }

            _context.Update(evento);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //Eliminar
        [HttpDelete("{id:int}")]
                            //Cualquier id que coincida
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await _context.Eventos.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("No se encontro elemento en la base de datos");
            }
            //Obj a enviar es instancia que solo tenga el valor de id
            _context.Remove(new Evento() { Id = id });
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
