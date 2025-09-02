using DTOs;
using DominioServicios;

namespace WebAPI.Endpoints
{
    public static class LocalidadEndpoints
    {
        public static void MapLocalidadEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/localidades");

            group.MapGet("/", async (LocalidadService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            });

            group.MapGet("/{id}", async (int id, LocalidadService service) =>
            {
                var loc = await service.GetByIdAsync(id);
                return loc is not null ? Results.Ok(loc) : Results.NotFound();
            });

            group.MapPost("/", async (LocalidadDTO dto, LocalidadService service) =>
            {
                var nuevo = await service.CreateAsync(dto);
                return Results.Created($"/api/localidades/{nuevo.IdLocalidad}", nuevo);
            });

            group.MapPut("/{id}", async (int id, LocalidadDTO dto, LocalidadService service) =>
            {
                await service.UpdateAsync(id, dto);
                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (int id, LocalidadService service) =>
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            });
        }
    }
}
