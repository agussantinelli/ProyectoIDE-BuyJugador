using DominioServicios;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using ScottPlot;
using ScottPlot.TickGenerators;
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

                using var img = plt.GetImage(width: w, height: h);      
                byte[] pngBytes = img.GetImageBytes(ImageFormat.Png);   
                return Results.File(pngBytes, "image/png");
            });

            g.MapGet("/historial.pdf",
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
                plt.Axes.Bottom.TickGenerator = new DateTimeAutomatic();
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

                using var img = plt.GetImage(width: w, height: h); 
                byte[] pngBytes = img.GetImageBytes(ImageFormat.Png);

                var document = new PdfDocument();
                document.Info.Title = "Historial de precios";

                var page = document.AddPage();
                if (w > h)
                    page.Orientation = PdfSharp.PageOrientation.Landscape;

                using (var gfx = XGraphics.FromPdfPage(page))
                {
                    var fontTitle = new XFont("DejaVu Sans", 16, XFontStyleEx.Bold);
                    var fontSub = new XFont("DejaVu Sans", 10, XFontStyleEx.Regular);

                    double top = 30;
                    gfx.DrawString("Historial de precios", fontTitle, XBrushes.Black,
                        new XRect(0, top, page.Width, 0), XStringFormats.TopCenter);
                    top += 20;

                    string rango = $"{(from.HasValue ? from.Value.ToString("dd/MM/yyyy") : "—")} a {(to.HasValue ? to.Value.ToString("dd/MM/yyyy") : "—")}";
                    gfx.DrawString($"Rango: {rango}", fontSub, XBrushes.DimGray,
                        new XRect(0, top, page.Width, 0), XStringFormats.TopCenter);
                    top += 20;
                   
                    string generado = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    gfx.DrawString($"Generado el {generado}", fontSub, XBrushes.DimGray,
                        new XRect(0, top, page.Width, 0), XStringFormats.TopCenter);
                    top += 20;

                    using var ms = new MemoryStream(pngBytes);
                    using var xImg = XImage.FromStream(ms);

                    double margin = 30;
                    double availW = page.Width - margin * 2;
                    double availH = page.Height - top - margin;

                    double scale = Math.Min(availW / xImg.PixelWidth, availH / xImg.PixelHeight);
                    double drawW = xImg.PixelWidth * scale;
                    double drawH = xImg.PixelHeight * scale;

                    double x = (page.Width - drawW) / 2.0;
                    double y = top + ((availH - drawH) / 2.0);

                    gfx.DrawImage(xImg, x, y, drawW, drawH);
                }

                using var outStream = new MemoryStream();
                document.Save(outStream, false);
                var fileName = $"Historial {DateTime.Now:dd-MM-yyyy HH.mm.ss}.pdf";
                return Results.File(outStream.ToArray(), "application/pdf", fileName);
            });
        }
    }
}
