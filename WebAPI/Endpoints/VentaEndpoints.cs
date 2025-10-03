using DTOs;
using DominioServicios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public static class VentaEndpoints
{
    public static void MapVentaEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/ventas");

        group.MapGet("/", async (VentaService ventaService) =>
        {
            var ventas = await ventaService.GetVentas().Select(v => VentaDTO.FromDominio(v)).ToListAsync();
            foreach (var venta in ventas)
            {
                var ventaConDetalles = await ventaService.GetVentaByIdAsync(venta.IdVenta);

                // --- CORRECCIÓN ---
                // Se usa FirstOrDefault() para manejar productos sin precios.
                // Si no hay precio, se asume 0.
                if (ventaConDetalles?.LineaVenta != null)
                {
                    venta.Total = ventaConDetalles.LineaVenta.Sum(l =>
                        l.Cantidad * (l.IdProductoNavigation?.Precios.OrderByDescending(p => p.FechaDesde).FirstOrDefault()?.Monto ?? 0)
                    );
                }
            }
            return Results.Ok(ventas);
        });

        group.MapGet("/{id:int}", async (int id, VentaService ventaService) =>
        {
            var venta = await ventaService.GetVentaByIdAsync(id);
            if (venta == null) return Results.NotFound();

            var ventaDto = VentaDTO.FromDominio(venta);
            ventaDto.Lineas = venta.LineaVenta.Select(LineaVentaDTO.FromDominio).ToList();

            foreach (var linea in ventaDto.Lineas)
            {
                var producto = venta.LineaVenta
                   .First(l => l.NroLineaVenta == linea.NroLineaVenta).IdProductoNavigation;

                // --- CORRECCIÓN ---
                // Se usa FirstOrDefault() también aquí para evitar el mismo error.
                linea.PrecioUnitario = producto.Precios?.OrderByDescending(p => p.FechaDesde).FirstOrDefault()?.Monto ?? 0;
                linea.Subtotal = linea.Cantidad * linea.PrecioUnitario;
            }
            ventaDto.Total = ventaDto.Lineas.Sum(l => l.Subtotal);

            return Results.Ok(ventaDto);
        });

        group.MapPost("/completa", async (CrearVentaCompletaDTO ventaDto, VentaService ventaService) =>
        {
            var nuevaVenta = await ventaService.CrearVentaCompletaAsync(ventaDto);
            return Results.Created($"/api/ventas/{nuevaVenta.IdVenta}", VentaDTO.FromDominio(nuevaVenta));
        });

        group.MapPut("/completa/{id:int}", async (int id, CrearVentaCompletaDTO ventaDto, VentaService ventaService) =>
        {
            if (id != ventaDto.IdVenta)
            {
                return Results.BadRequest("El ID de la URL no coincide con el ID de la venta.");
            }
            try
            {
                await ventaService.UpdateVentaCompletaAsync(ventaDto);
                return Results.Ok();
            }
            catch (System.Collections.Generic.KeyNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        });

        group.MapDelete("/{id:int}", async (int id, VentaService ventaService) =>
        {
            var result = await ventaService.DeleteVentaAsync(id);
            if (!result) return Results.NotFound();
            return Results.Ok();
        });
    }
}

