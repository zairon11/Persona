// importamos entityframewokr
using Microsoft.EntityFrameworkCore;
using Persona_API.Models;

namespace Persona_API.Dats
{
    public class AplicationDbContex : DbContext
    {
        public AplicationDbContex(DbContextOptions<AplicationDbContex> options ): base(options) 
        {

        }
        // en esta clase vamos a crear como un modelo para conectarnos con la DB
        // decimos que el modelo persona que tenemos en la carpeta Models se va a crear en la DB
        public DbSet<Persona> Personas { get; set; }

        // VAMOS AGREGAR nuevos datos a la DB
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>().HasData(
                new Persona()
                {
                    Id = 1,
                    Name = "Natural",
                    Description = "DESD",
                    Registrationdate = DateTime.Now,
                    State = 1

                },

                new Persona()
                {
                    Id = 2,
                    Name = "Juridica",
                    Description = "DEX",
                    Registrationdate = DateTime.Now,
                    State = 1

                }
                );
        }
    }
}
