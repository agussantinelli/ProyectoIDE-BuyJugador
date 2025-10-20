using DominioServicios;
using DTOs;
using Microsoft.AspNetCore.Mvc;

public static class PedidoEndpoints
{
    public static void MapPedidoEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/pedidos");

        group.MapGet("/", async (PedidoService pedidoService) =>
        {
            var pedidos = await pedidoService.GetAllPedidosDetalladosAsync();
            return Results.Ok(pedidos);
        });

        group.MapGet("/{id}", async (int id, PedidoService pedidoService) =>
        {
            var pedido = await pedidoService.GetPedidoDetalladoByIdAsync(id);
            return pedido is not null ? Results.Ok(pedido) : Results.NotFound();
        });

        group.MapPost("/completo", async (PedidoService pedidoService, [FromBody] CrearPedidoCompletoDTO pedidoDto) =>
        {
            try
            {
                var nuevoPedido = await pedidoService.CrearPedidoCompletoAsync(pedidoDto);
                if (nuevoPedido == null)
                    return Results.BadRequest(new { message = "No se pudo crear el pedido. Verifique los datos." });

                return Results.Created($"/api/pedidos/{nuevoPedido.IdPedido}", nuevoPedido);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = $"Error al crear el pedido: {ex.Message}" });
            }
        });

        group.MapDelete("/{id}", async (int id, PedidoService service) =>
        {
            try
            {
                await service.DeletePedidoCompletoAsync(id);
                return Results.NoContent();
            }
            catch (KeyNotFoundException)
            {
                return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });

        group.MapPut("/recibir/{id}", async (int id, PedidoService service) =>
        {
            try
            {
                await service.MarcarComoRecibidoAsync(id);
                return Results.Ok(new { message = "Pedido marcado como recibido." });
            }
            catch (KeyNotFoundException)
            {
                return Results.NotFound(new { message = "Pedido no encontrado." });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });

        group.MapPut("/{id}", async (int id, PedidoDTO pedidoDto, PedidoService service) =>
        {
            if (id != pedidoDto.IdPedido)
                return Results.BadRequest("El ID de la URL no coincide con el del cuerpo de la solicitud.");

            try
            {
                await service.UpdatePedidoCompletoAsync(id, pedidoDto);
                return Results.NoContent();
            }
            catch (KeyNotFoundException)
            {
                return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });
    }
}
