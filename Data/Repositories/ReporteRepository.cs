using DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ReporteRepository
    {
        private readonly BuyJugadorContext _context;
        private readonly string _connectionString;

        public ReporteRepository(BuyJugadorContext context)
        {
            _context = context;
            _connectionString = _context.Database.GetConnectionString()!;
        }

        public async Task<List<ReporteVentasDTO>> GetVentasPorPersonaUltimos7DiasAsync(int idPersona)
        {
            var reportes = new List<ReporteVentasDTO>();

            var tzId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "Argentina Standard Time"
                : "America/Argentina/Buenos_Aires";
            var tz = TimeZoneInfo.FindSystemTimeZoneById(tzId);
            var ahoraAr = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
            var fechaDesde = ahoraAr.AddDays(-7);

            const string query = @"
                SELECT
                    v.IdVenta,
                    v.Fecha,
                    p.NombreCompleto,
                    v.Estado,
                    SUM(lv.Cantidad * lv.PrecioUnitario) as TotalVenta
                FROM Ventas v
                JOIN Personas p ON v.IdPersona = p.IdPersona
                JOIN LineaVentas lv ON v.IdVenta = lv.IdVenta
                WHERE v.IdPersona = @IdPersona AND v.Fecha >= @FechaDesde
                GROUP BY v.IdVenta, v.Fecha, p.NombreCompleto, v.Estado
                ORDER BY v.Fecha DESC;";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdPersona", idPersona);
                    command.Parameters.AddWithValue("@FechaDesde", fechaDesde);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            reportes.Add(new ReporteVentasDTO
                            {
                                IdVenta = reader.GetInt32(reader.GetOrdinal("IdVenta")),
                                Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha")),
                                NombreVendedor = reader.GetString(reader.GetOrdinal("NombreCompleto")),
                                Estado = reader.GetString(reader.GetOrdinal("Estado")),
                                TotalVenta = reader.GetDecimal(reader.GetOrdinal("TotalVenta"))
                            });
                        }
                    }
                }
            }
            return reportes;
        }
    }
}
