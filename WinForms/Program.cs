using ApiClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
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
                    services.AddHttpClient<ProvinciaApiClient>(client =>
                    {
                        // Asegúrate de que este puerto coincida con el de tu WebAPI
                        client.BaseAddress = new Uri("https://localhost:7145");
                    });
                    services.AddHttpClient<TipoProductoApiClient>(client =>
                    {
                        client.BaseAddress = new Uri("https://localhost:7145");
                    });

                    // Registramos el formulario principal
                    services.AddTransient<MainForm>();
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

