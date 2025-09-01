
using DominioModelo;
using DominioServicios;

namespace WebAPI
{
    public static class ProductoEndpoints
    {
        public static void MapProductoEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/productos");

            group.MapGet("/", async (ProductoService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            });

            group.MapGet("/{id}", async (int id, ProductoService service) =>
            {
                var producto = await service.GetByIdAsync(id);
                return producto is not null ? Results.Ok(producto) : Results.NotFound();
            });

            group.MapPost("/", async (Producto producto, ProductoService service) =>
            {
                var nuevo = await service.CreateAsync(producto);
                return Results.Created($"/api/productos/{nuevo.Id}", nuevo);
            });

            group.MapPut("/{id}", async (int id, Producto producto, ProductoService service) =>
            {
                await service.UpdateAsync(id, producto);
                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (int id, ProductoService service) =>
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            });
        }
    }
}
