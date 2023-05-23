using EventosAPI.Entidades;
using Microsoft.EntityFrameworkCore;

namespace EventosAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Evento> Eventos { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

    }
}
