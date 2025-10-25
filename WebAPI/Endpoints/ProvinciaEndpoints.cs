using DominioServicios;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace WebAPI.Endpoints
{
    public static class ProvinciaEndpoints
    {
        public static void MapProvinciaEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/api/provincias", async (ProvinciaService provinciaService) =>
            {
                var provincias = await provinciaService.GetAllAsync();
                return Results.Ok(provincias);
            });

            routes.MapGet("/api/provincias/{id}", async (int id, ProvinciaService provinciaService) =>
            {
                var provincia = await provinciaService.GetByIdAsync(id);
                return provincia != null ? Results.Ok(provincia) : Results.NotFound();
            });

            routes.MapPost("/api/provincias", async (ProvinciaDTO provinciaDto, ProvinciaService provinciaService) =>
            {
                var createdProvincia = await provinciaService.CreateAsync(provinciaDto);
                return Results.Created($"/api/provincias/{createdProvincia.IdProvincia}", createdProvincia);
            });

            routes.MapPut("/api/provincias/{id}", async (int id, ProvinciaDTO provinciaDto, ProvinciaService provinciaService) =>
            {
                var updated = await provinciaService.UpdateAsync(id, provinciaDto);
                return updated ? Results.Ok() : Results.NotFound();
            });

            routes.MapDelete("/api/provincias/{id}", async (int id, ProvinciaService provinciaService) =>
            {
                var deleted = await provinciaService.DeleteAsync(id);
                return deleted ? Results.Ok() : Results.NotFound();
            });
        }
    }
}
