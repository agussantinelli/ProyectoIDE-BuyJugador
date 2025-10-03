using DTOs;
using DominioServicios;

namespace WebAPI.Endpoints
{
    public static class LineaPedidoEndpoints
    {
        public static void MapLineaPedidoEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/lineapedidos");

            // Endpoint para obtener todas las líneas de un pedido específico
            group.MapGet("/porpedido/{idPedido}", async (int idPedido, LineaPedidoService service) =>
            {
                var lineas = await service.GetLineasByPedidoIdAsync(idPedido);
                return Results.Ok(lineas);
            });
        }
    }
}
