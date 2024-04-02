using MarcasDeAutosDATA;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configurar el DbContext para conectarse a una base de datos PostgreSQL utilizando Inyección de Dependencias.
// Esto permite que la cadena de conexión para PostgreSQL sea inyectada desde el archivo appsettings.json.
builder.Services.AddDbContext<MarcasDeAutosContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MarcasDeAutosContext")));


var app = builder.Build();

// Configurar el canal de peticiones HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
