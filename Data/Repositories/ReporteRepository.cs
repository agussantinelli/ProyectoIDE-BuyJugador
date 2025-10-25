using DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Data;

namespace Data.Repositories
{
    public class ReporteRepository
    {
        private readonly BuyJugadorContext _context;
        private readonly string _connectionString;

        public ReporteRepository(BuyJugadorContext context)
        {
            _context = context;
            _connectionString = _context.Database.GetConnectionString()
                               ?? throw new InvalidOperationException("Connection string no disponible.");
        }

        public async Task<List<ReporteVentasDTO>> GetVentasPorPersonaUltimos7DiasAsync(
            int idPersona,
            CancellationToken ct = default)
        {
            var reportes = new List<ReporteVentasDTO>();

            var tzId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "Argentina Standard Time"
                : "America/Argentina/Buenos_Aires";
            var tz = TimeZoneInfo.FindSystemTimeZoneById(tzId);
            var fechaDesde = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz).AddDays(-7);

            const string sql = @"
                SELECT
                    v.IdVenta,
                    v.Fecha,
                    p.NombreCompleto,
                    v.Estado,
                    SUM(lv.Cantidad * lv.PrecioUnitario) AS TotalVenta
                FROM Ventas v
                JOIN Personas p ON v.IdPersona = p.IdPersona
                JOIN LineaVentas lv ON v.IdVenta = lv.IdVenta
                WHERE v.IdPersona = @IdPersona AND v.Fecha >= @FechaDesde
                GROUP BY v.IdVenta, v.Fecha, p.NombreCompleto, v.Estado
                ORDER BY v.Fecha DESC;";

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(ct);

            await using var command = new SqlCommand(sql, connection);

            var pId = command.Parameters.Add("@IdPersona", SqlDbType.Int);
            pId.Value = idPersona;

            var pFecha = command.Parameters.Add("@FechaDesde", SqlDbType.DateTime2);
            pFecha.Value = fechaDesde; 

            await using var reader = await command.ExecuteReaderAsync(ct);

            int ordIdVenta = reader.GetOrdinal("IdVenta");
            int ordFecha = reader.GetOrdinal("Fecha");
            int ordNombre = reader.GetOrdinal("NombreCompleto");
            int ordEstado = reader.GetOrdinal("Estado");
            int ordTotal = reader.GetOrdinal("TotalVenta");

            while (await reader.ReadAsync(ct))
            {
                reportes.Add(new ReporteVentasDTO
                {
                    IdVenta = reader.GetInt32(ordIdVenta),
                    Fecha = reader.GetDateTime(ordFecha),
                    NombreVendedor = reader.GetString(ordNombre),
                    Estado = reader.GetString(ordEstado),
                    TotalVenta = reader.GetDecimal(ordTotal)
                });
            }

            return reportes;
        }
    }
}
