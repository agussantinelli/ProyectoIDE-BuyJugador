using System.Net.Http.Headers;
using System.Net.Http;

namespace BlazorApp.Auth
{
    public class TokenMessageHandler : DelegatingHandler
    {
        private readonly CustomAuthenticationStateProvider _auth;

        public TokenMessageHandler(CustomAuthenticationStateProvider auth)
        {
            _auth = auth;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _auth.GetTokenAsync();
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
