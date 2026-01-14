using Aplication.Mapping;
using Aplication.UseCases;
using Aplication.UseCases.AdultoServices;
using Aplication.UseCases.EnfermeriaServices;
using Aplication.UseCases.FisioterapiaServices;
using Aplication.UseCases.OrientacionServices;
using Aplication.UseCases.ProteccionServices;
using Domain.Interfaces;
using Infraestructure.Data;
using Infraestructure.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });

    options.AddPolicy("PermitirReact",
        policy => policy.WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod());

});

// ConnectionSettings
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Infraestructure")));

//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
//Repositorios
builder.Services.AddScoped<IAdulto, AdultoRepositorio>();
builder.Services.AddScoped<IFichaEnfermeria, FichaEnfermeriaRepositorio>();
builder.Services.AddScoped<IFichaOrientacion, FichaOrientacionRepositorio>();
builder.Services.AddScoped<IFichaProteccion, FichaProteccionRepositorio>();
builder.Services.AddScoped<IFichaFisioterapia, FichaFisioterapiaRepositorio>();
//Casos de uso
builder.Services.AddScoped<CrearAdulto>();
builder.Services.AddScoped<EditarAdulto>();
builder.Services.AddScoped<EliminarAdulto>();

builder.Services.AddScoped<CrearFichaEnfermeria>();
builder.Services.AddScoped<EditarEnfermeria>();
builder.Services.AddScoped<EliminarEnfermeria>();

builder.Services.AddScoped<CrearFichaOrientacion>();
builder.Services.AddScoped<EditarOrientacion>();
builder.Services.AddScoped<EliminarOrientacion>();

builder.Services.AddScoped<CrearFichaProteccion>();
builder.Services.AddScoped<EditarProteccion>();
builder.Services.AddScoped<EliminarProteccion>();

builder.Services.AddScoped<CrearFichaFisioterapia>();
builder.Services.AddScoped<EditarFisioterapia>();
builder.Services.AddScoped<EliminarFisioterapia>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//CORS
app.UseCors("AllowAll");
app.UseCors("PermitirReact");

app.UseAuthorization();

app.MapControllers();

app.Run();
