using MarcasDeAutosDATA;
using Microsoft.EntityFrameworkCore;

namespace MarcasDeAutosTEST
{
    public class MarcasDeAutosContextTests
    {
        [Fact]
        public void OnModelCreating_SeedShouldCreateThreeMarcaAutos()
        {
            // Arrange - establecer el contexto EF en memoria
            var options = new DbContextOptionsBuilder<MarcasDeAutosContext>()
                .UseInMemoryDatabase(databaseName: "TestDbForSeeding") // Base de datos en memoria para la prueba
                .Options;

            using (var context = new MarcasDeAutosContext(options))
            {
                context.Database.EnsureCreated(); // Asegura que la base de datos está creada y la data seed aplicada
            }

            // Act - crear una instancia del contexto para acceder a los datos y verificar que la Data Seed ha sido aplicada
            using (var context = new MarcasDeAutosContext(options))
            {                                
                // Assert - verificar que los datos sembrados existen como se espera
                Assert.Equal(3, context.MarcasAutos.Count()); // Verificar que hay tres marcas en la base de datos
                Assert.Equal("Toyota", context.MarcasAutos.FirstOrDefault(m => m.Id == 1)?.Nombre);
                Assert.Equal("Ford", context.MarcasAutos.FirstOrDefault(m => m.Id == 2)?.Nombre);
                Assert.Equal("Chevrolet", context.MarcasAutos.FirstOrDefault(m => m.Id == 3)?.Nombre);
            }
        }
    }
}
