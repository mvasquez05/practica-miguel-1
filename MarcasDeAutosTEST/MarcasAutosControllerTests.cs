using Xunit;
using MarcasDeAutosAPI.Controllers;
using MarcasDeAutosDATA;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarcasDeAutosTEST
{
    public class MarcasAutosControllerTests
    {
        [Fact] // Indica que este método es una prueba unitaria
        public async Task GetMarcasAutos_ReturnsCorrectItems()
        {
            // Arrange - configurar el entorno de la prueba
            var options = new DbContextOptionsBuilder<MarcasDeAutosContext>()
                .UseInMemoryDatabase(databaseName: "TestDb") // Usa una base de datos en memoria llamada 'TestDb'
                .Options;

            // Crear y poblar la base de datos en memoria con algunos datos de prueba
            await using (var context = new MarcasDeAutosContext(options))
            {
                await context.MarcasAutos.AddAsync(new MarcaAuto { Id = 1, Nombre = "Toyota" });
                await context.MarcasAutos.AddAsync(new MarcaAuto { Id = 2, Nombre = "Kia" });
                await context.MarcasAutos.AddAsync(new MarcaAuto { Id = 3, Nombre = "Volvo" });
                await context.SaveChangesAsync(); // Guarda los cambios en la base de datos en memoria
            }

            // Act - ejecutar la acción que deseamos probar
            await using (var context = new MarcasDeAutosContext(options)) // Crea una nueva instancia de contexto
            {
                var controller = new MarcasAutosController(context); // Inyecta el contexto en el controlador

                // Ejecuta el método GetMarcasAutos() del controlador de forma asíncrona y almacena el resultado
                var result = await controller.GetMarcasAutos();

                // Assert - verificar los resultados de la acción
                var marcaList = Assert.IsType<List<MarcaAuto>>(result.Value); // Verificar que el resultado es del tipo List<MarcaAuto>
                Assert.Equal(3, marcaList.Count); // Verificar que hay tres elementos en la lista
                Assert.Equal("Toyota", marcaList[0].Nombre); // Verificar que el nombre del primer elemento es "Toyota"
                Assert.Equal("Kia", marcaList[1].Nombre); // Verificar que el nombre del segundo elemento es "Kia"
                Assert.Equal("Volvo", marcaList[2].Nombre); // Verificar que el nombre del segundo elemento es "Volvo"
            }
        }
    }
}