using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows.Forms;
using ApiClient;

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
                    string baseUrl = "https://localhost:7145";

                    // Registramos HttpClients con DI
                    services.AddHttpClient<ProvinciaApiClient>(client =>
                    {
                        client.BaseAddress = new Uri(baseUrl);
                    });

                    services.AddHttpClient<LocalidadApiClient>(client =>
                    {
                        client.BaseAddress = new Uri(baseUrl);
                    });

                    services.AddHttpClient<PersonaApiClient>(client =>
                    {
                        client.BaseAddress = new Uri(baseUrl);
                    });

                    services.AddHttpClient<TipoProductoApiClient>(client =>
                    {
                        client.BaseAddress = new Uri(baseUrl);
                    });

                    services.AddHttpClient<ProductoApiClient>(client =>
                    {
                        client.BaseAddress = new Uri(baseUrl);
                    });

                    // Formularios principales
                    services.AddTransient<MainForm>();
                    services.AddTransient<ProvinciaForm>();
                    services.AddTransient<TipoProductoForm>();
                    services.AddTransient<PersonaForm>();
                    services.AddTransient<ProductoForm>(sp =>
                        new ProductoForm(
                            sp.GetRequiredService<ProductoApiClient>(),
                            sp.GetRequiredService<TipoProductoApiClient>()
                        )
                    );

                    // Formularios de creación
                    services.AddTransient<CrearProductoForm>();
                    services.AddTransient<CrearTipoProductoForm>();
                    services.AddTransient<CrearPersonaForm>();

                    // Formularios de edición
                    services.AddTransient<EditarProductoForm>();
                    services.AddTransient<EditarTipoProductoForm>();
                    services.AddTransient<EditarPersonaForm>();
                })
                .Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var mainForm = services.GetRequiredService<MainForm>();
                Application.Run(mainForm);
            }
        }
    }
}
