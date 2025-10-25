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
                var todas = await service.GetAllAsync();
                return Results.Ok(todas);
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

            group.MapGet("/ordenadas", async (LocalidadService service) =>
            {
                var localidadesOrdenadas = await service.GetAllOrderedAsync();
                return Results.Ok(localidadesOrdenadas);
            });
        }
    }
}
