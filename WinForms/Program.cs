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

            // 1. Se configura y construye el host que manejar� las dependencias.
            var host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                var apiBaseAddress = new Uri("https://localhost:7145/"); // Aseg�rate que este puerto sea el correcto

                // Funci�n para manejar certificados SSL en desarrollo
                HttpMessageHandler CreateHandler()
                {
                    if (context.HostingEnvironment.IsDevelopment())
                        return new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, __, ___, ____) => true };
                    return new HttpClientHandler();
                }

                // Registro de todos los ApiClients
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
                services.AddTransient<A�adirProductoVentaForm>();
                services.AddTransient<PedidoForm>();
                services.AddTransient<CrearPedidoForm>();
                services.AddTransient<DetallePedidoForm>();
                services.AddTransient<AsignarProductosProveedorForm>();
                services.AddTransient<A�anirProductoPedidoForm>(); // Correg� el nombre que estaba en tu c�digo original
            }).Build();

            // 2. Bucle principal que controla el flujo de la aplicaci�n
            while (true)
            {
                var userSessionService = host.Services.GetRequiredService<UserSessionService>();

                // Usamos el Service Provider del host para obtener una nueva instancia del LoginForm
                using (var loginForm = host.Services.GetRequiredService<LoginForm>())
                {
                    // Si el usuario cierra el login, salimos del bucle y la app termina.
                    if (loginForm.ShowDialog() != DialogResult.OK)
                    {
                        break;
                    }
                }

                // Si el login fue exitoso, creamos y corremos el formulario principal.
                using (var mainForm = host.Services.GetRequiredService<MainForm>())
                {
                    Application.Run(mainForm);
                }

                // Despu�s de que MainForm se cierra, verificamos si fue por un logout.
                // Si CurrentUser es null, es porque se llam� a Logout().
                if (userSessionService.CurrentUser == null)
                {
                    // Si fue un logout, el bucle 'while' se reinicia y vuelve a mostrar el LoginForm.
                    continue;
                }
                else
                {
                    // Si se cerr� con la 'X', la sesi�n NO es null, y salimos del bucle para cerrar la app.
                    break;
                }
            }
        }
    }
}