using DTOs;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DominioServicios
{
    public class PdfReportService
    {
        // #CORRECCIÓN: Se añade un constructor estático para configurar el resolver de fuentes.
        // #Razón: La librería PDFsharp necesita ayuda para encontrar las fuentes del sistema (como Arial)
        // #cuando se ejecuta en un entorno .NET Core/.NET 8. Esto asegura que funcione correctamente.
        static PdfReportService()
        {
            GlobalFontSettings.FontResolver = new FontResolver();
        }

        public byte[] GenerateVentasPdf(IEnumerable<ReporteVentasDTO> reporteData, string nombreVendedor)
        {
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);

            var fontTitle = new XFont("Arial", 18, XFontStyle.Bold);
            var fontHeader = new XFont("Arial", 10, XFontStyle.Bold);
            var fontBody = new XFont("Arial", 9, XFontStyle.Regular);
            var fontTotal = new XFont("Arial", 10, XFontStyle.Bold);

            double yPos = 60;
            double xMargin = 40;

            // Título
            gfx.DrawString("Reporte de Ventas (Últimos 7 días)", fontTitle, XBrushes.Black, new XRect(0, yPos, page.Width, 0), XStringFormats.TopCenter);
            yPos += 30;
            gfx.DrawString($"Vendedor: {nombreVendedor}", new XFont("Arial", 12, XFontStyle.Regular), XBrushes.Black, new XRect(0, yPos, page.Width, 0), XStringFormats.TopCenter);
            yPos += 40;

            // Cabeceras de la tabla
            double[] columnWidths = { 80, 150, 120, 120 };
            string[] headers = { "ID Venta", "Fecha", "Estado", "Total" };

            for (int i = 0; i < headers.Length; i++)
            {
                var xPos = xMargin + columnWidths.Take(i).Sum();
                gfx.DrawRectangle(XBrushes.LightGray, xPos, yPos, columnWidths[i], 20);
                gfx.DrawString(headers[i], fontHeader, XBrushes.Black, new XRect(xPos + 5, yPos + 3, columnWidths[i] - 10, 20), XStringFormats.TopLeft);
            }
            yPos += 20;

            // Filas de datos
            foreach (var item in reporteData)
            {
                if (yPos > page.Height - 80) // Salto de página si no hay espacio
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    yPos = 60;
                }

                string[] rowData = {
                    item.IdVenta.ToString(),
                    item.Fecha.ToString("dd/MM/yyyy HH:mm"),
                    item.Estado,
                    item.TotalVenta.ToString("C2")
                };

                for (int i = 0; i < rowData.Length; i++)
                {
                    var xPos = xMargin + columnWidths.Take(i).Sum();
                    gfx.DrawRectangle(XPens.Gray, xPos, yPos, columnWidths[i], 20);
                    XStringFormat format = (i == 3) ? XStringFormats.TopRight : XStringFormats.TopLeft;
                    gfx.DrawString(rowData[i], fontBody, XBrushes.Black, new XRect(xPos + 5, yPos + 4, columnWidths[i] - 10, 20), format);
                }
                yPos += 20;
            }

            // Fila de Total
            var totalGeneral = reporteData.Sum(r => r.TotalVenta);
            var totalXPosLabel = xMargin + columnWidths.Take(3).Sum() - 5; // Posición para "Total General:"
            var totalXPosValue = xMargin + columnWidths.Sum(); // Posición para el valor

            gfx.DrawString("Total General:", fontTotal, XBrushes.Black, new XRect(0, yPos + 5, totalXPosLabel, 20), XStringFormats.TopRight);
            gfx.DrawString(totalGeneral.ToString("C2"), fontTotal, XBrushes.Black, new XRect(0, yPos + 5, totalXPosValue - 5, 20), XStringFormats.TopRight);

            using (var stream = new MemoryStream())
            {
                document.Save(stream, false);
                return stream.ToArray();
            }
        }
    }

    // #NUEVO: Implementación de IFontResolver para que PDFsharp encuentre las fuentes del sistema.
    public class FontResolver : IFontResolver
    {
        public byte[] GetFont(string faceName)
        {
            // Busca la fuente en las carpetas de fuentes comunes de Windows y Linux
            var fontPaths = new[]
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf"), // Windows
                "/usr/share/fonts/truetype/msttcorefonts/Arial.ttf", // Linux (Debian/Ubuntu)
                "/usr/share/fonts/corefonts/Arial.ttf", // Linux (CentOS/RHEL)
            };

            var fontPath = fontPaths.FirstOrDefault(File.Exists);
            if (fontPath != null)
            {
                return File.ReadAllBytes(fontPath);
            }

            // Si no se encuentra, podrías lanzar una excepción o devolver una fuente de respaldo.
            // Por simplicidad, devolvemos un array vacío, lo que podría causar un error si no se encuentra Arial.
            return Array.Empty<byte>();
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            // Para este ejemplo simple, no manejamos negrita/cursiva de forma especial.
            // PDFsharp intentará simularlos si no encuentra la fuente específica.
            return new FontResolverInfo(familyName);
        }
    }
}

