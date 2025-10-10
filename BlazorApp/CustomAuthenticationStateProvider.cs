using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using DTOs; // Necesitamos acceso a PersonaDTO

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
        // En lugar de un token, ahora buscamos los datos del usuario en localStorage
        var userSessionJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "userSession");

        if (string.IsNullOrWhiteSpace(userSessionJson))
        {
            return new AuthenticationState(_anonymous);
        }

        // Deserializamos los datos del usuario
        var userSession = JsonSerializer.Deserialize<PersonaDTO>(userSessionJson);
        var claimsPrincipal = CreateClaimsPrincipalFromUser(userSession);
        return new AuthenticationState(claimsPrincipal);
    }

    // Este método es llamado desde la página de login con el PersonaDTO
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

    // Método de ayuda para crear la identidad del usuario a partir del DTO
    private ClaimsPrincipal CreateClaimsPrincipalFromUser(PersonaDTO user)
    {
        if (user == null)
            return _anonymous;

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.IdPersona.ToString()),
            // ***** CORRECCIÓN APLICADA AQUÍ *****
            new Claim(ClaimTypes.Name, user.NombreCompleto),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Rol) // Usamos el rol que viene en el DTO
        };

        var identity = new ClaimsIdentity(claims, "apiauth");
        return new ClaimsPrincipal(identity);
    }
}
