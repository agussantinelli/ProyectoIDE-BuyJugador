using DominioServicios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace WebAPI.Endpoints
{
    public static class ReporteEndpoints
    {
        public static void MapReporteEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/reportes").RequireAuthorization(); // #BuenasPrácticas: Asegurar endpoints

            // Endpoint para obtener los datos del reporte en JSON
            group.MapGet("/ventas-vendedor/{idPersona:int}", async (int idPersona, ReporteService service) =>
            {
                var reporte = await service.GetVentasPorPersonaUltimos7DiasAsync(idPersona);
                return Results.Ok(reporte);
            })
            .Produces<List<DTOs.ReporteVentasDTO>>(StatusCodes.Status200OK)
            .WithName("GetReporteVentasPorVendedor")
            .WithTags("Reportes");

            // #NUEVO: Endpoint para generar y descargar el reporte en PDF
            group.MapGet("/ventas-vendedor/{idPersona:int}/pdf", async (int idPersona, ReporteService reporteService, PdfReportService pdfService, PersonaService personaService) =>
            {
                // 1. Obtener los datos del reporte.
                var reporteData = await reporteService.GetVentasPorPersonaUltimos7DiasAsync(idPersona);
                if (reporteData == null || !reporteData.Any())
                {
                    return Results.NotFound("No hay datos para generar el reporte.");
                }

                // 2. Obtener el nombre del vendedor para el título del PDF.
                var persona = await personaService.GetByIdAsync(idPersona);
                var nombreVendedor = persona?.NombreCompleto ?? $"ID {idPersona}";

                // 3. Generar el archivo PDF.
                var pdfBytes = pdfService.GenerateVentasPdf(reporteData, nombreVendedor);

                // 4. Devolver el archivo.
                string fileName = $"ReporteVentas_{nombreVendedor.Replace(" ", "_")}_{System.DateTime.Now:yyyyMMdd}.pdf";
                return Results.File(pdfBytes, "application/pdf", fileName);
            })
            .Produces(StatusCodes.Status200OK, typeof(File), "application/pdf")
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetReporteVentasPorVendedorAsPdf")
            .WithTags("Reportes");
        }
    }
}

