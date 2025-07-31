using DominioModelo;
using DominioServicios;
using DTOs;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System;

namespace WebAPI.Endpoints
{
    public static class ProvinciaEndpoints
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/provincias", (ProvinciaService provinciaService) =>
            {
                var provincias = provinciaService.GetAll();
                return Results.Ok(provincias);
            })
            .WithName("GetAllProvincias")
            .Produces<IReadOnlyList<DTOs.Provincia>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapGet("/provincias/{codigoProvincia}", (int codigoProvincia, ProvinciaService provinciaService) =>
            {
                var provincia = provinciaService.Get(codigoProvincia);
                return provincia is not null ? Results.Ok(provincia) : Results.NotFound();
            })
            .WithName("GetProvinciaByCodigo")
            .Produces<DTOs.Provincia>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPost("/provincias", (DominioModelo.Provincia provincia, ProvinciaService provinciaService) =>
            {
                try
                {
                    bool added = provinciaService.Add(provincia);
                    return added ? Results.Created($"/provincias/{provincia.CodigoProvincia}", provincia) : Results.Conflict(new { error = "Ya existe una provincia con ese nombre o código." });
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddProvincia")
            .Produces<DominioModelo.Provincia>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .WithOpenApi();

            app.MapPut("/provincias", (DominioModelo.Provincia provincia, ProvinciaService provinciaService) =>
            {
                try
                {
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