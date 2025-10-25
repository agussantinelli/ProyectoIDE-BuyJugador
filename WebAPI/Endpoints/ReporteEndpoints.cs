using DominioServicios;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Endpoints
{
    public static class ReporteEndpoints
    {
        public static void MapReporteEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/reportes");

            group.MapGet("/ventas-vendedor/{idPersona:int}", async ([FromServices] ReporteService service, int idPersona) =>
            {
                var reporte = await service.GetVentasPorPersonaUltimos7DiasAsync(idPersona);
                return Results.Ok(reporte);
            })
            .Produces<List<ReporteVentasDTO>>(StatusCodes.Status200OK)
            .WithName("GetReporteVentasPorVendedor")
            .WithTags("Reportes");

            group.MapGet("/ventas-vendedor/{idPersona:int}/pdf", async (
                int idPersona,
                [FromServices] ReporteService reporteService) =>
            {
                var pdfBytes = await reporteService.GetVentasVendedorPdfAsync(idPersona);
                if (pdfBytes == null)
                    return Results.NotFound("No hay datos para generar el reporte.");

                var fileName = $"Reporte_Ventas_Vendedor_{idPersona}_{DateTime.Now:dd-MM-yyyy}.pdf";
                return Results.File(pdfBytes, "application/pdf", fileName);
            })
            .Produces(StatusCodes.Status200OK, typeof(FileContentResult))
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetReporteVentasPorVendedorAsPdf")
            .WithTags("Reportes");

            group.MapGet("/historial-precios.png",
            async Task<IResult> ([FromQuery] DateTime? from,
                                 [FromQuery] DateTime? to,
                                 [FromQuery] int w,
                                 [FromQuery] int h,
                                 [FromServices] ReporteService service) =>
            {
                var png = await service.GetHistorialPreciosPngAsync(from, to, w, h);
                if (png is null) return Results.NotFound("Sin datos");
                return Results.File(png, "image/png");
            })
            .WithName("GetHistorialPreciosPng")
            .WithTags("Reportes");

            group.MapGet("/historial-precios.pdf",
            async Task<IResult> ([FromQuery] DateTime? from,
                                 [FromQuery] DateTime? to,
                                 [FromQuery] int w,
                                 [FromQuery] int h,
                                 [FromServices] ReporteService service) =>
            {
                var pdfBytes = await service.GetHistorialPreciosPdfAsync(from, to, w, h);
                if (pdfBytes is null) return Results.NotFound("Sin datos");
                var fileName = $"Historial_de_precios_{DateTime.Now:dd-MM-yyyy_HH.mm}.pdf";
                return Results.File(pdfBytes, "application/pdf", fileName);
            })
            .WithName("GetHistorialPreciosPdf")
            .WithTags("Reportes");
        }
    }
}
