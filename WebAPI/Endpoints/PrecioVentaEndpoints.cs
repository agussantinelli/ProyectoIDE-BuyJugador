using DominioServicios;
using DTOs;

namespace WebAPI.Endpoints
{
    public static class PrecioVentaEndpoints
    {
        public static void MapPrecioVentaEndpoints(this WebApplication app)
        {
            var g = app.MapGroup("/api/precios-venta");

            g.MapGet("/", async (PrecioVentaService service) =>
            {
                var precios = await service.GetAllAsync();
                return Results.Ok(precios);
            });

            g.MapGet("/{idProducto:int}/{fechaDesde:datetime}", async (int idProducto, DateTime fechaDesde, PrecioVentaService service) =>
            {
                var dto = await service.GetByIdAsync(idProducto, fechaDesde);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            });

            g.MapPost("/", async (PrecioVentaDTO dto, PrecioVentaService service) =>
            {
                var creado = await service.CreateAsync(dto);
                return Results.Created($"/api/precios-venta/{creado.IdProducto}/{creado.FechaDesde:yyyy-MM-ddTHH:mm:ss}", creado);
            });

            g.MapPut("/{idProducto:int}/{fechaDesde:datetime}", async (int idProducto, DateTime fechaDesde, PrecioVentaDTO dto, PrecioVentaService service) =>
            {
                var actualizado = await service.UpdateAsync(idProducto, fechaDesde, dto);
                return actualizado ? Results.NoContent() : Results.NotFound();
            });

            g.MapDelete("/{idProducto:int}/{fechaDesde:datetime}", async (int idProducto, DateTime fechaDesde, PrecioVentaService service) =>
            {
                var eliminado = await service.DeleteAsync(idProducto, fechaDesde);
                return eliminado ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}

