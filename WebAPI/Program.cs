using Data;
using DominioServicios;
using Microsoft.EntityFrameworkCore;
using WebAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

#region Builder Configuration

var connectionString = builder.Configuration.GetConnectionString("BuyJugadorConnection");
builder.Services.AddDbContext<BuyJugadorContext>(options =>
    options.UseSqlServer(connectionString));

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

#endregion

app.Run();
