using DTOs;
using DominioServicios;

namespace WebAPI.Endpoints
{
    public static class ProveedorEndpoints
    {
        public static void MapProveedorEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/proveedores");

            group.MapGet("/", async (ProveedorService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            });

            group.MapGet("/inactivos", async (ProveedorService service) =>
            {
                return Results.Ok(await service.GetInactivosAsync());
            });

            group.MapGet("/{id}", async (int id, ProveedorService service) =>
            {
                var prov = await service.GetByIdAsync(id);
                return prov is not null ? Results.Ok(prov) : Results.NotFound();
            });

            group.MapPost("/", async (ProveedorDTO dto, ProveedorService service) =>
            {
                var nuevo = await service.CreateAsync(dto);
                return Results.Created($"/api/proveedores/{nuevo.IdProveedor}", nuevo);
            });

            group.MapPut("/{id}", async (int id, ProveedorDTO dto, ProveedorService service) =>
            {
                await service.UpdateAsync(id, dto);
                return Results.NoContent();
            });

            group.MapPost("/{id}/reactivar", async (int id, ProveedorService service) =>
            {
                await service.ReactivarAsync(id);
                return Results.NoContent();
            });

            group.MapGet("/inactivos", async (ProveedorService service) =>
            {
                return Results.Ok(await service.GetInactivosAsync());
            });

            group.MapPost("/{id}/reactivar", async (int id, ProveedorService service) =>
            {
                await service.ReactivarAsync(id);
                return Results.NoContent();
            });

        }
    }
}
