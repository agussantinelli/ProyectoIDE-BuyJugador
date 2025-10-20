using DTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class ReporteService
    {
        private readonly string _connectionString;

        public ReporteService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BuyJugadorConnection");
        }

        public async Task<List<ReporteVentasDTO>> GetVentasPorPersonaUltimos7DiasAsync(int idPersona)
        {
            var reportes = new List<ReporteVentasDTO>();
            var fechaDesde = DateTime.UtcNow.AddDays(-7);

            // #CORRECCIÓN: Se utilizan los nombres de tabla correctos y pluralizados (Ventas, Personas, LineaVentas).
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

