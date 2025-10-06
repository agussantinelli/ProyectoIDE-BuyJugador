using DominioServicios;
using DTOs;

namespace WebAPI.Endpoints
{
    public static class PrecioCompraEndpoints
    {
        public static void MapPrecioCompraEndpoints(this WebApplication app)
        {
            var g = app.MapGroup("/api/precios-compra");

            g.MapGet("/", async (PrecioCompraService service) =>
            {
                var precios = await service.GetAllAsync();
                return Results.Ok(precios);
            });

            g.MapGet("/{idProducto:int}/{idProveedor:int}", async (int idProducto, int idProveedor, PrecioCompraService service) =>
            {
                var dto = await service.GetByIdAsync(idProducto, idProveedor);
                if (dto is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(dto);
            });

            g.MapPost("/", async (PrecioCompraDTO dto, PrecioCompraService service) =>
            {
                var creado = await service.CreateAsync(dto);
                return Results.Created($"/api/precios-compra/{creado.IdProducto}/{creado.IdProveedor}", creado);
            });

            g.MapPut("/{idProducto:int}/{idProveedor:int}", async (int idProducto, int idProveedor, PrecioCompraDTO dto, PrecioCompraService service) =>
            {
                await service.UpdateAsync(idProducto, idProveedor, dto);
                return Results.NoContent();
            });

            g.MapDelete("/{idProducto:int}/{idProveedor:int}", async (int idProducto, int idProveedor, PrecioCompraService service) =>
            {
                await service.DeleteAsync(idProducto, idProveedor);
                return Results.NoContent();
            });
        }
    }
}
