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
        public static WebApplication MapProductoEndpoints(this WebApplication app)
        {
            app.MapGet("/api/productos", async (ProductoService productoService) =>
            {
                var productos = await productoService.GetAllAsync();
                var productosDto = productos.Select(p => ProductoDTO.FromDominio(p)).ToList();
                return Results.Ok(productosDto);
            });

            app.MapGet("/api/productos/inactivos", async (ProductoService productoService) =>
            {
                var productos = await productoService.GetAllInactivosAsync();
                var productosDto = productos.Select(p => ProductoDTO.FromDominio(p)).ToList();
                return Results.Ok(productosDto);
            });

            app.MapGet("/api/productos/proveedor/{idProveedor}", async (int idProveedor, ProductoService productoService) =>
            {
                var productos = await productoService.GetByProveedorIdAsync(idProveedor);
                if (productos == null)
                {
                    return Results.NotFound();
                }

                var productosDto = productos.Select(p => {
                    var dto = ProductoDTO.FromDominio(p);
                    dto.PrecioCompra = p.PreciosCompra
                                        .Where(pc => pc.IdProveedor == idProveedor)
                                        .Select(pc => pc.Monto)
                                        .FirstOrDefault();
                    return dto;
                }).ToList();

                return Results.Ok(productosDto);
            });

            app.MapGet("/api/productos/{id}", async (int id, ProductoService productoService) =>
            {
                var producto = await productoService.GetByIdAsync(id);
                if (producto == null) return Results.NotFound();
                var dto = ProductoDTO.FromDominio(producto);
                return Results.Ok(dto);
            });

            app.MapPost("/api/productos", async (ProductoDTO productoDto, ProductoService productoService) =>
            {
                var producto = productoDto.ToDominio();
                await productoService.CreateAsync(producto);
                var nuevoDto = ProductoDTO.FromDominio(producto);
                return Results.Created($"/api/productos/{nuevoDto.IdProducto}", nuevoDto);
            });

            app.MapPut("/api/productos/{id}", async (int id, ProductoDTO productoDto, ProductoService productoService) =>
            {
                if (id != productoDto.IdProducto) return Results.BadRequest();
                var producto = productoDto.ToDominio();
                await productoService.UpdateAsync(producto);
                return Results.NoContent();
            });

            app.MapDelete("/api/productos/{id}", async (int id, ProductoService productoService) =>
            {
                await productoService.DeleteAsync(id);
                return Results.NoContent();
            });

            app.MapPost("/api/productos/{id}/reactivar", async (int id, ProductoService productoService) =>
            {
                await productoService.ReactivarAsync(id);
                return Results.NoContent();
            });

            return app;
        }
    }
}

