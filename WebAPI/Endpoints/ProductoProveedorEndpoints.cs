using DominioServicios;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Endpoints
{
    public static class ProductoProveedorEndpoints
    {
        public static void MapProductoProveedorEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/producto-proveedor");

            group.MapGet("/{idProveedor}", async (int idProveedor, ProductoProveedorService service) =>
            {
                return Results.Ok(await service.GetProductosByProveedorIdAsync(idProveedor));
            });

            group.MapPost("/", async (ProductoProveedorDTO dto, ProductoProveedorService service) =>
            {
                await service.CreateAsync(dto);
                return Results.Ok();
            });

            group.MapDelete("/{idProducto}/{idProveedor}", async (int idProducto, int idProveedor, ProductoProveedorService service) =>
            {
                var result = await service.DeleteAsync(idProducto, idProveedor);
                return result ? Results.Ok() : Results.NotFound();
            });
        }
    }
}

