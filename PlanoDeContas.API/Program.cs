using Microsoft.EntityFrameworkCore;
using PlanoDeContas.Application.Services;
using PlanoDeContas.Domain.Interfaces;
using PlanoDeContas.Infrastructure.Data;
using PlanoDeContas.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Configurar o DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar o repositório e serviço no DI
builder.Services.AddScoped<IPlanoDeContaRepository, PlanoDeContaRepository>();
builder.Services.AddScoped<PlanoDeContaService>();

// Configurar controllers e Swagger
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
