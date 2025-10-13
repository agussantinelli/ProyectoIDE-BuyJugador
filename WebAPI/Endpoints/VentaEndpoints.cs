using DTOs;
using DominioServicios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;

public static class VentaEndpoints
{
    public static void MapVentaEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/ventas");

        group.MapGet("/", async (VentaService ventaService) =>
        {
            // # REFACTORIZADO: Se simplifica la obtención del total.
            var ventas = await ventaService.GetVentas()
                .Include(v => v.LineaVenta)
                .Select(v => new VentaDTO
                {
                    IdVenta = v.IdVenta,
                    Fecha = v.Fecha,
                    Estado = v.Estado,
                    IdPersona = v.IdPersona,
                    NombreVendedor = v.IdPersonaNavigation.NombreCompleto,
                    Total = v.LineaVenta.Sum(l => l.Cantidad * l.PrecioUnitario)
                })
                .ToListAsync();

            return Results.Ok(ventas);
        });

        group.MapGet("/{id:int}", async (int id, VentaService ventaService) =>
        {
            var venta = await ventaService.GetVentaByIdAsync(id);
            if (venta == null) return Results.NotFound();

            var ventaDto = VentaDTO.FromDominio(venta);

            // # REFACTORIZADO: El mapeo a DTO ahora es más directo.
            ventaDto.Lineas = venta.LineaVenta.Select(l => LineaVentaDTO.FromDominio(l)).ToList();
            ventaDto.Total = ventaDto.Lineas.Sum(l => l.Subtotal);

            return Results.Ok(ventaDto);
        });

        group.MapPost("/completa", async (CrearVentaCompletaDTO ventaDto, VentaService ventaService) =>
        {
            try
            {
                var nuevaVenta = await ventaService.CrearVentaCompletaAsync(ventaDto);
                return Results.Created($"/api/ventas/{nuevaVenta.IdVenta}", VentaDTO.FromDominio(nuevaVenta));
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = $"Error al crear la venta: {ex.Message}" });
            }
        });

        group.MapPut("/completa/{id:int}", async (int id, CrearVentaCompletaDTO ventaDto, VentaService ventaService) =>
        {
            if (id != ventaDto.IdVenta)
                return Results.BadRequest("El ID de la URL no coincide con el ID de la venta.");

            try
            {
                await ventaService.UpdateVentaCompletaAsync(ventaDto);
                return Results.NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return Results.NotFound(new { message = e.Message });
            }
            catch (InvalidOperationException e)
            {
                return Results.BadRequest(new { message = e.Message });
            }
            catch (Exception ex)
            {
                return Results.Problem($"Ocurrió un error inesperado: {ex.Message}");
            }
        });

        group.MapDelete("/{id:int}", async (int id, VentaService ventaService) =>
        {
            var result = await ventaService.DeleteVentaAsync(id);
            return result ? Results.NoContent() : Results.NotFound();
        });
    }
}
