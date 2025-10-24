using DTOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    public class ReporteApiClient
    {
        private readonly HttpClient _httpClient;

        public ReporteApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ReporteVentasDTO>?> GetReporteVentasPorVendedorAsync(int idPersona)
        {
            var resp = await _httpClient.GetAsync($"api/reportes/ventas-vendedor/{idPersona}");

            if (resp.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null;
            }

            if (!resp.IsSuccessStatusCode)
            {
                return new List<ReporteVentasDTO>();
            }

            return await resp.Content.ReadFromJsonAsync<List<ReporteVentasDTO>>() ?? new List<ReporteVentasDTO>();
        }

        public async Task<byte[]?> GetReporteVentasPdfAsync(int idPersona)
        {
            var resp = await _httpClient.GetAsync($"api/reportes/ventas-vendedor/{idPersona}/pdf");
            if (resp.IsSuccessStatusCode)
            {
                return await resp.Content.ReadAsByteArrayAsync();
            }

            return null;
        }

        public async Task<byte[]?> GetHistorialPreciosPdfAsync(DateTime? from = null, DateTime? to = null, int w = 1200, int h = 500)
        {
            var fromStr = from.HasValue ? from.Value.ToString("yyyy-MM-dd") : string.Empty;
            var toStr = to.HasValue ? to.Value.ToString("yyyy-MM-dd") : string.Empty;

            var url = $"api/precios-venta/historial.pdf?from={fromStr}&to={toStr}&w={w}&h={h}";
            var resp = await _httpClient.GetAsync(url);

            if (resp.IsSuccessStatusCode)
            {
                return await resp.Content.ReadAsByteArrayAsync();
            }

            return null;
        }
    }
}
