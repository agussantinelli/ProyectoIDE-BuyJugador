using ApiClient;
using BlazorApp;
using BlazorApp.Auth;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;
using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var cultureInfo = new CultureInfo("es-AR");
NumberFormatInfo numberFormat = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
numberFormat.CurrencySymbol = "$";
numberFormat.CurrencyPositivePattern = 2;
cultureInfo.NumberFormat = numberFormat;
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBase = builder.Configuration["ApiBaseUrl"]
                 ?? Environment.GetEnvironmentVariable("API_BASE_URL")
                 ?? "https://localhost:7145/";
var apiUri = new Uri(apiBase);

builder.Services.AddScoped<InMemoryUserSession>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<UserSessionService>();
builder.Services.AddScoped<TokenMessageHandler>();

builder.Services.AddHttpClient("NoAuth", c => c.BaseAddress = apiUri);
builder.Services.AddHttpClient("Api", c => c.BaseAddress = apiUri)
              .AddHttpMessageHandler<TokenMessageHandler>();

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
builder.Services.AddScoped(sp => new ReporteApiClient(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api")));

builder.Logging.SetMinimumLevel(LogLevel.Debug);

await builder.Build().RunAsync();

