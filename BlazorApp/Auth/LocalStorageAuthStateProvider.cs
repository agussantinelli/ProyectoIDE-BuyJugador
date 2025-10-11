using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace BlazorApp.Auth
{
    public class LocalStorageAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _js;

        public LocalStorageAuthStateProvider(IJSRuntime js)
        {
            _js = js;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await _js.InvokeAsync<string>("localStorageHelper.getItem", "authToken");

                if (string.IsNullOrWhiteSpace(token))
                    return Anonymous();

                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "Usuario")
                }, authenticationType: "jwt");

                var user = new ClaimsPrincipal(identity);
                return new AuthenticationState(user);
            }
            catch
            {
                return Anonymous();
            }
        }

        public void NotifyUserAuthentication(ClaimsPrincipal user) =>
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));

        private static AuthenticationState Anonymous() =>
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }
}
