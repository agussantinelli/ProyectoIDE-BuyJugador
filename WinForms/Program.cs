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

<<<<<<< HEAD
                    // --- REGISTRO DE NUEVOS FORMULARIOS ---
                    services.AddTransient<MainForm>(); // Form de Dueño
                    services.AddTransient<EmpleadoForm>(); // Form de Empleado
                    services.AddTransient<LoginForm>(); // Form de Login


                    services.AddTransient<Provincia>();
                    services.AddTransient<TipoProducto>();
                    services.AddTransient<Persona>();
                    services.AddTransient<Producto>(sp =>
                        new Producto(
=======
                    // Formularios principales
                    services.AddTransient<MainForm>();
                    services.AddTransient<ProvinciaForm>();
                    services.AddTransient<TipoProductoForm>();
                    services.AddTransient<PersonaForm>();
                    services.AddTransient<ProductoForm>(sp =>
                        new ProductoForm(
>>>>>>> 33fd96e74a3519b0bfc220b2bb8b33cdf07f037e
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
<<<<<<< HEAD

=======
>>>>>>> 33fd96e74a3519b0bfc220b2bb8b33cdf07f037e
                })
                .Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                // Abre el formulario de Login primero
                var loginForm = services.GetRequiredService<LoginForm>();
                var result = loginForm.ShowDialog();

                // Si el login fue exitoso, decide qué formulario mostrar
                if (result == DialogResult.OK)
                {
                    if (loginForm.RolUsuario == "Dueño")
                    {
                        var mainForm = services.GetRequiredService<MainForm>();
                        Application.Run(mainForm);
                    }
                    else // Por defecto, si no es Dueño, es Empleado
                    {
                        var empleadoForm = services.GetRequiredService<EmpleadoForm>();
                        Application.Run(empleadoForm);
                    }
                }
                else
                {
                    // Si el usuario cierra el login, la aplicación termina
                    Application.Exit();
                }
            }
        }
    }
}
