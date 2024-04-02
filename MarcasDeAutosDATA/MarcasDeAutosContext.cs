using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MarcasDeAutosDATA
{
    public class MarcasDeAutosContext : DbContext
    {
        public DbSet<MarcaAuto> MarcasAutos { get; set; }

        // Obtenemos el connection string a través de inyección de dependencias
        public MarcasDeAutosContext(DbContextOptions<MarcasDeAutosContext> options)
            : base(options)
        {
        }

        // Mecanismo de Data Seed para cargar la tabla con al menos tres ejemplos de marcas de autos.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MarcaAuto>().HasData(
                new MarcaAuto { Id = 1, Nombre = "Toyota" },
                new MarcaAuto { Id = 2, Nombre = "Ford" },
                new MarcaAuto { Id = 3, Nombre = "Chevrolet" }
            );
        }
    }

    public class MarcaAuto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        // Otros campos relevantes para tu modelo de datos
    }
}
