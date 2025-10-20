using DominioServicios;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;

namespace WebAPI.Endpoints
{
    // #NUEVO: Endpoints dedicados para los reportes.
    public static class ReporteEndpoints
    {
        public static void MapReporteEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/reportes").WithTags("Reportes");

            group.MapGet("/ventas-vendedor/{idPersona:int}", async (int idPersona, ReporteService service) =>
            {
                var reporte = await service.GetVentasPorPersonaUltimos7DiasAsync(idPersona);
                return Results.Ok(reporte);
            })
            .Produces<List<ReporteVentasDTO>>(StatusCodes.Status200OK)
            .WithName("GetReporteVentasPorVendedor");
        }
    }
}
