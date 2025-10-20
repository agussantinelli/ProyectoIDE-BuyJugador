using DTOs;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DominioServicios
{
    public class PdfReportService
    {
        public byte[] GenerateVentasPdf(List<ReporteVentasDTO> reporteData, string nombreVendedor)
        {
            // #Intención: Configurar el documento y las fuentes a utilizar.
            // #Corrección: Se utiliza XFontStyleEx en lugar de XFontStyle para compatibilidad con las versiones modernas de PDFsharp.
            var document = new PdfDocument();
            document.Info.Title = $"Reporte de Ventas - {nombreVendedor}";
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);

            var fontTitle = new XFont("Arial", 16, XFontStyleEx.Bold);
            var fontHeader = new XFont("Arial", 12, XFontStyleEx.Bold);
            var fontBody = new XFont("Arial", 10, XFontStyleEx.Regular);
            var fontTotal = new XFont("Arial", 12, XFontStyleEx.Bold);

            double yPos = 40;
            double leftMargin = 40;
            double rightMargin = page.Width - 40;

            // --- Título ---
            gfx.DrawString($"Reporte de Ventas: {nombreVendedor}", fontTitle, XBrushes.Black, new XRect(0, yPos, page.Width, 0), XStringFormats.TopCenter);
            yPos += 30;
            gfx.DrawString($"Últimos 7 días - Generado el {System.DateTime.Now:dd/MM/yyyy HH:mm}", fontBody, XBrushes.Gray, new XRect(0, yPos, page.Width, 0), XStringFormats.TopCenter);
            yPos += 40;

            // --- Cabecera de la Tabla ---
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

            // --- Filas de Datos ---
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
                    yPos = 40; // Reiniciar en la nueva página
                }
            }

            // --- Línea y Total General ---
            yPos += 10;
            gfx.DrawLine(XPens.Black, leftMargin, yPos, rightMargin, yPos);
            yPos += 15;

            var totalGeneral = reporteData.Sum(r => r.TotalVenta);
            gfx.DrawString("Total General:", fontTotal, XBrushes.Black, new XRect(leftMargin, yPos, page.Width - leftMargin - rightMargin, 0), XStringFormats.TopRight);
            gfx.DrawString(totalGeneral.ToString("C2"), fontTotal, XBrushes.Black, new XRect(rightMargin - columnWidths[3], yPos, columnWidths[3], 0), XStringFormats.TopRight);

            // --- Guardar en memoria ---
            using (var stream = new MemoryStream())
            {
                document.Save(stream, false);
                return stream.ToArray();
            }
        }
    }
}

