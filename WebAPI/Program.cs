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

// Configuración para generación de PDF
PdfReportService.Configure();
GlobalFontSettings.FontResolver = new FontResolver();

// 1. CONFIGURACIÓN DE CORS
// Se define una política llamada "AllowBlazorApp" que confía en el origen de tu aplicación Blazor.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp",
        policy => policy.WithOrigins("https://localhost:7035") // Origen de tu Blazor App
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});


// 2. REGISTRO DE SERVICIOS
// Conexión a la base de datos
builder.Services.AddDbContext<BuyJugadorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BuyJugadorConnection")));

// Servicios de dominio
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

// Servicios para API Endpoints y Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BuyJugador API", Version = "v1" });
});

// 3. CONFIGURACIÓN DE AUTENTICACIÓN Y AUTORIZACIÓN (JWT)
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

// 4. CONFIGURACIÓN DEL PIPELINE DE HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BuyJugador API v1"));

    // Seeder para la base de datos en entorno de desarrollo
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<BuyJugadorContext>();
        await DbSeeder.SeedAsync(context);
    }
}

app.UseHttpsRedirection();

// Se aplican las políticas en el orden correcto
app.UseCors("AllowBlazorApp");
app.UseAuthentication();
app.UseAuthorization();

// 5. MAPEADO DE ENDPOINTS
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
