using BlazorApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ApiClient;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBase = builder.Configuration["ApiBaseUrl"]
           ?? Environment.GetEnvironmentVariable("API_BASE_URL")
           ?? "https://localhost:7145/"; 

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBase) });


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

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Logging.SetMinimumLevel(LogLevel.Warning);

await builder.Build().RunAsync();
