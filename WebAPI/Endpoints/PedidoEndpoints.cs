using DominioServicios;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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

            group.MapGet("/{id}", async (int id, PedidoService pedidoService) =>
            {
                var pedido = await pedidoService.GetPedidoDetalladoByIdAsync(id);

                return pedido is not null ? Results.Ok(pedido) : Results.NotFound();
            });

            group.MapPost("/completo", async (PedidoService pedidoService, [FromBody] CrearPedidoCompletoDTO pedidoDto) =>
            {
                var nuevoPedido = await pedidoService.CrearPedidoCompletoAsync(pedidoDto);

                if (nuevoPedido != null)
                {
                    return Results.Created($"/api/pedidos/{nuevoPedido.IdPedido}", nuevoPedido);
                }

                return Results.BadRequest(new { message = "Error al crear el pedido. Verifique que todos los productos y precios de compra existan." });
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

            group.MapPut("/{id}", async (int id, PedidoDTO pedidoDto, PedidoService service) =>
            {
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
}

