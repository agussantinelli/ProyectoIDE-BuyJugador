using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Auth
{
    public sealed class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private static readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        // Este método es llamado por Blazor al inicio y cuando se notifica un cambio.
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await _localStorage.GetItemAsync<string>("authToken");

                if (string.IsNullOrWhiteSpace(token))
                {
                    return new AuthenticationState(_anonymous);
                }

                // Si hay token, lo parseamos para crear la identidad del usuario
                return new AuthenticationState(CreateClaimsPrincipalFromToken(token));
            }
            catch
            {
                // Si el token es inválido o hay algún error, el usuario es anónimo.
                return new AuthenticationState(_anonymous);
            }
        }

        public async Task MarkUserAsLoggedInAsync(string token)
        {
            await _localStorage.SetItemAsync("authToken", token);
            var authenticatedUser = CreateClaimsPrincipalFromToken(token);
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));

            // Notifica a Blazor que el estado de autenticación cambió.
            NotifyAuthenticationStateChanged(authState);
        }

        // Llamado para cerrar sesión.
        public async Task MarkUserAsLoggedOutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            var authState = Task.FromResult(new AuthenticationState(_anonymous));

            // Notifica que el usuario ya no está autenticado.
            NotifyAuthenticationStateChanged(authState);
        }

        // Método privado para parsear el token y crear el ClaimsPrincipal
        private static ClaimsPrincipal CreateClaimsPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity();

            if (tokenHandler.CanReadToken(token))
            {
                var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
                // Usamos los claims del token para construir la identidad
                identity = new ClaimsIdentity(jwtSecurityToken.Claims, "jwtAuth");
            }

            return new ClaimsPrincipal(identity);
        }
    }
}
