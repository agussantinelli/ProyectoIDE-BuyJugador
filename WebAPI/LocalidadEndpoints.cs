using DominioModelo;
using DominioServicios;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebAPI
{
    public static class LocalidadEndpoints
    {
        public static void MapLocalidadEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/localidades");

            group.MapGet("/", async (LocalidadService service) =>
            {
                // Endpoint para obtener todas las localidades
                return Results.Ok(await service.GetAllAsync());
            });

            group.MapGet("/{id}", async (int id, LocalidadService service) =>
            {
                // Endpoint para obtener una localidad por su ID
                var localidad = await service.GetByIdAsync(id);
                return localidad is not null ? Results.Ok(localidad) : Results.NotFound();
            });

            //group.MapPost("/", async (Localidad localidad, LocalidadService service) =>
            //{
            //    // Endpoint para crear una nueva localidad
            //    var nuevo = await service.CreateAsync(localidad);
            //    return Results.Created($"/api/localidades/{nuevo.Id}", nuevo);
            //});

            //group.MapPut("/{id}", async (int id, Localidad localidad, LocalidadService service) =>
            //{
            //    // Endpoint para actualizar una localidad existente
            //    var updatedLocalidad = await service.UpdateAsync(id, localidad);
            //    return updatedLocalidad is not null ? Results.NoContent() : Results.NotFound();
            //});

            //group.MapDelete("/{id}", async (int id, LocalidadService service) =>
            //{
            //    // Endpoint para eliminar una localidad
            //    var deleted = await service.DeleteAsync(id);
            //    return deleted ? Results.NoContent() : Results.NotFound();
            //});
        }
    }
}
