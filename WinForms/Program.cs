using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ApiClient;
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
            var host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                var apiBaseAddress = new Uri("https://localhost:7145/");

                HttpMessageHandler CreateHandler()
                {
                    if (context.HostingEnvironment.IsDevelopment())
                        return new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, __, ___, ____) => true };
                    return new HttpClientHandler();
                }

                services.AddHttpClient<PersonaApiClient>(c => c.BaseAddress = apiBaseAddress).ConfigurePrimaryHttpMessageHandler(CreateHandler);
                services.AddHttpClient<PrecioCompraApiClient>(c => c.BaseAddress = apiBaseAddress).ConfigurePrimaryHttpMessageHandler(CreateHandler);
                services.AddHttpClient<ProvinciaApiClient>(c => c.BaseAddress = apiBaseAddress).ConfigurePrimaryHttpMessageHandler(CreateHandler);
                services.AddHttpClient<LocalidadApiClient>(c => c.BaseAddress = apiBaseAddress).ConfigurePrimaryHttpMessageHandler(CreateHandler);
                services.AddHttpClient<ProductoApiClient>(c => c.BaseAddress = apiBaseAddress).ConfigurePrimaryHttpMessageHandler(CreateHandler);
                services.AddHttpClient<TipoProductoApiClient>(c => c.BaseAddress = apiBaseAddress).ConfigurePrimaryHttpMessageHandler(CreateHandler);
                services.AddHttpClient<ProveedorApiClient>(c => c.BaseAddress = apiBaseAddress).ConfigurePrimaryHttpMessageHandler(CreateHandler);
                services.AddHttpClient<PrecioVentaApiClient>(c => c.BaseAddress = apiBaseAddress).ConfigurePrimaryHttpMessageHandler(CreateHandler);
                services.AddHttpClient<VentaApiClient>(c => c.BaseAddress = apiBaseAddress).ConfigurePrimaryHttpMessageHandler(CreateHandler);
                services.AddHttpClient<LineaVentaApiClient>(c => c.BaseAddress = apiBaseAddress).ConfigurePrimaryHttpMessageHandler(CreateHandler);
                services.AddHttpClient<PedidoApiClient>(c => c.BaseAddress = apiBaseAddress).ConfigurePrimaryHttpMessageHandler(CreateHandler);
                services.AddHttpClient<LineaPedidoApiClient>(c => c.BaseAddress = apiBaseAddress).ConfigurePrimaryHttpMessageHandler(CreateHandler);
                services.AddHttpClient<ProductoProveedorApiClient>(c => c.BaseAddress = apiBaseAddress).ConfigurePrimaryHttpMessageHandler(CreateHandler);


                // --- Servicios ---
                services.AddSingleton<UserSessionService>();

                // --- Formularios ---
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
            }).Build();

            using var scope = host.Services.CreateScope();
            var sp = scope.ServiceProvider;

            var loginForm = sp.GetRequiredService<LoginForm>();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                var mainForm = sp.GetRequiredService<MainForm>();
                Application.Run(mainForm);
            }
        }
    }
}
