using DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    public class LineaVentaApiClient
    {
        private readonly HttpClient _httpClient;

        public LineaVentaApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<LineaVentaDTO>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<LineaVentaDTO>>("api/lineaventas");
        }

        public async Task<List<LineaVentaDTO>?> GetLineasByVentaIdAsync(int idVenta)
        {
            return await _httpClient.GetFromJsonAsync<List<LineaVentaDTO>>($"api/lineaventas/porventa/{idVenta}");
        }

        public async Task<LineaVentaDTO?> GetByIdAsync(int idVenta, int nroLinea)
        {
            return await _httpClient.GetFromJsonAsync<LineaVentaDTO?>($"api/lineaventas/{idVenta}/{nroLinea}");
        }

        public async Task<HttpResponseMessage> CreateAsync(LineaVentaDTO dto)
        {
            return await _httpClient.PostAsJsonAsync("api/lineaventas", dto);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int idVenta, int nroLinea, LineaVentaDTO dto)
        {
            return await _httpClient.PutAsJsonAsync($"api/lineaventas/{idVenta}/{nroLinea}", dto);
        }

        public async Task<HttpResponseMessage> UpdateCantidadAsync(int idVenta, int nroLinea, int nuevaCantidad)
        {
            return await _httpClient.PutAsJsonAsync($"api/lineaventas/{idVenta}/{nroLinea}/cantidad", nuevaCantidad);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int idVenta, int nroLinea)
        {
            return await _httpClient.DeleteAsync($"api/lineaventas/{idVenta}/{nroLinea}");
        }
    }
}
