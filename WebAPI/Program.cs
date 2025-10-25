using Data;
using DominioServicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PdfSharp.Fonts;
using System.Text;
using WebAPI.Endpoints;
using Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
ReporteService.Configure();
GlobalFontSettings.FontResolver = new FontResolver();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp",
        policy => policy.WithOrigins("https://localhost:7035")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});
builder.Services.AddDbContext<BuyJugadorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BuyJugadorConnection")));

builder.Services.AddScoped<ReporteRepository>();

builder.Services.AddScoped<UnitOfWork>();
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


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BuyJugador API", Version = "v1" });
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
