using ApiClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {

                    var apiBaseAddress = new Uri("https://localhost:7145/");


                    services.AddSingleton<UserSessionService>();

                    services.AddTransient<TokenMessageHandler>();


                    Action<HttpClient> configureClient = client => client.BaseAddress = apiBaseAddress;

                    services.AddHttpClient<PersonaApiClient>(configureClient)
                        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, __, ___, ____) => true })
                        .AddHttpMessageHandler<TokenMessageHandler>();

                    services.AddHttpClient<ProductoApiClient>(configureClient)
                        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, __, ___, ____) => true })
                        .AddHttpMessageHandler<TokenMessageHandler>();

                    services.AddHttpClient<ProveedorApiClient>(configureClient)
                        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, __, ___, ____) => true })
                        .AddHttpMessageHandler<TokenMessageHandler>();

                    services.AddHttpClient<PedidoApiClient>(configureClient)
                        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, __, ___, ____) => true })
                        .AddHttpMessageHandler<TokenMessageHandler>();

                    services.AddHttpClient<VentaApiClient>(configureClient)
                        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, __, ___, ____) => true })
                        .AddHttpMessageHandler<TokenMessageHandler>();

                    services.AddHttpClient<LineaPedidoApiClient>(configureClient)
                        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, __, ___, ____) => true })
                        .AddHttpMessageHandler<TokenMessageHandler>();

                    services.AddHttpClient<LineaVentaApiClient>(configureClient)
                        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, __, ___, ____) => true })
                        .AddHttpMessageHandler<TokenMessageHandler>();

                    services.AddHttpClient<LocalidadApiClient>(configureClient)
                        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, __, ___, ____) => true })
                        .AddHttpMessageHandler<TokenMessageHandler>();

                    services.AddHttpClient<PrecioCompraApiClient>(configureClient)
                        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, __, ___, ____) => true })
                        .AddHttpMessageHandler<TokenMessageHandler>();

                    services.AddHttpClient<PrecioVentaApiClient>(configureClient)
                        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, __, ___, ____) => true })
                        .AddHttpMessageHandler<TokenMessageHandler>();

                    services.AddHttpClient<ProductoProveedorApiClient>(configureClient)
                        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, __, ___, ____) => true })
                        .AddHttpMessageHandler<TokenMessageHandler>();

                    services.AddHttpClient<ProvinciaApiClient>(configureClient)
                        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, __, ___, ____) => true })
                        .AddHttpMessageHandler<TokenMessageHandler>();

                    services.AddHttpClient<TipoProductoApiClient>(configureClient)
                        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, __, ___, ____) => true })
                        .AddHttpMessageHandler<TokenMessageHandler>();


                    services.AddTransient<LoginForm>();
                    services.AddTransient<MainForm>();
                    services.AddTransient<PersonaForm>();
                    services.AddTransient<CrearPersonaForm>();
                    services.AddTransient<EditarPersonaForm>();
                    services.AddTransient<ProvinciaForm>();
                    services.AddTransient<LocalidadForm>();
                    services.AddTransient<TipoProductoForm>();
                    services.AddTransient<CrearTipoProductoForm>();
                    services.AddTransient<EditarTipoProductoForm>();
                    services.AddTransient<ProductoForm>();
                    services.AddTransient<CrearProductoForm>();
                    services.AddTransient<EditarProductoForm>();
                    services.AddTransient<ProveedorForm>();
                    services.AddTransient<CrearProveedorForm>();
                    services.AddTransient<EditarProveedorForm>();
                    services.AddTransient<HistorialPreciosForm>();
                    services.AddTransient<EditarPrecioForm>();
                    services.AddTransient<VentaForm>();
                    services.AddTransient<CrearVentaForm>();
                    services.AddTransient<DetalleVentaForm>();
                    services.AddTransient<AñadirProductoVentaForm>();
                    services.AddTransient<PedidoForm>();
                    services.AddTransient<CrearPedidoForm>();
                    services.AddTransient<DetallePedidoForm>();
                    services.AddTransient<AsignarProductosProveedorForm>();
                    services.AddTransient<AñanirProductoPedidoForm>();
                })
                .Build();

            while (true)
            {
                var userSessionService = host.Services.GetRequiredService<UserSessionService>();

                using (var loginForm = host.Services.GetRequiredService<LoginForm>())
                {
                    if (loginForm.ShowDialog() != DialogResult.OK)
                    {
                        break; 
                    }
                }

                using (var mainForm = host.Services.GetRequiredService<MainForm>())
                {
                    Application.Run(mainForm);
                }

                if (userSessionService.CurrentUser == null)
                {
                    continue; 
                }
                else
                {
                    break; 
                }
            }
        }
    }
}

