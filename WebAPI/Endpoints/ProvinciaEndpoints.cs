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
    public static class ProvinciaEndpoints
    {
        public static void Map(WebApplication app)
        {
            // Endpoint para obtener todas las provincias
            app.MapGet("/provincias", (ProvinciaService provinciaService) =>
            {
                var provinciasDominio = provinciaService.GetAll();
                // Usando el constructor del DTO para crear la lista
                var provinciasDto = provinciasDominio.Select(p => new DTOs.Provincia(p.CodigoProvincia, p.NombreProvincia)).ToList();
                return Results.Ok(provinciasDto);
            })
            .WithName("GetAllProvincias")
            .Produces<IReadOnlyList<DTOs.Provincia>>(StatusCodes.Status200OK)
            .WithOpenApi();

            // Endpoint para obtener una provincia por su código
            app.MapGet("/provincias/{codigoProvincia}", (int codigoProvincia, ProvinciaService provinciaService) =>
            {
                var provinciaDominio = provinciaService.Get(codigoProvincia);
                if (provinciaDominio is null)
                {
                    return Results.NotFound();
                }
                // Usando el constructor del DTO para crear la instancia
                var provinciaDto = new DTOs.Provincia(provinciaDominio.CodigoProvincia, provinciaDominio.NombreProvincia);
                return Results.Ok(provinciaDto);
            })
            .WithName("GetProvinciaByCodigo")
            .Produces<DTOs.Provincia>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // Endpoint para agregar una nueva provincia
            app.MapPost("/provincias", (DTOs.Provincia dto, ProvinciaService provinciaService) =>
            {
                try
                {
                    var provincia = new DominioModelo.Provincia(dto.CodigoProvincia, dto.NombreProvincia);
                    bool added = provinciaService.Add(provincia);
                    return added ? Results.Created($"/provincias/{provincia.CodigoProvincia}", dto) : Results.Conflict(new { error = "Ya existe una provincia con ese nombre o código." });
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddProvincia")
            .Produces<DTOs.Provincia>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .WithOpenApi();

            // Endpoint para actualizar una provincia existente
            app.MapPut("/provincias", (DTOs.Provincia dto, ProvinciaService provinciaService) =>
            {
                try
                {
                    var provincia = new DominioModelo.Provincia(dto.CodigoProvincia, dto.NombreProvincia);
                    bool updated = provinciaService.Update(provincia);
                    return updated ? Results.NoContent() : Results.NotFound();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("UpdateProvincia")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            // Endpoint para eliminar una provincia por su código
            app.MapDelete("/provincias/{codigoProvincia}", (int codigoProvincia, ProvinciaService provinciaService) =>
            {
                bool deleted = provinciaService.Delete(codigoProvincia);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteProvincia")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}