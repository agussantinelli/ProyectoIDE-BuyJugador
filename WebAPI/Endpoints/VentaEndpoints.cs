using Data;
using DominioServicios;
using DTOs;
using System;

namespace WebAPI.Endpoints
{
    public static class VentaEndpoints
    {
        public static void MapVentaEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/ventas");

            group.MapGet("/", async (VentaService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            });

            group.MapGet("/{id}", async (int id, VentaService service) =>
            {
                var venta = await service.GetByIdAsync(id);
                return venta is not null ? Results.Ok(venta) : Results.NotFound();
            });

            group.MapPost("/completa", async (CrearVentaCompletaDTO dto, VentaService service) =>
            {
                try
                {
                    var nuevaVenta = await service.CreateVentaCompletaAsync(dto);
                    return Results.Created($"/api/ventas/{nuevaVenta.IdVenta}", nuevaVenta);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { message = ex.Message });
                }
            });

            group.MapPut("/{id}", async (int id, VentaDTO dto, VentaService service) =>
            {
                await service.UpdateAsync(id, dto);
                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (int id, VentaService service) =>
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            });
            app.MapPut("api/ventas/finalizar/{idVenta}", async (BuyJugadorContext db, int idVenta) =>
            {
                var venta = await db.Ventas.FindAsync(idVenta);
                if (venta == null) return Results.NotFound("Venta no encontrada.");

                if (venta.Estado == "Finalizada")
                    return Results.BadRequest("La venta ya está finalizada.");

                venta.Estado = "Finalizada";
                await db.SaveChangesAsync();

                return Results.Ok();
            });

        }
    }
}

