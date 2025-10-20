using DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    // #NUEVO: ApiClient para consumir los endpoints de reportes.
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
    }
}
