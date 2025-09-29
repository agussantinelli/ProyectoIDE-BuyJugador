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

                    // Form principal
                    services.AddTransient<MainForm>();
                    services.AddTransient<EmpleadoForm>();
                    services.AddTransient<LoginForm>();

                    // Formularios hijos
                    services.AddTransient<PersonaForm>();
                    services.AddTransient<ProductoForm>();
                    services.AddTransient<ProvinciaForm>();
                    services.AddTransient<LocalidadForm>();
                    services.AddTransient<TipoProductoForm>();

                })
                .Build();

            using var scope = host.Services.CreateScope();
            var sp = scope.ServiceProvider;

            var loginForm = sp.GetRequiredService<LoginForm>();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                if (loginForm.RolUsuario == "Dueño")
                {
                    var mainForm = sp.GetRequiredService<MainForm>();
                    Application.Run(mainForm);
                }
                else if (loginForm.RolUsuario == "Empleado")
                {
                    var empleadoForm = sp.GetRequiredService<EmpleadoForm>();
                    Application.Run(empleadoForm);
                }
            }

        }
    }
}