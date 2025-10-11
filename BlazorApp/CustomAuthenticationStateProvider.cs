using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using DTOs; 

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _jsRuntime;
    private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public CustomAuthenticationStateProvider(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var userSessionJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "userSession");

        if (string.IsNullOrWhiteSpace(userSessionJson))
        {
            return new AuthenticationState(_anonymous);
        }

        var userSession = JsonSerializer.Deserialize<PersonaDTO>(userSessionJson);
        var claimsPrincipal = CreateClaimsPrincipalFromUser(userSession);
        return new AuthenticationState(claimsPrincipal);
    }

    public async Task MarkUserAsAuthenticated(PersonaDTO user)
    {
        var userSessionJson = JsonSerializer.Serialize(user);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "userSession", userSessionJson);

        var claimsPrincipal = CreateClaimsPrincipalFromUser(user);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    public async Task MarkUserAsLoggedOut()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "userSession");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }

    private ClaimsPrincipal CreateClaimsPrincipalFromUser(PersonaDTO user)
    {
        if (user == null)
            return _anonymous;

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.IdPersona.ToString()),
            new Claim(ClaimTypes.Name, user.NombreCompleto),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Rol) 
        };

        var identity = new ClaimsIdentity(claims, "apiauth");
        return new ClaimsPrincipal(identity);
    }
}
