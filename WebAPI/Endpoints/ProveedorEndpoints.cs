using DTOs;
using DominioServicios;

namespace WebAPI.Endpoints
{
    public static class ProveedorEndpoints
    {
        public static void MapProveedorEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/proveedores");

            // Obtiene todos los proveedores activos.
            group.MapGet("/", async (ProveedorService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            });

            // Obtiene los proveedores que han sido dados de baja.
            group.MapGet("/inactivos", async (ProveedorService service) =>
            {
                return Results.Ok(await service.GetInactivosAsync());
            });

            // Obtiene un proveedor por su ID.
            group.MapGet("/{id}", async (int id, ProveedorService service) =>
            {
                var prov = await service.GetByIdAsync(id);
                return prov is not null ? Results.Ok(prov) : Results.NotFound();
            });

            // Crea un nuevo proveedor.
            group.MapPost("/", async (ProveedorDTO dto, ProveedorService service) =>
            {
                var nuevo = await service.CreateAsync(dto);
                return Results.Created($"/api/proveedores/{nuevo.IdProveedor}", nuevo);
            });

            // Actualiza los datos de un proveedor existente.
            group.MapPut("/{id}", async (int id, ProveedorDTO dto, ProveedorService service) =>
            {
                await service.UpdateAsync(id, dto);
                return Results.NoContent();
            });

            // Realiza la baja lógica de un proveedor.
            group.MapDelete("/{id}", async (int id, ProveedorService service) =>
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            });

            // Reactiva un proveedor que fue dado de baja.
            group.MapPost("/{id}/reactivar", async (int id, ProveedorService service) =>
            {
                await service.ReactivarAsync(id);
                return Results.NoContent();
            });
        }
    }
}
