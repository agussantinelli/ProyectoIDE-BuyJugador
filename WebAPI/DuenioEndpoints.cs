using DominioModelo;
using DominioServicios;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebAPI
{
    public static class DuenioEndpoints
    {
        public static void MapDuenioEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/duenios");

            group.MapGet("/", async (DuenioService service) =>
            {
                // Endpoint para obtener todos los dueños
                return Results.Ok(await service.GetAllAsync());
            });

            group.MapGet("/{id}", async (int id, DuenioService service) =>
            {
                // Endpoint para obtener un dueñio por su ID
                var duenio = await service.GetByIdAsync(id);
                return duenio is not null ? Results.Ok(duenio) : Results.NotFound();
            });

            group.MapPost("/", async (Duenio duenio, DuenioService service) =>
            {
                // Endpoint para crear un nuevo dueño
                var nuevo = await service.CreateAsync(duenio);
                return Results.Created($"/api/duenios/{nuevo.Id}", nuevo);
            });

            //group.MapPut("/{id}", async (int id, Duenio duenio, DuenioService service) =>
            //{
            //    // Endpoint para actualizar un dueño existente
            //    var updatedDuenio = await service.UpdateAsync(id, duenio);
            //    return updatedDuenio is not null ? Results.NoContent() : Results.NotFound();
            //});

            //group.MapDelete("/{id}", async (int id, DuenioService service) =>
            //{
            //    // Endpoint para eliminar un dueño
            //    var deleted = await service.DeleteAsync(id);
            //    return deleted ? Results.NoContent() : Results.NotFound();
            //});
        }
    }
}
