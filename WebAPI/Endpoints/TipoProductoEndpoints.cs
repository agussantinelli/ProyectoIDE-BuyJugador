using DTOs;
using DominioServicios;

namespace WebAPI.Endpoints
{
    public static class TipoProductoEndpoints
    {
        public static void MapTipoProductoEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/tiposproducto");

            group.MapGet("/", async (TipoProductoService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            });

            group.MapGet("/{id}", async (int id, TipoProductoService service) =>
            {
                var tipo = await service.GetByIdAsync(id);
                return tipo is not null ? Results.Ok(tipo) : Results.NotFound();
            });

            group.MapPost("/", async (TipoProductoDTO dto, TipoProductoService service) =>
            {
                var nuevo = await service.CreateAsync(dto);
                return Results.Created($"/api/tiposproducto/{nuevo.IdTipoProducto}", nuevo);
            });

            group.MapPut("/{id}", async (int id, TipoProductoDTO dto, TipoProductoService service) =>
            {
                await service.UpdateAsync(id, dto);
                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (int id, TipoProductoService service) =>
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            });
        }
    }
}
