using EventosAPI.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventosAPI.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetAll()
        {
            return await _context.Usuario.ToListAsync(); //Obtiene la lista de usuarios, puede cambiarse .Eventos y eliminar return para trabajar con otros datos

        }

        [HttpGet("{id:int}")] //Individual
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            return await _context.Usuario.FirstOrDefaultAsync(x => x.Id == id);
        }

        //Crear un objeto nuevo
        [HttpPost]
        //Solo un objeto
        public async Task<ActionResult> Post(Usuario usuario)
        {
            //Validacion que exista el evento para el cual se registrara el usuario
            var existeEvento = await _context.Eventos.AnyAsync(x => x.Id == usuario.EventoId);
            if (!existeEvento)  //Confirmar si existe el evento
            {
                return BadRequest("No existe ese evento");
            }

            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //Actualizar
        [HttpPut("{id:int}")] //Por id
        public async Task<ActionResult> Put(Usuario usuario, int id)
        {
            //Validar si existe el usuario 
            var existeUsuario = await _context.Usuario.AnyAsync(x => x.Id == usuario.Id);

            if (!existeUsuario)
            {
                return BadRequest("No existe el usuario");


            }

            if (usuario.Id != id)
            {
                return BadRequest("El id del usuario no coincide con el de la URL");
            }

            _context.Update(usuario);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //Eliminar
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await _context.Usuario.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("No se encontro este usuario en la base de datos");
            }

            _context.Remove(new Usuario()
            {
                Id = id
            });
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
