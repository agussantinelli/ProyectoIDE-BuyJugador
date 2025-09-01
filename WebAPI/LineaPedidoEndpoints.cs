using DominioModelo;
using DominioServicios;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebAPI
{
    public static class LineaPedidoEndpoints
    {
        public static void MapLineaPedidoEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/lineapedidos");

            group.MapGet("/", async (LineaPedidoService service) =>
            {
                // Endpoint para obtener todas las líneas de pedido
                return Results.Ok(await service.GetAllAsync());
            });

            group.MapGet("/{id}", async (int id, LineaPedidoService service) =>
            {
                // Endpoint para obtener una línea de pedido por su ID
                var lineaPedido = await service.GetByIdAsync(id);
                return lineaPedido is not null ? Results.Ok(lineaPedido) : Results.NotFound();
            });

            group.MapPost("/", async (LineaPedido lineaPedido, LineaPedidoService service) =>
            {
                // Endpoint para crear una nueva línea de pedido
                var nuevo = await service.CreateAsync(lineaPedido);
                return Results.Created($"/api/lineapedidos/{nuevo.Id}", nuevo);
            });

            //group.MapPut("/{id}", async (int id, LineaPedido lineaPedido, LineaPedidoService service) =>
            //{
            //    // Endpoint para actualizar una línea de pedido existente
            //    var updatedLineaPedido = await service.UpdateAsync(id, lineaPedido);
            //    return updatedLineaPedido is not null ? Results.NoContent() : Results.NotFound();
            //});

            //group.MapDelete("/{id}", async (int id, LineaPedidoService service) =>
            //{
            //    // Endpoint para eliminar una línea de pedido
            //    var deleted = await service.DeleteAsync(id);
            //    return deleted ? Results.NoContent() : Results.NotFound();
            //});
        }
    }
}
