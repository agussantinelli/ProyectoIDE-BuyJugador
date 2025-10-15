using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace WinForms
{
    public class TokenMessageHandler : DelegatingHandler
    {
        private readonly UserSessionService _userSessionService;

        public TokenMessageHandler(UserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _userSessionService.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            InnerHandler ??= new HttpClientHandler();

            return await base.SendAsync(request, cancellationToken);
        }
    }
}

