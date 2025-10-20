using DTOs;
using System.Collections.Generic;
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
            return await _httpClient.GetFromJsonAsync<List<ReporteVentasDTO>>($"api/reportes/ventas-vendedor/{idPersona}");
        }

        // #NUEVO: Método para descargar el PDF como un array de bytes.
        public async Task<byte[]?> GetReporteVentasPdfAsync(int idPersona)
        {
            var response = await _httpClient.GetAsync($"api/reportes/ventas-vendedor/{idPersona}/pdf");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }
            return null;
        }
    }
}

