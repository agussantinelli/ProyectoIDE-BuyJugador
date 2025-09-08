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

                    services.AddHttpClient<TipoProductoApiClient>(client =>
                    {
                        client.BaseAddress = new Uri(baseUrl);
                    });

                    services.AddHttpClient<ProductoApiClient>(client =>
                    {
                        client.BaseAddress = new Uri(baseUrl);
                    });

                    // Registramos los formularios
                    services.AddTransient<MainForm>();
                    services.AddTransient<ProductoForm>();
                    services.AddTransient<Provincia>();
                    services.AddTransient<TipoProducto>();
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
