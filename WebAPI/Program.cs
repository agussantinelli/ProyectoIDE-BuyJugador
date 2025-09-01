using Data;
using DominioServicios;
using Microsoft.EntityFrameworkCore;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

#region Builder Configuration

// 1. Configurar la conexión a la base de datos
var connectionString = builder.Configuration.GetConnectionString("BuyJugadorConnection");
builder.Services.AddDbContext<BuyJugadorContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddRazorPages();

#endregion

#region Services Registration

// 2. Registrar todos tus servicios para inyección de dependencias
// Se han agregado los servicios que faltaban para completar la funcionalidad CRUD
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

#endregion

var app = builder.Build();

#region Application Pipeline Configuration

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

#endregion

#region API Endpoints Registration

// 3. Registrar los endpoints de la API
// Se han agregado las llamadas para los nuevos endpoints creados
app.MapProductoEndpoints();
app.MapProveedorEndpoints();
app.MapDuenioEndpoints();
app.MapEmpleadoEndpoints();
app.MapLineaPedidoEndpoints();
app.MapLineaVentaEndpoints();
app.MapLocalidadEndpoints();
app.MapPedidoEndpoints();
//app.MapPrecioEndpoints();
//app.MapProvinciaEndpoints();
//app.MapTipoProductoEndpoints();
//app.MapVentaEndpoints();

#endregion

app.Run();
