using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApp.Auth
{
    public class UserSessionService
    {
        private readonly AuthenticationStateProvider _authProvider;
        private readonly InMemoryUserSession _userSession;

        public UserSessionService(AuthenticationStateProvider authProvider, InMemoryUserSession userSession)
        {
            _authProvider = authProvider;
            _userSession = userSession;
        }

        public async Task<ClaimsPrincipal> GetUserAsync()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            return authState.User;
        }

        public async Task<bool> EsAdminAsync()
        {
            var user = await GetUserAsync();
            return user.IsInRole("Admin");
        }

        public async Task<string?> GetNombreUsuarioAsync()
        {
            var user = await GetUserAsync();
            return user.Identity?.Name;
        }

        // # Se actualiza para leer desde el servicio en memoria. Ya no es asíncrono.
        public string? GetToken()
        {
            return _userSession.Token;
        }

        public async Task<bool> EstaLogueadoAsync()
        {
            var user = await GetUserAsync();
            return user.Identity?.IsAuthenticated == true;
        }

        public async Task<int?> GetUserIdAsync()
        {
            var user = await GetUserAsync();
            if (user.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;
                }
            }
            return null;
        }
    }
}

