using Data;
using DominioServicios;
using Microsoft.EntityFrameworkCore;
// using WebAPI.Endpoints; // Descomentaremos esto cuando creemos los endpoints

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar la conexión a la base de datos
var connectionString = builder.Configuration.GetConnectionString("BuyJugadorConnection");
builder.Services.AddDbContext<BuyJugadorContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddRazorPages();

// 2. Registrar todos tus servicios para inyección de dependencias
builder.Services.AddScoped<DuenioService>();
builder.Services.AddScoped<EmpleadoService>();
builder.Services.AddScoped<LineaPedidoService>();
builder.Services.AddScoped<LineaVentaService>();
builder.Services.AddScoped<LocalidadService>();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<PrecioService>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<ProveedorService>();
builder.Services.AddScoped<ProvinciaService>();
builder.Services.AddScoped<TipoProductoService>();
builder.Services.AddScoped<VentaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// 3. Registrar los endpoints de la API (lo haremos más adelante)
// app.MapProductoEndpoints(); 

app.Run();
