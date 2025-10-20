using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging; // # 1. Importar el servicio de Logging

namespace BlazorApp.Auth
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly InMemoryUserSession _userSession;
        private readonly ILogger<CustomAuthenticationStateProvider> _logger; // # 2. Declarar el Logger

        // # 3. Inyectar ILogger en el constructor
        public CustomAuthenticationStateProvider(InMemoryUserSession userSession, ILogger<CustomAuthenticationStateProvider> logger)
        {
            _userSession = userSession;
            _logger = logger;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = _userSession.Token;

            if (string.IsNullOrWhiteSpace(token))
            {
                // # 4. Añadir Log de diagnóstico
                _logger.LogInformation("GetAuthenticationStateAsync: No token found. Returning anonymous user.");
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            }

            _logger.LogInformation("GetAuthenticationStateAsync: Token found. Returning authenticated user.");
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }

        // ... El resto de los métodos (NotifyUserAuthentication, etc.) no necesitan cambios ...
        public void NotifyUserAuthentication(string token)
        {
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            var user = new ClaimsPrincipal(identity);
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs != null)
            {
                keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);
                if (roles != null)
                {
                    if (roles.ToString()?.Trim().StartsWith("[") == true)
                    {
                        var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());
                        if (parsedRoles != null)
                        {
                            foreach (var parsedRole in parsedRoles)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                            }
                        }
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                    }
                }
                claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString() ?? string.Empty)));
            }
            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return System.Convert.FromBase64String(base64);
        }
    }
}

