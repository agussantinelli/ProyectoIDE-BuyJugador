using Data;
using DominioServicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PdfSharp.Fonts;
using System.Text;
using WebAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n para generaci�n de PDF
PdfReportService.Configure();
GlobalFontSettings.FontResolver = new FontResolver();

// 1. CONFIGURACI�N DE CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp",
        policy => policy.WithOrigins("https://localhost:7035")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// 2. REGISTRO DE SERVICIOS
// Conexi�n a la base de datos
builder.Services.AddDbContext<BuyJugadorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BuyJugadorConnection")));

// #CAMBIO: Registrar UnitOfWork.
// #Intenci�n: Registrar la Unidad de Trabajo con un ciclo de vida "Scoped".
// #Esto significa que se crear� una �nica instancia de UnitOfWork por cada solicitud HTTP,
// #asegurando que todos los repositorios y servicios en esa solicitud compartan la misma transacci�n.
builder.Services.AddScoped<UnitOfWork>();

// #CAMBIO: Los servicios de dominio ahora dependen de UnitOfWork, no directamente de DbContext.
// La inyecci�n de dependencias se encargar� de pasar la instancia Scoped de UnitOfWork a cada servicio.
builder.Services.AddScoped<PersonaService>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<ProveedorService>();
builder.Services.AddScoped<TipoProductoService>();
builder.Services.AddScoped<ProvinciaService>();
builder.Services.AddScoped<LocalidadService>();
builder.Services.AddScoped<ProductoProveedorService>();
builder.Services.AddScoped<PrecioVentaService>();
builder.Services.AddScoped<PrecioCompraService>();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<VentaService>();
builder.Services.AddScoped<LineaPedidoService>();
builder.Services.AddScoped<LineaVentaService>();
builder.Services.AddScoped<ReporteService>();
builder.Services.AddScoped<PdfReportService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BuyJugador API", Version = "v1" });
});

// 3. CONFIGURACI�N DE AUTENTICACI�N Y AUTORIZACI�N (JWT)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// 4. CONFIGURACI�N DEL PIPELINE DE HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BuyJugador API v1"));

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<BuyJugadorContext>();
        await DbSeeder.SeedAsync(context);
    }
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorApp");
app.UseAuthentication();
app.UseAuthorization();

// 5. MAPEADO DE ENDPOINTS (sin cambios aqu�)
app.MapAuthenticationEndpoints();
app.MapPersonaEndpoints();
app.MapProductoEndpoints();
app.MapProveedorEndpoints();
app.MapTipoProductoEndpoints();
app.MapProvinciaEndpoints();
app.MapLocalidadEndpoints();
app.MapProductoProveedorEndpoints();
app.MapPrecioVentaEndpoints();
app.MapPrecioCompraEndpoints();
app.MapPedidoEndpoints();
app.MapVentaEndpoints();
app.MapLineaPedidoEndpoints();
app.MapLineaVentaEndpoints();
app.MapReporteEndpoints();

app.Run();
