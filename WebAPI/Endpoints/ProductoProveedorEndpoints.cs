using DominioServicios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WebAPI.Endpoints
{
    public static class ProductoProveedorEndpoints
    {
        public static void MapProductoProveedorEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/producto-proveedor");

            group.MapGet("/{idProveedor}", async (int idProveedor, ProductoProveedorService service) =>
            {
                return Results.Ok(await service.GetProductosByProveedorIdAsync(idProveedor));
            });

            group.MapPut("/{idProveedor}", async (int idProveedor, [FromBody] List<int> idProductos, ProductoProveedorService service) =>
            {
                try
                {
                    await service.UpdateProductosProveedorAsync(idProveedor, idProductos);
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Ocurrió un error: {ex.Message}");
                }
            });
        }
    }
}

