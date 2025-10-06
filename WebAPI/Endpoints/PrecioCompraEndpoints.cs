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
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            });

            g.MapPost("/", async (PrecioCompraDTO dto, PrecioCompraService service) =>
            {
                var creado = await service.CreateAsync(dto);
                return Results.Created($"/api/precios-compra/{creado.IdProducto}/{creado.IdProveedor}", creado);
            });

            g.MapPut("/{idProducto:int}/{idProveedor:int}", async (int idProducto, int idProveedor, PrecioCompraDTO dto, PrecioCompraService service) =>
            {
                var actualizado = await service.UpdateAsync(idProducto, idProveedor, dto);
                return actualizado ? Results.NoContent() : Results.NotFound();
            });

            g.MapDelete("/{idProducto:int}/{idProveedor:int}", async (int idProducto, int idProveedor, PrecioCompraService service) =>
            {
                var eliminado = await service.DeleteAsync(idProducto, idProveedor);
                return eliminado ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
