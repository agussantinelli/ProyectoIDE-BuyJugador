using DTOs;
using DominioServicios;

namespace WebAPI.Endpoints
{
    public static class PrecioEndpoints
    {
        public static void MapPrecioEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/precios");

            group.MapGet("/", async (PrecioService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            });

            group.MapGet("/{idProducto}/{fechaDesde}", async (int idProducto, DateTime fechaDesde, PrecioService service) =>
            {
                var precio = await service.GetByIdAsync(idProducto, fechaDesde);
                return precio is not null ? Results.Ok(precio) : Results.NotFound();
            });

            group.MapPost("/", async (PrecioDTO dto, PrecioService service) =>
            {
                var nuevo = await service.CreateAsync(dto);
                return Results.Created($"/api/precios/{nuevo.IdProducto}/{nuevo.FechaDesde:yyyy-MM-ddTHH:mm:ss}", nuevo);
            });

            group.MapPut("/{idProducto}/{fechaDesde}", async (int idProducto, DateTime fechaDesde, PrecioDTO dto, PrecioService service) =>
            {
                await service.UpdateAsync(idProducto, fechaDesde, dto);
                return Results.NoContent();
            });

            group.MapDelete("/{idProducto}/{fechaDesde}", async (int idProducto, DateTime fechaDesde, PrecioService service) =>
            {
                await service.DeleteAsync(idProducto, fechaDesde);
                return Results.NoContent();
            });
        }
    }
}
