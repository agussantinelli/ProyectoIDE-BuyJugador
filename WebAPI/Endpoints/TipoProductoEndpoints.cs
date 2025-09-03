using DominioServicios;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Endpoints
{
    public static class TipoProductoEndpoints
    {
        public static void MapTipoProductoEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/api/tiposproducto", async (TipoProductoService tipoProductoService) =>
            {
                var tiposProducto = await tipoProductoService.GetAllAsync();
                return Results.Ok(tiposProducto);
            });

            routes.MapGet("/api/tiposproducto/{id}", async (int id, TipoProductoService tipoProductoService) =>
            {
                var tipoProducto = await tipoProductoService.GetByIdAsync(id);
                return tipoProducto != null ? Results.Ok(tipoProducto) : Results.NotFound();
            });

            routes.MapPost("/api/tiposproducto", async (TipoProductoDTO tipoProductoDto, TipoProductoService tipoProductoService) =>
            {
                var createdTipoProducto = await tipoProductoService.CreateAsync(tipoProductoDto);
                return Results.Created($"/api/tiposproducto/{createdTipoProducto.IdTipoProducto}", createdTipoProducto);
            });

            routes.MapPut("/api/tiposproducto/{id}", async (int id, TipoProductoDTO tipoProductoDto, TipoProductoService tipoProductoService) =>
            {
                var updated = await tipoProductoService.UpdateAsync(id, tipoProductoDto);
                return updated ? Results.Ok() : Results.NotFound();
            });

            routes.MapDelete("/api/tiposproducto/{id}", async (int id, TipoProductoService tipoProductoService) =>
            {
                var deleted = await tipoProductoService.DeleteAsync(id);
                return deleted ? Results.Ok() : Results.NotFound();
            });
        }
    }
}
