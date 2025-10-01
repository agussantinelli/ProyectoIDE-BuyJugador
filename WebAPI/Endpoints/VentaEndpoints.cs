using DTOs;
using DominioServicios;
using System;

namespace WebAPI.Endpoints
{
    public static class VentaEndpoints
    {
        public static void MapVentaEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/ventas");

            group.MapGet("/", async (VentaService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            });

            group.MapGet("/{id}", async (int id, VentaService service) =>
            {
                var venta = await service.GetByIdAsync(id);
                return venta is not null ? Results.Ok(venta) : Results.NotFound();
            });

            group.MapPost("/completa", async (CrearVentaCompletaDTO dto, VentaService service) =>
            {
                try
                {
                    var nuevaVenta = await service.CreateVentaCompletaAsync(dto);
                    return Results.Created($"/api/ventas/{nuevaVenta.IdVenta}", nuevaVenta);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { message = ex.Message });
                }
            });

            group.MapPut("/{id}", async (int id, VentaDTO dto, VentaService service) =>
            {
                await service.UpdateAsync(id, dto);
                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (int id, VentaService service) =>
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            });
        }
    }
}

