using Data;
using DominioServicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          // Aquí pones la dirección de tu app Blazor
                          policy.WithOrigins("https://localhost:7035")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});


#region Builder Configuration

var connectionString = builder.Configuration.GetConnectionString("BuyJugadorConnection");

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
builder.Services.AddScoped<PrecioCompraService>();
builder.Services.AddScoped<PrecioVentaService>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<ProveedorService>();
builder.Services.AddScoped<ProvinciaService>();
builder.Services.AddScoped<TipoProductoService>();
builder.Services.AddScoped<VentaService>();
builder.Services.AddScoped<ProductoProveedorService>();

#endregion

var app = builder.Build();

#region Application Pipeline Configuration

// # Configura el pipeline de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
    // # En desarrollo, habilita Swagger para documentar y probar la API.
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // # En producción, usa un manejador de excepciones y HSTS.
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ***** UBICACIÓN CORRECTA PARA CORS *****
// Debe ir después de UseRouting() y antes de UseAuthorization().
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapRazorPages();

#endregion

#region Database Seeding

// # Ejecuta el seeder para poblar la base de datos con datos iniciales.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<BuyJugadorContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        // # Es una buena práctica ejecutar tareas asíncronas de inicialización aquí.
        await DbSeeder.SeedAsync(context);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

#endregion

#region API Endpoints Registration

// # Mapea todos los grupos de endpoints de la API.
app.MapProductoEndpoints();
app.MapProveedorEndpoints();
app.MapPersonaEndpoints();
app.MapLineaPedidoEndpoints();
app.MapLineaVentaEndpoints();
app.MapLocalidadEndpoints();
app.MapPedidoEndpoints();
app.MapPrecioCompraEndpoints();
app.MapPrecioVentaEndpoints();
app.MapProvinciaEndpoints();
app.MapTipoProductoEndpoints();
app.MapVentaEndpoints();
app.MapProductoProveedorEndpoints();

#endregion

app.Run();
