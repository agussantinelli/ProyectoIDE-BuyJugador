using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace BlazorApp.Auth
{
    public class TokenMessageHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;
        private readonly InMemoryUserSession _inMemorySession;

        public TokenMessageHandler(ILocalStorageService localStorage, InMemoryUserSession inMemorySession)
        {
            _localStorage = localStorage;
            _inMemorySession = inMemorySession;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                string? token = null;

                try
                {
                    token = await _localStorage.GetItemAsync<string>("authToken", cancellationToken);
                }
                catch
                {
                    // Ignorar errores de LocalStorage.
                }

                if (string.IsNullOrWhiteSpace(token))
                {
                    token = _inMemorySession.Token;
                }

                if (!string.IsNullOrWhiteSpace(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al adjuntar el token de autorización: {ex.Message}");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}

