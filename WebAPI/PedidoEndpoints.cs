using DominioModelo;
using DominioServicios;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebAPI
{
    public static class PedidoEndpoints
    {
        public static void MapPedidoEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/pedidos");

            group.MapGet("/", async (PedidoService service) =>
            {
                // Endpoint para obtener todos los pedidos
                return Results.Ok(await service.GetAllAsync());
            });

            group.MapGet("/{id}", async (int id, PedidoService service) =>
            {
                // Endpoint para obtener un pedido por su ID
                var pedido = await service.GetByIdAsync(id);
                return pedido is not null ? Results.Ok(pedido) : Results.NotFound();
            });

            group.MapPost("/", async (Pedido pedido, PedidoService service) =>
            {
                // Endpoint para crear un nuevo pedido
                var nuevo = await service.CreateAsync(pedido);
                return Results.Created($"/api/pedidos/{nuevo.Id}", nuevo);
            });

            //group.MapPut("/{id}", async (int id, Pedido pedido, PedidoService service) =>
            //{
            //    // Endpoint para actualizar un pedido existente
            //    var updatedPedido = await service.UpdateAsync(id, pedido);
            //    return updatedPedido is not null ? Results.NoContent() : Results.NotFound();
            //});

            //group.MapDelete("/{id}", async (int id, PedidoService service) =>
            //{
            //    // Endpoint para eliminar un pedido
            //    var deleted = await service.DeleteAsync(id);
            //    return deleted ? Results.NoContent() : Results.NotFound();
            //});
        }
    }
}
