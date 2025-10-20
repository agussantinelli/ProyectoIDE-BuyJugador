using Blazored.LocalStorage;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorApp.Auth
{
    // PIEZA 1: El "Mensajero"
    // Su única tarea es tomar el token del almacenamiento y adjuntarlo a cada petición.
    public class TokenMessageHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;

        public TokenMessageHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                // Antes de enviar la petición, intentamos obtener el token.
                var token = await _localStorage.GetItemAsync<string>("authToken", cancellationToken);

                // Si existe, lo ponemos en la cabecera "Authorization".
                if (!string.IsNullOrWhiteSpace(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al adjuntar el token de autorización: {ex.Message}");
            }

            // Enviamos la petición al servidor (con o sin el token).
            return await base.SendAsync(request, cancellationToken);
        }
    }
}

