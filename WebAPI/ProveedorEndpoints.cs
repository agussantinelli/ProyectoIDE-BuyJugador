
using DominioServicios;
using DominioModelo;

namespace WebAPI
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

            group.MapGet("/{id}", async (int id, ProveedorService service) =>
            {
                var proveedor = await service.GetByIdAsync(id);
                return proveedor is not null ? Results.Ok(proveedor) : Results.NotFound();
            });

            group.MapPost("/", async (Proveedor proveedor, ProveedorService service) =>
            {
                var nuevo = await service.CreateAsync(proveedor);
                return Results.Created($"/api/proveedores/{nuevo.Id}", nuevo);
            });

            group.MapPut("/{id}", async (int id, Proveedor proveedor, ProveedorService service) =>
            {
                await service.UpdateAsync(id, proveedor);
                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (int id, ProveedorService service) =>
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            });
        }
    }
}
