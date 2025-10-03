using Data;
using DominioServicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

#region Builder Configuration

// 1. Se restaura la lectura de la cadena de conexión desde el archivo de configuración.
var connectionString = builder.Configuration.GetConnectionString("BuyJugadorConnection");

// 2. Se configura el DbContext para que use la cadena de conexión leída.
builder.Services.AddDbContext<BuyJugadorContext>(options =>
    options.UseSqlServer(connectionString)
           .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();

#endregion


#region Services Registration

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
// --- NUEVO SERVICIO AÑADIDO ---
builder.Services.AddScoped<ProductoProveedorService>();

#endregion

var app = builder.Build();


#region Application Pipeline Configuration

if (app.Environment.IsDevelopment())
{
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

// CORRECCIÓN: La llamada al seeder ahora es asíncrona para esperar a la API de localidades.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<BuyJugadorContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        // Se llama a la nueva versión asíncrona del método
        await DbSeeder.SeedAsync(context);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

#region API Endpoints Registration

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
app.MapProductoProveedorEndpoints();

#endregion

app.Run();

