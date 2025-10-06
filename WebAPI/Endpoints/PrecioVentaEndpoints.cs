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
                if (dto is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(dto);
            });

            g.MapPost("/", async (PrecioVentaDTO dto, PrecioVentaService service) =>
            {
                var creado = await service.CreateAsync(dto);
                return Results.Created($"/api/precios-venta/{creado.IdProducto}/{creado.FechaDesde:yyyy-MM-ddTHH:mm:ss}", creado);
            });

            g.MapPut("/{idProducto:int}/{fechaDesde:datetime}", async (int idProducto, DateTime fechaDesde, PrecioVentaDTO dto, PrecioVentaService service) =>
            {
                await service.UpdateAsync(idProducto, fechaDesde, dto);
                return Results.NoContent();
            });

            g.MapDelete("/{idProducto:int}/{fechaDesde:datetime}", async (int idProducto, DateTime fechaDesde, PrecioVentaService service) =>
            {
                await service.DeleteAsync(idProducto, fechaDesde);
                return Results.NoContent();
            });
        }
    }
}
