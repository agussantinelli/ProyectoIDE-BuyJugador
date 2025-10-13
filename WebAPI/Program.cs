using Data;
using DominioServicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// # 1. Configuraci�n de Servicios

builder.Services.AddDbContext<BuyJugadorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BuyJugadorConnection")));

// # Agrega los servicios de dominio
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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BuyJugador API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp",
        builder => builder.WithOrigins("https://localhost:7035")
                           .AllowAnyHeader()
                           .AllowAnyMethod());
});

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

// # 2. Configuraci�n del Pipeline de Middleware

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

// # 3. Mapeo de Endpoints
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

app.Run();

