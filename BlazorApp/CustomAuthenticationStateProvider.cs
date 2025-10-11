using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;
using DTOs;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _js;
    private static readonly ClaimsPrincipal Anonymous = new(new ClaimsIdentity());

    public CustomAuthenticationStateProvider(IJSRuntime js) => _js = js;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var json = await _js.InvokeAsync<string?>("localStorage.getItem", "userSession");

            if (string.IsNullOrWhiteSpace(json))
                return new AuthenticationState(Anonymous);

            PersonaDTO? user = null;
            try
            {
                user = JsonSerializer.Deserialize<PersonaDTO>(json);
            }
            catch
            {
                await _js.InvokeVoidAsync("localStorage.removeItem", "userSession");
                return new AuthenticationState(Anonymous);
            }

            var principal = CreatePrincipal(user);
            return new AuthenticationState(principal);
        }
        catch
        {
            return new AuthenticationState(Anonymous);
        }
    }

    public async Task MarkUserAsAuthenticated(PersonaDTO user)
    {
        var json = JsonSerializer.Serialize(user);
        await _js.InvokeVoidAsync("localStorage.setItem", "userSession", json);
        var principal = CreatePrincipal(user);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }

    public async Task MarkUserAsLoggedOut()
    {
        await _js.InvokeVoidAsync("localStorage.removeItem", "userSession");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(Anonymous)));
    }

    private static ClaimsPrincipal CreatePrincipal(PersonaDTO? user)
    {
        if (user is null)
            return Anonymous;

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.IdPersona.ToString()),
            new(ClaimTypes.Name, user.NombreCompleto ?? string.Empty),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
            new(ClaimTypes.Role, user.Rol ?? string.Empty)
        };

        var identity = new ClaimsIdentity(claims, authenticationType: "apiauth");
        return new ClaimsPrincipal(identity);
    }
}
