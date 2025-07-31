using DominioModelo;
using DominioServicios;
using DTOs;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System;

namespace WebAPI.Endpoints
{
    public static class TipoProductoEndpoints
    {
        public static void Map(WebApplication app)
        {
            // Endpoint para obtener todos los tipos de producto
            app.MapGet("/tiposproducto", (TipoProductoService tipoProductoService) =>
            {
                var tiposProducto = tipoProductoService.GetAll();
                return Results.Ok(tiposProducto);
            })
            .WithName("GetAllTiposProducto")
            .Produces<IReadOnlyList<TipoProducto>>(StatusCodes.Status200OK)
            .WithOpenApi();

            // Endpoint para obtener un tipo de producto por su ID
            app.MapGet("/tiposproducto/{id}", (int id, TipoProductoService tipoProductoService) =>
            {
                var tipoProducto = tipoProductoService.Get(id);
                return tipoProducto is not null ? Results.Ok(tipoProducto) : Results.NotFound();
            })
            .WithName("GetTipoProductoById")
            .Produces<TipoProducto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // Endpoint para agregar un nuevo tipo de producto
            app.MapPost("/tiposproducto", (TipoProducto tipoProducto, TipoProductoService tipoProductoService) =>
            {
                try
                {
                    bool added = tipoProductoService.Add(tipoProducto);
                    return added ? Results.Created($"/tiposproducto/{tipoProducto.IdTipoProducto}", tipoProducto) : Results.Conflict(new { error = "Ya existe un tipo de producto con ese nombre." });
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddTipoProducto")
            .Produces<TipoProducto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .WithOpenApi();

            // Endpoint para actualizar un tipo de producto existente
            app.MapPut("/tiposproducto", (TipoProducto tipoProducto, TipoProductoService tipoProductoService) =>
            {
                try
                {
                    bool updated = tipoProductoService.Update(tipoProducto);
                    return updated ? Results.NoContent() : Results.NotFound();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("UpdateTipoProducto")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            // Endpoint para eliminar un tipo de producto por su ID
            app.MapDelete("/tiposproducto/{id}", (int id, TipoProductoService tipoProductoService) =>
            {
                bool deleted = tipoProductoService.Delete(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteTipoProducto")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}