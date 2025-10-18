using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApp.Auth
{
    public class UserSessionService
    {
        private readonly AuthenticationStateProvider _authProvider;
        private readonly ILocalStorageService _localStorage;

        public UserSessionService(AuthenticationStateProvider authProvider, ILocalStorageService localStorage)
        {
            _authProvider = authProvider;
            _localStorage = localStorage;
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

        public async Task<string?> GetTokenAsync()
        {
            return await _localStorage.GetItemAsync<string>("authToken");
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
                // Buscamos el claim "NameIdentifier", que por convención contiene el ID del usuario.
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
