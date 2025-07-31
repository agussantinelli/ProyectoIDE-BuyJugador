using DominioModelo;
using DominioServicios;
using DTOs;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System;
using System.Linq;

namespace WebAPI.Endpoints
{
    public static class TipoProductoEndpoints
    {
        public static void Map(WebApplication app)
        {
            // Endpoint para obtener todos los tipos de producto
            app.MapGet("/tiposproducto", (TipoProductoService tipoProductoService) =>
            {
                var tiposProductoDominio = tipoProductoService.GetAll();
                // Usando el constructor del DTO para crear la lista
                var tiposProductoDto = tiposProductoDominio.Select(tp => new DTOs.TipoProducto(tp.IdTipoProducto, tp.NombreTipoProducto)).ToList();
                return Results.Ok(tiposProductoDto);
            })
            .WithName("GetAllTiposProducto")
            .Produces<IReadOnlyList<DTOs.TipoProducto>>(StatusCodes.Status200OK)
            .WithOpenApi();

            // Endpoint para obtener un tipo de producto por su ID
            app.MapGet("/tiposproducto/{id}", (int id, TipoProductoService tipoProductoService) =>
            {
                var tipoProductoDominio = tipoProductoService.Get(id);
                if (tipoProductoDominio is null)
                {
                    return Results.NotFound();
                }
                // Usando el constructor del DTO para crear la instancia
                var tipoProductoDto = new DTOs.TipoProducto(tipoProductoDominio.IdTipoProducto, tipoProductoDominio.NombreTipoProducto);
                return Results.Ok(tipoProductoDto);
            })
            .WithName("GetTipoProductoById")
            .Produces<DTOs.TipoProducto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // Endpoint para agregar un nuevo tipo de producto
            app.MapPost("/tiposproducto", (DTOs.TipoProducto dto, TipoProductoService tipoProductoService) =>
            {
                try
                {
                    var tipoProducto = new DominioModelo.TipoProducto(dto.IdTipoProducto, dto.NombreTipoProducto);
                    bool added = tipoProductoService.Add(tipoProducto);
                    return added ? Results.Created($"/tiposproducto/{tipoProducto.IdTipoProducto}", dto) : Results.Conflict(new { error = "Ya existe un tipo de producto con ese nombre." });
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddTipoProducto")
            .Produces<DTOs.TipoProducto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .WithOpenApi();

            // Endpoint para actualizar un tipo de producto existente
            app.MapPut("/tiposproducto", (DTOs.TipoProducto dto, TipoProductoService tipoProductoService) =>
            {
                try
                {
                    var tipoProducto = new DominioModelo.TipoProducto(dto.IdTipoProducto, dto.NombreTipoProducto);
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