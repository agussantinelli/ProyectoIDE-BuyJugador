using BlazorApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ApiClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7145/") 
});

builder.Services.AddScoped<ProductoApiClient>();
builder.Services.AddScoped<ProveedorApiClient>();
builder.Services.AddScoped<PedidoApiClient>();
builder.Services.AddScoped<VentaApiClient>();
builder.Services.AddScoped<LineaPedidoApiClient>();
builder.Services.AddScoped<LineaVentaApiClient>();
builder.Services.AddScoped<LocalidadApiClient>();
builder.Services.AddScoped<PersonaApiClient>();
builder.Services.AddScoped<PrecioCompraApiClient>();
builder.Services.AddScoped<PrecioVentaApiClient>();
builder.Services.AddScoped<ProductoProveedorApiClient>();
builder.Services.AddScoped<ProvinciaApiClient>();
builder.Services.AddScoped<TipoProductoApiClient>();

await builder.Build().RunAsync();
