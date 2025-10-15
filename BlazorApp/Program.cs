using BlazorApp;
using BlazorApp.Auth;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using ApiClient;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<BlazorApp.App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBase = builder.Configuration["ApiBaseUrl"]
            ?? Environment.GetEnvironmentVariable("API_BASE_URL")
            ?? "https://localhost:7145/";
var apiUri = new Uri(apiBase);

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddScoped<BlazorApp.Auth.TokenMessageHandler>();

builder.Services.AddHttpClient("NoAuth", c => c.BaseAddress = apiUri);
builder.Services.AddHttpClient("Api", c => c.BaseAddress = apiUri)
               .AddHttpMessageHandler<BlazorApp.Auth.TokenMessageHandler>();

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

builder.Logging.SetMinimumLevel(LogLevel.Debug);

await builder.Build().RunAsync();

