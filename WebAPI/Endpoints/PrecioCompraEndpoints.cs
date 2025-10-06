using DominioServicios;
using DTOs;

namespace WebAPI.Endpoints
{
    public static class PrecioCompraEndpoints
    {
        public static void MapPrecioCompraEndpoints(this WebApplication app)
        {
            var g = app.MapGroup("/api/precios-compra");

            g.MapGet("/", async (PrecioCompraService s) => Results.Ok(await s.GetAllAsync()));

            g.MapGet("/{idProducto:int}/{idProveedor:int}", async (int idProducto, int idProveedor, PrecioCompraService s) =>
            {
                var dto = await s.GetByIdAsync(idProducto, idProveedor);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            });

            g.MapPost("/", async (PrecioCompraDTO dto, PrecioCompraService s) =>
            {
                var nuevo = await s.UpsertAsync(dto);
                return Results.Created($"/api/precios-compra/{nuevo.IdProducto}/{nuevo.IdProveedor}", nuevo);
            });

            g.MapPut("/{idProducto:int}/{idProveedor:int}", async (int idProducto, int idProveedor, PrecioCompraDTO dto, PrecioCompraService s) =>
            {
                if (idProducto != dto.IdProducto || idProveedor != dto.IdProveedor)
                    return Results.BadRequest();
                await s.UpsertAsync(dto);
                return Results.NoContent();
            });

            g.MapDelete("/{idProducto:int}/{idProveedor:int}", async (int idProducto, int idProveedor, PrecioCompraService s) =>
            {
                await s.DeleteAsync(idProducto, idProveedor);
                return Results.NoContent();
            });
        }
    }
}
