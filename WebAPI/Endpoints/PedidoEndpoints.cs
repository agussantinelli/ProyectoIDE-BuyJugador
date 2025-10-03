using DominioServicios;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPI.Endpoints
{
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

            group.MapPost("/completo", async (PedidoService pedidoService, [FromBody] CrearPedidoCompletoDTO pedidoDto) =>
            {
                try
                {
                    var nuevoPedido = await pedidoService.CrearPedidoCompletoAsync(pedidoDto);
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
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { message = ex.Message });
                }
            });
        }
    }
}

