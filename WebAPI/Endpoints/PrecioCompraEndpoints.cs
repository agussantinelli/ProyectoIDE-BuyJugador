using DominioServicios;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Endpoints
{
    public static class PrecioCompraEndpoints
    {
        public static void MapPrecioCompraEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/preciocompra");

            group.MapGet("/", async (PrecioCompraService service) =>
            {
                var todos = await service.GetAllAsync();
                return Results.Ok(todos);
            });

            group.MapGet("/{idProducto}/{idProveedor}", async (int idProducto, int idProveedor, PrecioCompraService service) =>
            {
                var precio = await service.GetByIdAsync(idProducto, idProveedor);
                return precio is not null ? Results.Ok(precio) : Results.NotFound();
            });

            group.MapPost("/", async (PrecioCompraService service, [FromBody] PrecioCompraDTO dto) =>
            {
                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/preciocompra/{created.IdProducto}/{created.IdProveedor}", created);
            });

            group.MapPut("/{idProducto}/{idProveedor}", async (int idProducto, int idProveedor, [FromBody] PrecioCompraDTO dto, PrecioCompraService service) =>
            {
                await service.UpdateAsync(idProducto, idProveedor, dto);
                return Results.NoContent();
            });

            group.MapDelete("/{idProducto}/{idProveedor}", async (int idProducto, int idProveedor, PrecioCompraService service) =>
            {
                await service.DeleteAsync(idProducto, idProveedor);
                return Results.NoContent();
            });
        }
    }
}
