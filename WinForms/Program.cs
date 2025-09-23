using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ApiClient;
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
                    string apiBaseAddress = "http://localhost:7145"; 

                    // Registra los clientes de API con HttpClientFactory.
                    // Esto crea los ApiClients y les inyecta automáticamente un HttpClient configurado.
                    services.AddHttpClient<PersonaApiClient>(client => client.BaseAddress = new Uri(apiBaseAddress));
                    services.AddHttpClient<ProvinciaApiClient>(client => client.BaseAddress = new Uri(apiBaseAddress));
                    services.AddHttpClient<LocalidadApiClient>(client => client.BaseAddress = new Uri(apiBaseAddress));
                    services.AddHttpClient<TipoProductoApiClient>(client => client.BaseAddress = new Uri(apiBaseAddress));
                    services.AddHttpClient<ProductoApiClient>(client => client.BaseAddress = new Uri(apiBaseAddress));

                    // Registra los formularios para que puedan ser inyectados
                    services.AddTransient<LoginForm>();
                    services.AddTransient<MainForm>();
                    services.AddTransient<EmpleadoForm>();
                    services.AddTransient<PersonaForm>();
                    services.AddTransient<ProductoForm>();
                    services.AddTransient<TipoProductoForm>();
                })
                .Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var loginForm = services.GetRequiredService<LoginForm>();

                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    var mainForm = services.GetRequiredService<MainForm>();
                    // Le pasamos el rol al MainForm después de crearlo
                    mainForm.EstablecerRol(loginForm.RolUsuario);
                    Application.Run(mainForm);
                }
            }
        }
    }
}
