using DTOs;
using DominioServicios;

namespace WebAPI.Endpoints
{
    public static class ProvinciaEndpoints
    {
        public static void MapProvinciaEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/provincias");

            group.MapGet("/", async (ProvinciaService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            });

            group.MapGet("/{id}", async (int id, ProvinciaService service) =>
            {
                var prov = await service.GetByIdAsync(id);
                return prov is not null ? Results.Ok(prov) : Results.NotFound();
            });

            group.MapPost("/", async (ProvinciaDTO dto, ProvinciaService service) =>
            {
                var nuevo = await service.CreateAsync(dto);
                return Results.Created($"/api/provincias/{nuevo.IdProvincia}", nuevo);
            });

            group.MapPut("/{id}", async (int id, ProvinciaDTO dto, ProvinciaService service) =>
            {
                await service.UpdateAsync(id, dto);
                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (int id, ProvinciaService service) =>
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            });
        }
    }
}
