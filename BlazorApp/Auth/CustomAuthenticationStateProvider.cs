using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace BlazorApp.Auth
{
    public sealed class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _js;
        private static readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

        private const string TokenKey = "authToken";
        private const string NameKey = "authName";

        public CustomAuthenticationStateProvider(IJSRuntime js)
        {
            _js = js;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await GetItem(TokenKey);
                var name = await GetItem(NameKey);

                if (!string.IsNullOrWhiteSpace(token))
                {
                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, string.IsNullOrWhiteSpace(name) ? "Usuario" : name),
                        new Claim("access_token", token)
                    }, authenticationType: "jwt");

                    return new AuthenticationState(new ClaimsPrincipal(identity));
                }
            }
            catch { /* ignorar */ }

            return new AuthenticationState(_anonymous);
        }

        // ---- LOGIN (el que ya usábamos) ----
        public async Task MarkUserAsLoggedInAsync(string token, string? displayName = null)
        {
            await SetItem(TokenKey, token);
            await SetItem(NameKey, displayName ?? "Usuario");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        // ---- LOGOUT ----
        public async Task MarkUserAsLoggedOut()
        {
            await RemoveItem(TokenKey);
            await RemoveItem(NameKey);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }

        // ==== WRAPPERS para compatibilidad con tu código existente ====
        // Login usado por Login.razor
        public Task MarkUserAsAuthenticatedAsync(string token, string? displayName = null)
            => MarkUserAsLoggedInAsync(token, displayName);

        // Token usado por TokenMessageHandler.cs (y donde lo necesites)
        public async Task<string?> GetTokenAsync()
            => await GetItem(TokenKey);

        private async Task<string?> GetItem(string key)
        {
            try { return await _js.InvokeAsync<string?>("localStorageHelper.getItem", key); }
            catch { return await _js.InvokeAsync<string?>("localStorage.getItem", key); }
        }

        private async Task SetItem(string key, string value)
        {
            try { await _js.InvokeVoidAsync("localStorageHelper.setItem", key, value); }
            catch { await _js.InvokeVoidAsync("localStorage.setItem", key, value); }
        }

        private async Task RemoveItem(string key)
        {
            try { await _js.InvokeVoidAsync("localStorageHelper.removeItem", key); }
            catch { await _js.InvokeVoidAsync("localStorage.removeItem", key); }
        }
    }
}
