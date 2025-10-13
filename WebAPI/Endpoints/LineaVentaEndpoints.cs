using DTOs;
using DominioServicios;

namespace WebAPI.Endpoints
{
    public static class LineaVentaEndpoints
    {
        public static void MapLineaVentaEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/lineaventas");

            group.MapGet("/", async (LineaVentaService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            });

            group.MapGet("/{idVenta}/{nroLinea}", async (int idVenta, int nroLinea, LineaVentaService service) =>
            {
                var linea = await service.GetByIdAsync(idVenta, nroLinea);
                return linea is not null ? Results.Ok(linea) : Results.NotFound();
            });

            group.MapPost("/", async (LineaVentaDTO dto, LineaVentaService service) =>
            {
                var nuevo = await service.CreateAsync(dto);
                return Results.Created($"/api/lineaventas/{nuevo.IdVenta}/{nuevo.NroLineaVenta}", nuevo);
            });

            group.MapPut("/{idVenta}/{nroLinea}", async (int idVenta, int nroLinea, LineaVentaDTO dto, LineaVentaService service) =>
            {
                await service.UpdateAsync(idVenta, nroLinea, dto);
                return Results.NoContent();
            });

            group.MapDelete("/{idVenta}/{nroLinea}", async (int idVenta, int nroLinea, LineaVentaService service) =>
            {
                await service.DeleteAsync(idVenta, nroLinea);
                return Results.NoContent();
            });

            group.MapGet("/porventa/{idVenta}", async (int idVenta, LineaVentaService service) =>
            {
                var lineas = await service.GetLineasByVentaIdAsync(idVenta);
                return Results.Ok(lineas);
            });

        }
    }
}
