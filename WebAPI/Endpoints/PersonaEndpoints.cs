using DTOs;
using DominioServicios;

namespace WebAPI.Endpoints
{
    public static class PersonaEndpoints
    {
        public static void MapPersonaEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/personas");

            group.MapGet("/", async (PersonaService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            });

            group.MapGet("/{id}", async (int id, PersonaService service) =>
            {
                var persona = await service.GetByIdAsync(id);
                return persona is not null ? Results.Ok(persona) : Results.NotFound();
            });

            group.MapPost("/", async (PersonaDTO dto, PersonaService service) =>
            {
                var nueva = await service.CreateAsync(dto);
                return Results.Created($"/api/personas/{nueva.IdPersona}", nueva);
            });

            group.MapPut("/{id}", async (int id, PersonaDTO dto, PersonaService service) =>
            {
                await service.UpdateAsync(id, dto);
                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (int id, PersonaService service) =>
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            });
        }
    }
}
