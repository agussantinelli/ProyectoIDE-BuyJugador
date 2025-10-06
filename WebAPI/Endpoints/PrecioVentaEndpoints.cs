using DominioServicios;
using DTOs;

namespace WebAPI.Endpoints
{
    public static class PrecioVentaEndpoints
    {
        public static void MapPrecioVentaEndpoints(this WebApplication app)
        {
            var g = app.MapGroup("/api/precios-venta");

            g.MapGet("/", async (PrecioVentaService s) => Results.Ok(await s.GetAllAsync()));

            g.MapGet("/{idProducto:int}/{fechaDesde:datetime}", async (int idProducto, DateTime fechaDesde, PrecioVentaService s) =>
            {
                var dto = await s.GetByIdAsync(idProducto, fechaDesde);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            });

            g.MapPost("/", async (PrecioVentaDTO dto, PrecioVentaService s) =>
            {
                var nuevo = await s.CreateAsync(dto);
                return Results.Created($"/api/precios-venta/{nuevo.IdProducto}/{nuevo.FechaDesde:yyyy-MM-ddTHH:mm:ss}", nuevo);
            });

            g.MapPut("/{idProducto:int}/{fechaDesde:datetime}", async (int idProducto, DateTime fechaDesde, PrecioVentaDTO dto, PrecioVentaService s) =>
            {
                await s.UpdateAsync(idProducto, fechaDesde, dto);
                return Results.NoContent();
            });

            g.MapDelete("/{idProducto:int}/{fechaDesde:datetime}", async (int idProducto, DateTime fechaDesde, PrecioVentaService s) =>
            {
                await s.DeleteAsync(idProducto, fechaDesde);
                return Results.NoContent();
            });
        }
    }
}
