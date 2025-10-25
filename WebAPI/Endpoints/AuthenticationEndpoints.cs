using DominioServicios;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Endpoints
{
    public static class AuthenticationEndpoints
    {
        public static void MapAuthenticationEndpoints(this WebApplication app)
        {
            app.MapPost("/api/Authentication/login", [AllowAnonymous] async (
                LoginRequestDTO loginRequest,
                PersonaService personaService,
                IConfiguration config) =>
            {
                var personaDto = await personaService.LoginAsync(loginRequest.Dni, loginRequest.Password);
                if (personaDto == null) return Results.Unauthorized();

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, personaDto.IdPersona.ToString()),
                    new Claim(ClaimTypes.Name, personaDto.NombreCompleto ?? string.Empty),
                    new Claim(ClaimTypes.Role, personaDto.Rol)
                };

                var token = new JwtSecurityToken(
                    issuer: config["Jwt:Issuer"],
                    audience: config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: credentials);

                var response = new LoginResponseDTO
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    UserInfo = personaDto
                };

                return Results.Ok(response);
            });
        }
    }
}
