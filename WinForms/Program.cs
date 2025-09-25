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

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    var apiBaseAddress = new Uri("https://localhost:7145/");

                    // Handler: ignorar certificado SOLO en Desarrollo (para localhost)
                    HttpMessageHandler CreateHandler()
                    {
                        if (context.HostingEnvironment.IsDevelopment())
                        {
                            return new HttpClientHandler
                            {
                                ServerCertificateCustomValidationCallback = (_, __, ___, ____) => true
                            };
                        }
                        return new HttpClientHandler();
                    }

                    // HttpClientFactory para tus ApiClients
                    services.AddHttpClient<PersonaApiClient>(c => c.BaseAddress = apiBaseAddress)
                            .ConfigurePrimaryHttpMessageHandler(CreateHandler);
                    services.AddHttpClient<ProvinciaApiClient>(c => c.BaseAddress = apiBaseAddress)
                            .ConfigurePrimaryHttpMessageHandler(CreateHandler);
                    services.AddHttpClient<LocalidadApiClient>(c => c.BaseAddress = apiBaseAddress)
                            .ConfigurePrimaryHttpMessageHandler(CreateHandler);
                    services.AddHttpClient<TipoProductoApiClient>(c => c.BaseAddress = apiBaseAddress)
                            .ConfigurePrimaryHttpMessageHandler(CreateHandler);
                    services.AddHttpClient<ProductoApiClient>(c => c.BaseAddress = apiBaseAddress)
                            .ConfigurePrimaryHttpMessageHandler(CreateHandler);

                    // Formularios
                    services.AddTransient<LoginForm>();
                    services.AddTransient<MainForm>();
                    services.AddTransient<EmpleadoForm>();
                    services.AddTransient<PersonaForm>();
                    services.AddTransient<ProductoForm>();
                    services.AddTransient<ProvinciaForm>();

                    services.AddTransient<TipoProductoForm>();
                })
                .Build();

            using var scope = host.Services.CreateScope();
            var sp = scope.ServiceProvider;

            var loginForm = sp.GetRequiredService<LoginForm>();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                var mainForm = sp.GetRequiredService<MainForm>();
                mainForm.EstablecerRol(loginForm.RolUsuario);
                Application.Run(mainForm);
            }
        }
    }
}