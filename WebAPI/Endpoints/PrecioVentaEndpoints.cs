using DominioServicios;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPI.Endpoints
{
    public static class PrecioVentaEndpoints
    {
        public static void MapPrecioVentaEndpoints(this WebApplication app)
        {
            var g = app.MapGroup("/api/precios-venta");

            // # (NUEVO) Endpoint para obtener los datos del reporte.
            g.MapGet("/historial", async (PrecioVentaService service) => Results.Ok(await service.GetHistorialPreciosAsync()));

            g.MapGet("/", async (PrecioVentaService service) => Results.Ok(await service.GetAllAsync()));

            g.MapGet("/vigente/{idProducto:int}", async (int idProducto, PrecioVentaService service) =>
            {
                var precio = await service.GetPrecioVigenteAsync(idProducto);
                return precio is not null ? Results.Ok(precio) : Results.NotFound();
            });

            g.MapGet("/{idProducto:int}/{fechaDesde:datetime}", async (int idProducto, DateTime fechaDesde, PrecioVentaService service) =>
            {
                var dto = await service.GetByIdAsync(idProducto, fechaDesde);
                return dto is not null ? Results.Ok(dto) : Results.NotFound();
            });

            g.MapPost("/", async (PrecioVentaDTO dto, PrecioVentaService service) =>
            {
                var creado = await service.CreateAsync(dto);
                return Results.Created($"/api/precios-venta/{creado.IdProducto}/{creado.FechaDesde:yyyy-MM-ddTHH:mm:ss}", creado);
            });

            g.MapPut("/{idProducto:int}/{fechaDesde:datetime}", async (int idProducto, DateTime fechaDesde, PrecioVentaDTO dto, PrecioVentaService service) =>
            {
                await service.UpdateAsync(idProducto, fechaDesde, dto);
                return Results.NoContent();
            });

            g.MapDelete("/{idProducto:int}/{fechaDesde:datetime}", async (int idProducto, DateTime fechaDesde, PrecioVentaService service) =>
            {
                await service.DeleteAsync(idProducto, fechaDesde);
                return Results.NoContent();
            });
        }
    }
}