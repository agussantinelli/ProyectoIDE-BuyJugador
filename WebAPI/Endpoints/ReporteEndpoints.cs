using DominioServicios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Endpoints
{
    public static class ReporteEndpoints
    {
        public static void MapReporteEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/reportes").RequireAuthorization();

            group.MapGet("/ventas-vendedor/{idPersona:int}", async ([FromServices] ReporteService service, int idPersona) =>
            {
                var reporte = await service.GetVentasPorPersonaUltimos7DiasAsync(idPersona);
                return Results.Ok(reporte);
            })
            .Produces<List<DTOs.ReporteVentasDTO>>(StatusCodes.Status200OK)
            .WithName("GetReporteVentasPorVendedor")
            .WithTags("Reportes");

            group.MapGet("/ventas-vendedor/{idPersona:int}/pdf", async (
                int idPersona,
                [FromServices] ReporteService reporteService,
                [FromServices] PdfReportService pdfService,
                [FromServices] PersonaService personaService) =>
            {
                var reporteData = await reporteService.GetVentasPorPersonaUltimos7DiasAsync(idPersona);
                if (reporteData == null || !reporteData.Any())
                {
                    return Results.NotFound("No hay datos para generar el reporte.");
                }

                var persona = await personaService.GetByIdAsync(idPersona);
                var nombreVendedor = persona?.NombreCompleto ?? $"ID {idPersona}";

                var pdfBytes = pdfService.GenerateVentasPdf(reporteData, nombreVendedor);

                var fileName = $"Reporte {nombreVendedor} {System.DateTime.Now:dd-MM-yyyy}.pdf";
                return Results.File(pdfBytes, "application/pdf", fileName);
            })
            .Produces(StatusCodes.Status200OK, typeof(FileContentResult))
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetReporteVentasPorVendedorAsPdf")
            .WithTags("Reportes");
        }
    }
}

