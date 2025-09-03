using Data;
using DominioServicios;
using Microsoft.EntityFrameworkCore;
using WebAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

#region Builder Configuration

// 1. Configurar la conexión a la base de datos
// Lee la cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("BuyJugadorConnection");
builder.Services.AddDbContext<BuyJugadorContext>(options =>
    options.UseSqlServer(connectionString));

// ** AÑADIDO: Necesario para que la API pueda descubrir y usar los endpoints. **
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add services to the container. (Este ya lo tenías, lo muevo aquí por orden)
builder.Services.AddRazorPages();

#endregion

#region Services Registration

// 2. Registrar todos tus servicios para inyección de dependencias
// Esto está perfecto, no se necesita cambiar nada.
builder.Services.AddScoped<PersonaService>();
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
if (app.Environment.IsDevelopment())
{
    // ** AÑADIDO: Habilita la interfaz de Swagger solo en desarrollo **
    // Esto te dará una página para probar tu API fácilmente.
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
// Esto también está perfecto.
app.MapProductoEndpoints();
app.MapProveedorEndpoints();
app.MapPersonaEndpoints();
app.MapLineaPedidoEndpoints();
app.MapLineaVentaEndpoints();
app.MapLocalidadEndpoints();
app.MapPedidoEndpoints();
app.MapPrecioEndpoints();
app.MapProvinciaEndpoints();
app.MapTipoProductoEndpoints();
app.MapVentaEndpoints();

#endregion

app.Run();
