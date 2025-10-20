using DominioServicios;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScottPlot;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Endpoints
{
    public static class PrecioVentaEndpoints
    {
        public static void MapPrecioVentaEndpoints(this WebApplication app)
        {
            var g = app.MapGroup("/api/precios-venta");

            g.MapGet("/historial", async (PrecioVentaService service) => Results.Ok(await service.GetHistorialPreciosAsync()));

            g.MapGet("", async (PrecioVentaService service) => Results.Ok(await service.GetAllAsync()));

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

            g.MapPost("", async (PrecioVentaDTO dto, PrecioVentaService service) =>
            {
                var creado = await service.CreateAsync(dto);
                return Results.Created(
                    $"/api/precios-venta/{creado.IdProducto}/{creado.FechaDesde.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture)}",
                    creado);
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

            // ✅ PNG del historial (ScottPlot 5)
            g.MapGet("/historial.png",
            async Task<IResult> ([FromQuery] DateTime? from,
                                 [FromQuery] DateTime? to,
                                 [FromQuery] int w,
                                 [FromQuery] int h,
                                 PrecioVentaService service) =>
            {
                if (w <= 0) w = 1200;
                if (h <= 0) h = 500;

                var historial = await service.GetHistorialPreciosAsync();
                if (historial is null || historial.Count == 0)
                    return Results.NotFound("Sin datos");

                var plt = new Plot();
                plt.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.DateTimeAutomatic();
                plt.Title("Evolución de precios");
                plt.XLabel("Fecha");
                plt.YLabel("Precio");

                foreach (var prod in historial)
                {
                    var puntos = prod.Puntos
                        .Where(p => (!from.HasValue || p.Fecha.Date >= from.Value.Date) &&
                                    (!to.HasValue || p.Fecha.Date <= to.Value.Date))
                        .OrderBy(p => p.Fecha)
                        .ToList();

                    if (puntos.Count < 2) continue;

                    double[] xs = puntos.Select(p => p.Fecha.ToOADate()).ToArray();
                    double[] ys = puntos.Select(p => (double)p.Monto).ToArray();

                    var sc = plt.Add.Scatter(xs, ys);
                    sc.LegendText = prod.NombreProducto;
                    sc.LineWidth = 2;
                    sc.MarkerSize = 2;
                }

                plt.ShowLegend(ScottPlot.Alignment.UpperLeft);

                using var img = plt.GetImage(width: w, height: h);        // ScottPlot.Image
                byte[] pngBytes = img.GetImageBytes(ImageFormat.Png);    // ScottPlot.ImageFormat
                return Results.File(pngBytes, "image/png");
            });
        }
    }
}
