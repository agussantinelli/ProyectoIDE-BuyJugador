using DTOs;
using DominioServicios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace WebAPI.Endpoints
{
    public static class ProveedorEndpoints
    {
        public static void MapProveedorEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/proveedores");

            group.MapGet("/", async (ProveedorService service) =>
            {
                var proveedores = await service.GetAllAsync();
                return Results.Ok(proveedores);
            });

            group.MapGet("/inactivos", async (ProveedorService service) =>
            {
                var proveedores = await service.GetInactivosAsync();
                return Results.Ok(proveedores);
            });

            // # NUEVO: Endpoint para obtener proveedores por ID de producto.
            group.MapGet("/producto/{idProducto}", async (int idProducto, ProveedorService service) =>
            {
                var proveedores = await service.GetByProductoIdAsync(idProducto);
                return Results.Ok(proveedores);
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
                if (id != dto.IdProveedor) return Results.BadRequest("El ID no coincide.");

                var success = await service.UpdateAsync(id, dto);
                return success ? Results.NoContent() : Results.NotFound();
            });

            group.MapDelete("/{id}", async (int id, ProveedorService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.NoContent() : Results.NotFound();
            });

            group.MapPut("/{id}/reactivar", async (int id, ProveedorService service) =>
            {
                var success = await service.ReactivarAsync(id);
                return success ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
