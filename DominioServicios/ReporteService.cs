using Data.Repositories;
using DTOs;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using ScottPlot;
using ScottPlot.TickGenerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class ReporteService
    {
        private readonly ReporteRepository _reporteRepo;
        private readonly PersonaService _personaService;
        private readonly PrecioVentaService _precioVentaService;

        public ReporteService(
            ReporteRepository reporteRepo,
            PersonaService personaService,
            PrecioVentaService precioVentaService)
        {
            _reporteRepo = reporteRepo;
            _personaService = personaService;
            _precioVentaService = precioVentaService;
        }

        public static void Configure()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }


        public Task<List<ReporteVentasDTO>> GetVentasPorPersonaUltimos7DiasAsync(int idPersona)
            => _reporteRepo.GetVentasPorPersonaUltimos7DiasAsync(idPersona);

        public async Task<byte[]?> GetVentasVendedorPdfAsync(int idPersona)
        {
            var reporteData = await _reporteRepo.GetVentasPorPersonaUltimos7DiasAsync(idPersona);
            if (reporteData == null || reporteData.Count == 0) return null;

            var persona = await _personaService.GetByIdAsync(idPersona);
            var nombreVendedor = persona?.NombreCompleto ?? $"ID {idPersona}";

            return GenerateVentasPdf(reporteData, nombreVendedor);
        }

        private byte[] GenerateVentasPdf(List<ReporteVentasDTO> reporteData, string nombreVendedor)
        {
            var document = new PdfDocument();
            document.Info.Title = $"Reporte de Ventas - {nombreVendedor}";
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);

            var fontTitle = new XFont("DejaVu Sans", 16, XFontStyleEx.Bold);
            var fontHeader = new XFont("DejaVu Sans", 12, XFontStyleEx.Bold);
            var fontBody = new XFont("DejaVu Sans", 10, XFontStyleEx.Regular);
            var fontTotal = new XFont("DejaVu Sans", 12, XFontStyleEx.Bold);

            double yPos = 40;
            double leftMargin = 40;
            double rightMargin = page.Width - 40;

            gfx.DrawString($"Reporte de Ventas: {nombreVendedor}", fontTitle, XBrushes.Black,
                new XRect(0, yPos, page.Width, 0), XStringFormats.TopCenter);
            yPos += 30;

            gfx.DrawString($"Últimos 7 días - Generado el {DateTime.Now:dd/MM/yyyy HH:mm}", fontBody, XBrushes.Gray,
                new XRect(0, yPos, page.Width, 0), XStringFormats.TopCenter);
            yPos += 40;

            double[] columnWidths = { 80, 150, 120, 150 };
            string[] headers = { "ID Venta", "Fecha", "Estado", "Total" };
            double currentX = leftMargin;

            for (int i = 0; i < headers.Length; i++)
            {
                var format = (i == headers.Length - 1) ? XStringFormats.TopRight : XStringFormats.TopLeft;
                gfx.DrawString(headers[i], fontHeader, XBrushes.Black, new XRect(currentX, yPos, columnWidths[i], 0), format);
                currentX += columnWidths[i];
            }

            yPos += 20;
            gfx.DrawLine(XPens.Gray, leftMargin, yPos, rightMargin, yPos);
            yPos += 10;

            foreach (var item in reporteData)
            {
                currentX = leftMargin;
                gfx.DrawString(item.IdVenta.ToString(), fontBody, XBrushes.Black, new XRect(currentX, yPos, columnWidths[0], 0), XStringFormats.TopLeft);
                currentX += columnWidths[0];

                gfx.DrawString(item.Fecha.ToString("dd/MM/yyyy HH:mm"), fontBody, XBrushes.Black, new XRect(currentX, yPos, columnWidths[1], 0), XStringFormats.TopLeft);
                currentX += columnWidths[1];

                gfx.DrawString(item.Estado, fontBody, XBrushes.Black, new XRect(currentX, yPos, columnWidths[2], 0), XStringFormats.TopLeft);
                currentX += columnWidths[2];

                gfx.DrawString(item.TotalVenta.ToString("C2"), fontBody, XBrushes.Black, new XRect(currentX, yPos, columnWidths[3], 0), XStringFormats.TopRight);
                yPos += 25;

                if (yPos > page.Height - 80)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    yPos = 40;
                }
            }

            yPos += 10;
            gfx.DrawLine(XPens.Black, leftMargin, yPos, rightMargin, yPos);
            yPos += 15;

            var totalGeneral = reporteData.Sum(r => r.TotalVenta);
            double totalLabelWidth = 120;
            double totalValueX = rightMargin - columnWidths[3];
            double totalLabelX = totalValueX - totalLabelWidth - 5;

            gfx.DrawString("Total General:", fontTotal, XBrushes.Black, new XRect(totalLabelX, yPos, totalLabelWidth, 0), XStringFormats.TopRight);
            gfx.DrawString(totalGeneral.ToString("C2"), fontTotal, XBrushes.Black, new XRect(totalValueX, yPos, columnWidths[3], 0), XStringFormats.TopRight);

            using var stream = new MemoryStream();
            document.Save(stream, false);
            return stream.ToArray();
        }

        public async Task<byte[]?> GetHistorialPreciosPngAsync(DateTime? from, DateTime? to, int w, int h)
        {
            if (w <= 0) w = 1200;
            if (h <= 0) h = 500;

            var historial = await _precioVentaService.GetHistorialPreciosAsync();
            if (historial is null || !historial.Any())
                return null;

            var plt = new Plot();
            plt.Axes.Bottom.TickGenerator = new DateTimeAutomatic();
            plt.Title("Evolución de precios");
            plt.XLabel("Fecha");
            plt.YLabel("Precio");

            bool anySeries = false;

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
                sc.Label = prod.NombreProducto;
                sc.LineWidth = 2;
                sc.MarkerSize = 2;

                anySeries = true;
            }

            if (!anySeries)
                return null; 

            plt.ShowLegend(Alignment.UpperLeft);

            using var img = plt.GetImage(width: w, height: h);
            return img.GetImageBytes(ImageFormat.Png);
        }


        public async Task<byte[]?> GetHistorialPreciosPdfAsync(DateTime? from, DateTime? to, int w, int h)
        {
            var png = await GetHistorialPreciosPngAsync(from, to, w, h);
            if (png == null) return null;

            var document = new PdfSharp.Pdf.PdfDocument();
            document.Info.Title = "Historial de precios";

            var page = document.AddPage();
            if (w > h) page.Orientation = PdfSharp.PageOrientation.Landscape;

            using (var gfx = XGraphics.FromPdfPage(page))
            {
                var fontTitle = new XFont("DejaVu Sans", 16, XFontStyleEx.Bold);
                var fontSub = new XFont("DejaVu Sans", 10, XFontStyleEx.Regular);

                double top = 30;
                gfx.DrawString("Historial de precios", fontTitle, XBrushes.Black,
                    new XRect(0, top, page.Width, 0), XStringFormats.TopCenter);
                top += 20;

                string rango = $"{(from.HasValue ? from.Value : "—"):dd/MM/yyyy} a {(to.HasValue ? to.Value : "—"):dd/MM/yyyy}";
                gfx.DrawString($"Rango: {rango}", fontSub, XBrushes.DimGray,
                    new XRect(0, top, page.Width, 0), XStringFormats.TopCenter);
                top += 20;

                gfx.DrawString($"Generado el {DateTime.Now:dd/MM/yyyy HH:mm:ss}", fontSub, XBrushes.DimGray,
                    new XRect(0, top, page.Width, 0), XStringFormats.TopCenter);
                top += 20;

                using var imageStream = new MemoryStream(png, 0, png.Length, writable: false, publiclyVisible: true);
                imageStream.Position = 0; 

                using var xImg = XImage.FromStream(imageStream);

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
            return outStream.ToArray();
        }

    }
}
