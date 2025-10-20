using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorApp.Auth
{
    public class TokenMessageHandler : DelegatingHandler
    {
        // # Intención: Cambiar la dependencia al servicio de sesión en memoria.
        private readonly InMemoryUserSession _userSession;

        public TokenMessageHandler(InMemoryUserSession userSession)
        {
            _userSession = userSession;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                // # Leemos el token desde el servicio en memoria. Es síncrono, no se necesita 'await'.
                var token = _userSession.Token;

                if (!string.IsNullOrWhiteSpace(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
            catch (Exception ex)
            {
                // # Es una buena práctica registrar el error en la consola para depuración.
                Console.WriteLine($"Error al adjuntar el token de autorización: {ex.Message}");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}

