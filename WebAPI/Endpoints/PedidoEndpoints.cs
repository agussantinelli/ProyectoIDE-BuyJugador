using DTOs;
using DominioServicios;

namespace WebAPI.Endpoints
{
    public static class PedidoEndpoints
    {
        public static void MapPedidoEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/pedidos");

            group.MapGet("/", async (PedidoService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            });

            group.MapGet("/{id}", async (int id, PedidoService service) =>
            {
                var pedido = await service.GetByIdAsync(id);
                return pedido is not null ? Results.Ok(pedido) : Results.NotFound();
            });

            group.MapPost("/", async (PedidoDTO dto, PedidoService service) =>
            {
                var nuevo = await service.CreateAsync(dto);
                return Results.Created($"/api/pedidos/{nuevo.IdPedido}", nuevo);
            });

            group.MapPut("/{id}", async (int id, PedidoDTO dto, PedidoService service) =>
            {
                await service.UpdateAsync(id, dto);
                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (int id, PedidoService service) =>
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            });
        }
    }
}
