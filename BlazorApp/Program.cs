using BlazorApp;
using BlazorApp.Auth;
using System.Net.Http;               
using Microsoft.Extensions.DependencyInjection; 
using BlazorApp.Auth;               
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using ApiClient;                             

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Base de tu WebAPI
var apiBase = builder.Configuration["ApiBaseUrl"]
           ?? Environment.GetEnvironmentVariable("API_BASE_URL")
           ?? "https://localhost:7145/";
var apiUri = new Uri(apiBase);

// ---------- AUTH ----------
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<CustomAuthenticationStateProvider>());

// Handler que agrega el Bearer a TODAS las llamadas autenticadas
builder.Services.AddScoped<TokenMessageHandler>();

// ---------- HTTP CLIENTS ----------
// Cliente SIN token (para /login)
builder.Services.AddHttpClient("NoAuth", c => c.BaseAddress = apiUri);

// Cliente CON token (para el resto de la API)
builder.Services.AddHttpClient("Api", c => c.BaseAddress = apiUri)
                .AddHttpMessageHandler<TokenMessageHandler>();

// ---------- TUS API CLIENTS (usan el cliente "Api") ----------
builder.Services.AddScoped(sp => new ProductoApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new ProveedorApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new PedidoApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new VentaApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new LineaPedidoApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new LineaVentaApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new LocalidadApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new PersonaApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new PrecioCompraApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new PrecioVentaApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new ProductoProveedorApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new ProvinciaApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new TipoProductoApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));

// Log detallado en desarrollo
builder.Logging.SetMinimumLevel(LogLevel.Debug);

await builder.Build().RunAsync();

