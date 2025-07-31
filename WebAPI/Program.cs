using DominioServicios;
using WebAPI.Endpoints;
using DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpLogging(o => { });

// Registrar los servicios de dominio como Singletons
builder.Services.AddSingleton<ProvinciaService>();
builder.Services.AddSingleton<TipoProductoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpLogging();
}

app.UseHttpsRedirection();

// Mapea los endpoints de las entidades
ProvinciaEndpoints.Map(app);
TipoProductoEndpoints.Map(app);

app.Run();