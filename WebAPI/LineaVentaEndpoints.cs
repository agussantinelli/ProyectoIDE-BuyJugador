using DominioModelo;
using DominioServicios;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebAPI
{
    public static class LineaVentaEndpoints
    {
        public static void MapLineaVentaEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/lineaventas");

            group.MapGet("/", async (LineaVentaService service) =>
            {
                // Endpoint para obtener todas las líneas de venta
                return Results.Ok(await service.GetAllAsync());
            });

            group.MapGet("/{id}", async (int id, LineaVentaService service) =>
            {
                // Endpoint para obtener una línea de venta por su ID
                var lineaVenta = await service.GetByIdAsync(id);
                return lineaVenta is not null ? Results.Ok(lineaVenta) : Results.NotFound();
            });

            group.MapPost("/", async (LineaVenta lineaVenta, LineaVentaService service) =>
            {
                // Endpoint para crear una nueva línea de venta
                var nuevo = await service.CreateAsync(lineaVenta);
                return Results.Created($"/api/lineaventas/{nuevo.Id}", nuevo);
            });

            //group.MapPut("/{id}", async (int id, LineaVenta lineaVenta, LineaVentaService service) =>
            //{
            //    // Endpoint para actualizar una línea de venta existente
            //    var updatedLineaVenta = await service.UpdateAsync(id, lineaVenta);
            //    return updatedLineaVenta is not null ? Results.NoContent() : Results.NotFound();
            //});

            //group.MapDelete("/{id}", async (int id, LineaVentaService service) =>
            //{
            //    // Endpoint para eliminar una línea de venta
            //    var deleted = await service.DeleteAsync(id);
            //    return deleted ? Results.NoContent() : Results.NotFound();
            //});
        }
    }
}
