using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScottPlot;
using SkiaSharp;               
using DominioServicios;

[ApiController]
[Route("api/graficos")]
public class GraficosController : ControllerBase
{
    private readonly PrecioVentaService _svc;

    public GraficosController(PrecioVentaService svc) => _svc = svc;

    [HttpGet("historial-precios.png")]
    [AllowAnonymous] // quítalo si no lo necesitás
    public async Task<IActionResult> HistorialPrecios(
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        [FromQuery] int w = 1200,
        [FromQuery] int h = 500)
    {
        if (w <= 0) w = 1200;
        if (h <= 0) h = 500;

        var historial = await _svc.GetHistorialPreciosAsync();
        if (historial is null || historial.Count == 0)
            return NotFound("Sin datos");

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
        byte[] pngBytes = img.GetImageBytes(ImageFormat.Png);     // ScottPlot.ImageFormat
        return File(pngBytes, "image/png");

    }
}
