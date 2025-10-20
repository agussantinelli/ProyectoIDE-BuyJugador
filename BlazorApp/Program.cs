using ApiClient;
using BlazorApp;
using BlazorApp.Auth;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// --- 1. Configuración de Cultura para formato de moneda ARS ---
var cultureInfo = new CultureInfo("es-AR");
NumberFormatInfo numberFormat = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
numberFormat.CurrencySymbol = "$";
numberFormat.CurrencyPositivePattern = 2;
cultureInfo.NumberFormat = numberFormat;
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// --- 2. Configuración de la URL base de la API ---
var apiBase = builder.Configuration["ApiBaseUrl"]
              ?? Environment.GetEnvironmentVariable("API_BASE_URL")
              ?? "https://localhost:7145/";
var apiUri = new Uri(apiBase);

// --- 3. Configuración de Servicios de Autenticación y Local Storage ---
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<UserSessionService>();
builder.Services.AddScoped<TokenMessageHandler>(); // Registramos el manejador de tokens

// --- 4. Configuración Centralizada de HttpClient ---
// Cliente SIN autenticación para el endpoint de login.
builder.Services.AddHttpClient("NoAuth", c => c.BaseAddress = apiUri);

// Cliente CON autenticación que será usado por todos los ApiClients.
// ---- MODIFICACIÓN CLAVE ----
// Usamos una función de fábrica para asegurar la correcta resolución del handler.
builder.Services.AddHttpClient("Api", c => c.BaseAddress = apiUri)
                .AddHttpMessageHandler(sp => sp.GetRequiredService<TokenMessageHandler>());

// --- 5. Registro Unificado de todos los ApiClients ---
// Todos los ApiClients se registran para usar el IHttpClientFactory y obtener el cliente "Api"
// que ya está configurado para la autenticación.
builder.Services.AddScoped(sp => new VentaApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new PersonaApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new ProductoApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new ProveedorApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new TipoProductoApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new ProvinciaApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new LocalidadApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new PrecioVentaApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new PrecioCompraApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new ProductoProveedorApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new LineaVentaApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new PedidoApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));
builder.Services.AddScoped(sp => new LineaPedidoApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));


builder.Logging.SetMinimumLevel(LogLevel.Debug);

await builder.Build().RunAsync();

