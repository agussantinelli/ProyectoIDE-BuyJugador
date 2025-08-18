using Data; // Asegúrate de tener esta referencia
using DominioServicios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar la conexión a la base de datos
var connectionString = builder.Configuration.GetConnectionString("BuyJugadorConnection");
builder.Services.AddDbContext<BuyJugadorContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddRazorPages();

// 2. Registrar tus servicios para inyección de dependencias
// Esto permite que tus clases de servicio se puedan usar en los endpoints.
builder.Services.AddScoped<DuenioService>();
builder.Services.AddScoped<EmpleadoService>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<ProveedorService>();
builder.Services.AddScoped<VentaService>();
builder.Services.AddScoped<PrecioService>();
builder.Services.AddScoped<LocalidadService>();
builder.Services.AddScoped<DominioServicio.ProvinciaService>();
    
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

// 3. Aquí registraremos los endpoints de la API más adelante
// app.MapProductoEndpoints(); 

app.Run();