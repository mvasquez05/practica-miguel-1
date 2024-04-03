using MarcasDeAutosDATA;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

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

            // Act - crear una instancia del contexto y permitir que EF Core aplique las configuraciones
            using (var context = new MarcasDeAutosContext(options))
            {
                // EF Core aplica automáticamente la Data Seed en la primera operación de consulta o guardado
                context.Database.EnsureCreated(); // Asegura que la base de datos está creada
            }

            // Assert - abrir un nuevo contexto para verificar que la Data Seed ha sido aplicada
            using (var context = new MarcasDeAutosContext(options))
            {
                // Asegurarse de que hay tres marcas en la base de datos
                Assert.Equal(3, context.MarcasAutos.Count());
                // Verificar que los datos son los esperados
                Assert.NotNull(context.MarcasAutos.FirstOrDefault(m => m.Id == 1 && m.Nombre == "Toyota"));
                Assert.NotNull(context.MarcasAutos.FirstOrDefault(m => m.Id == 2 && m.Nombre == "Ford"));
                Assert.NotNull(context.MarcasAutos.FirstOrDefault(m => m.Id == 3 && m.Nombre == "Chevrolet"));
            }
        }
    }
}
