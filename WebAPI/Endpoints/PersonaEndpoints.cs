using DominioServicios;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Endpoints
{
    public static class PersonaEndpoints
    {
        public static void MapPersonaEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/personas");

            group.MapGet("/", async (PersonaService service) =>
            {
                var personas = await service.GetAllAsync();
                return Results.Ok(personas);
            });
            group.MapGet("/activos-reporte", async (PersonaService service) =>
            {
                var personas = await service.GetPersonasActivasParaReporteAsync();
                return Results.Ok(personas);
            });

            group.MapGet("/inactivos", async (PersonaService service) =>
            {
                var personas = await service.GetInactivosAsync();
                return Results.Ok(personas);
            });

            group.MapGet("/{id}", async (PersonaService service, int id) =>
            {
                var persona = await service.GetByIdAsync(id);
                return persona != null ? Results.Ok(persona) : Results.NotFound();
            });

            group.MapPost("/", async (PersonaService service, PersonaDTO personaDto) =>
            {
                var dtoCreado = await service.CreateAsync(personaDto);
                return Results.Created($"/api/personas/{dtoCreado.IdPersona}", dtoCreado);
            });

            group.MapPut("/{id}", async (PersonaService service, int id, PersonaDTO personaDto) =>
            {
                var actualizado = await service.UpdateAsync(id, personaDto);
                return actualizado ? Results.NoContent() : Results.NotFound();
            });

            group.MapPut("/reactivar/{id}", async (PersonaService service, int id) =>
            {
                var reactivado = await service.ReactivarAsync(id);
                return reactivado ? Results.NoContent() : Results.NotFound();
            });

            group.MapDelete("/{id}", async (PersonaService service, int id) =>
            {
                var borrado = await service.DeleteAsync(id);
                return borrado ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}

