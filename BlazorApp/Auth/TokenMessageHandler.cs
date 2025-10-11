using Blazored.LocalStorage; // 1. Usar ILocalStorageService
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorApp.Auth
{
    public class TokenMessageHandler : DelegatingHandler
    {
        // 2. Inyectar ILocalStorageService en lugar del AuthProvider
        private readonly ILocalStorageService _localStorage;

        public TokenMessageHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // 3. Obtener el token directamente desde el Local Storage
            var token = await _localStorage.GetItemAsync<string>("authToken", cancellationToken);

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
