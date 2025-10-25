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
        private readonly HttpClient _http;

        public ReporteApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ReporteVentasDTO>?> GetReporteVentasPorVendedorAsync(int idPersona)
        {
            var resp = await _http.GetAsync($"api/reportes/ventas-vendedor/{idPersona}");
            if (resp.StatusCode == HttpStatusCode.Unauthorized) return null;
            if (!resp.IsSuccessStatusCode) return new List<ReporteVentasDTO>();
            return await resp.Content.ReadFromJsonAsync<List<ReporteVentasDTO>>() ?? new();
        }

        public async Task<byte[]?> GetReporteVentasPdfAsync(int idPersona)
        {
            var resp = await _http.GetAsync($"api/reportes/ventas-vendedor/{idPersona}/pdf");
            return resp.IsSuccessStatusCode ? await resp.Content.ReadAsByteArrayAsync() : null;
        }

        public async Task<byte[]?> GetHistorialPreciosPdfAsync(DateTime? from = null, DateTime? to = null, int w = 1200, int h = 500)
        {
            var parts = new List<string>();
            if (from.HasValue) parts.Add($"from={from.Value:yyyy-MM-dd}");
            if (to.HasValue) parts.Add($"to={to.Value:yyyy-MM-dd}");
            parts.Add($"w={w}");
            parts.Add($"h={h}");

            var url = $"api/reportes/historial-precios.pdf?{string.Join("&", parts)}";
            var resp = await _http.GetAsync(url);
            return resp.IsSuccessStatusCode ? await resp.Content.ReadAsByteArrayAsync() : null;
        }

        public async Task<byte[]?> GetHistorialPreciosPngAsync(DateTime? from = null, DateTime? to = null, int w = 1200, int h = 500)
        {
            var parts = new List<string>();
            if (from.HasValue) parts.Add($"from={from.Value:yyyy-MM-dd}");
            if (to.HasValue) parts.Add($"to={to.Value:yyyy-MM-dd}");
            parts.Add($"w={w}");
            parts.Add($"h={h}");

            var url = $"api/reportes/historial-precios.png?{string.Join("&", parts)}";
            var resp = await _http.GetAsync(url);
            return resp.IsSuccessStatusCode ? await resp.Content.ReadAsByteArrayAsync() : null;
        }

    }
}
