using DTOs;
using DominioServicios;

namespace WebAPI.Endpoints
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

            group.MapPost("/", async (ProductoDTO dto, ProductoService service) =>
            {
                var nuevo = await service.CreateAsync(dto);
                return Results.Created($"/api/productos/{nuevo.IdProducto}", nuevo);
            });

            group.MapPut("/{id}", async (int id, ProductoDTO dto, ProductoService service) =>
            {
                await service.UpdateAsync(id, dto);
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
