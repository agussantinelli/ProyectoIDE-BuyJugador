using DominioModelo;
using DominioServicios;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebAPI
{
    public static class EmpleadoEndpoints
    {
        public static void MapEmpleadoEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/empleados");

            group.MapGet("/", async (EmpleadoService service) =>
            {
                // Endpoint para obtener todos los empleados
                return Results.Ok(await service.GetAllAsync());
            });

            group.MapGet("/{id}", async (int id, EmpleadoService service) =>
            {
                // Endpoint para obtener un empleado por su ID
                var empleado = await service.GetByIdAsync(id);
                return empleado is not null ? Results.Ok(empleado) : Results.NotFound();
            });

            group.MapPost("/", async (Empleado empleado, EmpleadoService service) =>
            {
                // Endpoint para crear un nuevo empleado
                var nuevo = await service.CreateAsync(empleado);
                return Results.Created($"/api/empleados/{nuevo.Id}", nuevo);
            });

            //group.MapPut("/{id}", async (int id, Empleado empleado, EmpleadoService service) =>
            //{
            //    // Endpoint para actualizar un empleado existente
            //    var updatedEmpleado = await service.UpdateAsync(id, empleado);
            //    return updatedEmpleado is not null ? Results.NoContent() : Results.NotFound();
            //});

            //group.MapDelete("/{id}", async (int id, EmpleadoService service) =>
            //{
            //    // Endpoint para eliminar un empleado
            //    var deleted = await service.DeleteAsync(id);
            //    return deleted ? Results.NoContent() : Results.NotFound();
            //});
        }
    }
}
