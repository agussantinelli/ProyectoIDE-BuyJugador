using DTOs;
using DominioServicios;

namespace WebAPI.Endpoints
{
    public static class LineaPedidoEndpoints
    {
        public static void MapLineaPedidoEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/lineapedidos");

            group.MapGet("/", async (LineaPedidoService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            });

            group.MapGet("/{idPedido}/{nroLinea}", async (int idPedido, int nroLinea, LineaPedidoService service) =>
            {
                var linea = await service.GetByIdAsync(idPedido, nroLinea);
                return linea is not null ? Results.Ok(linea) : Results.NotFound();
            });

            group.MapPost("/", async (LineaPedidoDTO dto, LineaPedidoService service) =>
            {
                var nuevo = await service.CreateAsync(dto);
                return Results.Created($"/api/lineapedidos/{nuevo.IdPedido}/{nuevo.NroLineaPedido}", nuevo);
            });

            group.MapPut("/{idPedido}/{nroLinea}", async (int idPedido, int nroLinea, LineaPedidoDTO dto, LineaPedidoService service) =>
            {
                await service.UpdateAsync(idPedido, nroLinea, dto);
                return Results.NoContent();
            });

            group.MapDelete("/{idPedido}/{nroLinea}", async (int idPedido, int nroLinea, LineaPedidoService service) =>
            {
                await service.DeleteAsync(idPedido, nroLinea);
                return Results.NoContent();
            });
        }
    }
}
