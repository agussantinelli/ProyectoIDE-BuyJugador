// WebAPI/Endpoints/PersonaEndpoints.cs
using DominioServicios; 
using DTOs;

namespace WebAPI.Endpoints
{
    public static class PersonaEndpoints
    {
        public static void MapPersonaEndpoints(this WebApplication app)
        {
            app.MapGet("/api/personas", async (PersonaService service) =>
            {
                var personas = await service.GetAllAsync();
                return Results.Ok(personas);
            });

            app.MapGet("/api/personas/inactivos", async (PersonaService service) =>
            {
                var personas = await service.GetInactivosAsync();
                return Results.Ok(personas);
            });

            app.MapGet("/api/personas/{id}", async (PersonaService service, int id) =>
            {
                var persona = await service.GetByIdAsync(id);
                return persona != null ? Results.Ok(persona) : Results.NotFound();
            });

            app.MapPost("/api/personas", async (PersonaService service, PersonaDTO personaDto) =>
            {
                var dtoCreado = await service.CreateAsync(personaDto);
                return Results.Created($"/api/personas/{dtoCreado.IdPersona}", dtoCreado);
            });

            app.MapPut("/api/personas/{id}", async (PersonaService service, int id, PersonaDTO personaDto) =>
            {
                var actualizado = await service.UpdateAsync(id, personaDto);
                return actualizado ? Results.NoContent() : Results.NotFound();
            });

            app.MapPut("/api/personas/reactivar/{id}", async (PersonaService service, int id) =>
            {
                var reactivado = await service.ReactivarAsync(id);
                return reactivado ? Results.NoContent() : Results.NotFound();
            });

            app.MapDelete("/api/personas/{id}", async (PersonaService service, int id) =>
            {
                var borrado = await service.DeleteAsync(id);
                return borrado ? Results.NoContent() : Results.NotFound();
            });

            app.MapPost("/api/personas/login", async (PersonaService service, LoginRequestDto loginRequest) =>
            {
                var personaDto = await service.LoginAsync(loginRequest.Dni, loginRequest.Password);
                return personaDto != null ? Results.Ok(personaDto) : Results.Unauthorized();
            }).AllowAnonymous();
        }

        public class LoginRequestDto
        {
            public int Dni { get; set; }
            public string Password { get; set; } = null!;
        }
    }
}
