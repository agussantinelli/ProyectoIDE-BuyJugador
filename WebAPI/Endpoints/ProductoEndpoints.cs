using DTOs;
using DominioServicios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Endpoints
{
    public static class ProductoEndpoints
    {
        // CORRECCIÓN: Se renombra el método a "MapProductoEndpoints" para consistencia.
        public static WebApplication MapProductoEndpoints(this WebApplication app)
        {
            app.MapGet("/api/productos", async (ProductoService productoService) =>
            {
                var productos = await productoService.GetAllAsync();
                var productosDto = productos.Select(p => ProductoDTO.FromDominio(p)).ToList();
                return Results.Ok(productosDto);
            });

            app.MapGet("/api/productos/{id}", async (int id, ProductoService productoService) =>
            {
                var producto = await productoService.GetByIdAsync(id);
                if (producto == null)
                {
                    return Results.NotFound();
                }
                var productoDto = ProductoDTO.FromDominio(producto);
                return Results.Ok(productoDto);
            });

            app.MapPost("/api/productos", async (ProductoDTO productoDto, ProductoService productoService) =>
            {
                var producto = productoDto.ToDominio();
                await productoService.CreateAsync(producto);
                var nuevoProductoDto = ProductoDTO.FromDominio(producto);
                return Results.Created($"/api/productos/{nuevoProductoDto.IdProducto}", nuevoProductoDto);
            });

            app.MapPut("/api/productos/{id}", async (int id, ProductoDTO productoDto, ProductoService productoService) =>
            {
                if (id != productoDto.IdProducto)
                {
                    return Results.BadRequest();
                }

                var producto = productoDto.ToDominio();
                await productoService.UpdateAsync(producto);
                return Results.NoContent();
            });

            app.MapDelete("/api/productos/{id}", async (int id, ProductoService productoService) =>
            {
                await productoService.DeleteAsync(id);
                return Results.NoContent();
            });

            return app;
        }
    }
}

