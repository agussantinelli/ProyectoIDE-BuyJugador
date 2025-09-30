using DTOs;
using DominioServicios;

namespace WebAPI.Endpoints
{
    public static class ProveedorEndpoints
    {
        public static void MapProveedorEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/proveedores");

            // GET /api/proveedores
            // Obtiene todos los proveedores activos.
            group.MapGet("/", async (ProveedorService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            });

            // GET /api/proveedores/inactivos
            // Obtiene los proveedores que han sido dados de baja.
            group.MapGet("/inactivos", async (ProveedorService service) =>
            {
                return Results.Ok(await service.GetInactivosAsync());
            });

            // GET /api/proveedores/{id}
            // Obtiene un proveedor por su ID.
            group.MapGet("/{id}", async (int id, ProveedorService service) =>
            {
                var prov = await service.GetByIdAsync(id);
                return prov is not null ? Results.Ok(prov) : Results.NotFound();
            });

            // POST /api/proveedores
            // Crea un nuevo proveedor.
            group.MapPost("/", async (ProveedorDTO dto, ProveedorService service) =>
            {
                var nuevo = await service.CreateAsync(dto);
                return Results.Created($"/api/proveedores/{nuevo.IdProveedor}", nuevo);
            });

            // PUT /api/proveedores/{id}
            // Actualiza los datos de un proveedor existente.
            group.MapPut("/{id}", async (int id, ProveedorDTO dto, ProveedorService service) =>
            {
                await service.UpdateAsync(id, dto);
                return Results.NoContent();
            });

            // DELETE /api/proveedores/{id}
            // Realiza la baja lógica de un proveedor.
            group.MapDelete("/{id}", async (int id, ProveedorService service) =>
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            });

            // POST /api/proveedores/{id}/reactivar
            // Reactiva un proveedor que fue dado de baja.
            group.MapPost("/{id}/reactivar", async (int id, ProveedorService service) =>
            {
                await service.ReactivarAsync(id);
                return Results.NoContent();
            });
        }
    }
}
