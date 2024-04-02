using MarcasDeAutosDATA;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Inyección de Dependencias para Inyectar la Configuración de la connection string ubicada en el archivo appsettings.json
// Configure DbContext with PostgreSQL
builder.Services.AddDbContext<MarcasDeAutosContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MarcasDeAutosContext")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
