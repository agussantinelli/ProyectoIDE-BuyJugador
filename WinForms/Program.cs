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


                    var handler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, __, ___, ____) => true };

                    services.AddHttpClient<PersonaApiClient>(configureClient).ConfigurePrimaryHttpMessageHandler(() => handler).AddHttpMessageHandler<TokenMessageHandler>();
                    services.AddHttpClient<ProductoApiClient>(configureClient).ConfigurePrimaryHttpMessageHandler(() => handler).AddHttpMessageHandler<TokenMessageHandler>();
                    services.AddHttpClient<ProveedorApiClient>(configureClient).ConfigurePrimaryHttpMessageHandler(() => handler).AddHttpMessageHandler<TokenMessageHandler>();
                    services.AddHttpClient<PedidoApiClient>(configureClient).ConfigurePrimaryHttpMessageHandler(() => handler).AddHttpMessageHandler<TokenMessageHandler>();
                    services.AddHttpClient<VentaApiClient>(configureClient).ConfigurePrimaryHttpMessageHandler(() => handler).AddHttpMessageHandler<TokenMessageHandler>();
                    services.AddHttpClient<LineaPedidoApiClient>(configureClient).ConfigurePrimaryHttpMessageHandler(() => handler).AddHttpMessageHandler<TokenMessageHandler>();
                    services.AddHttpClient<LineaVentaApiClient>(configureClient).ConfigurePrimaryHttpMessageHandler(() => handler).AddHttpMessageHandler<TokenMessageHandler>();
                    services.AddHttpClient<LocalidadApiClient>(configureClient).ConfigurePrimaryHttpMessageHandler(() => handler).AddHttpMessageHandler<TokenMessageHandler>();
                    services.AddHttpClient<PrecioCompraApiClient>(configureClient).ConfigurePrimaryHttpMessageHandler(() => handler).AddHttpMessageHandler<TokenMessageHandler>();
                    services.AddHttpClient<PrecioVentaApiClient>(configureClient).ConfigurePrimaryHttpMessageHandler(() => handler).AddHttpMessageHandler<TokenMessageHandler>();
                    services.AddHttpClient<ProductoProveedorApiClient>(configureClient).ConfigurePrimaryHttpMessageHandler(() => handler).AddHttpMessageHandler<TokenMessageHandler>();
                    services.AddHttpClient<ProvinciaApiClient>(configureClient).ConfigurePrimaryHttpMessageHandler(() => handler).AddHttpMessageHandler<TokenMessageHandler>();
                    services.AddHttpClient<TipoProductoApiClient>(configureClient).ConfigurePrimaryHttpMessageHandler(() => handler).AddHttpMessageHandler<TokenMessageHandler>();
                    services.AddHttpClient<ReporteApiClient>(configureClient).ConfigurePrimaryHttpMessageHandler(() => handler).AddHttpMessageHandler<TokenMessageHandler>();

                    // #Intención: Registrar todos los formularios para inyección de dependencias.
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
                    services.AddTransient<ReporteHistorialPreciosForm>();
                    services.AddTransient<EditarPrecioForm>();
                    services.AddTransient<VentaForm>();
                    services.AddTransient<CrearVentaForm>();
                    services.AddTransient<DetalleVentaForm>();
                    services.AddTransient<AñadirProductoVentaForm>();
                    services.AddTransient<PedidoForm>();
                    services.AddTransient<CrearPedidoForm>();
                    services.AddTransient<DetallePedidoForm>();
                    services.AddTransient<AsignarProductosProveedorForm>();
                    services.AddTransient<AñadirProductoPedidoForm>();
                    services.AddTransient<VerProductosProveedorForm>();
                    services.AddTransient<VerProveedoresProductoForm>();
                    services.AddTransient<ReporteVentasForm>();
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

                // #Lógica Restaurada: Se mantiene tu flujo original para el ciclo de la aplicación.
                if (userSessionService.CurrentUser == null)
                {
                    continue; // El usuario cerró sesión, volver al login.
                }
                else
                {
                    break; // El usuario cerró la ventana principal, terminar la aplicación.
                }
            }
        }
    }
}