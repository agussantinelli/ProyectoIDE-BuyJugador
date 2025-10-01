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
            return await _httpClient.GetFromJsonAsync<List<LineaVentaDTO>>("api/lineasventa");
        }

        public async Task<LineaVentaDTO?> GetByIdAsync(int nroLineaVenta)
        {
            return await _httpClient.GetFromJsonAsync<LineaVentaDTO?>($"api/lineasventa/{nroLineaVenta}");
        }

        public async Task<HttpResponseMessage> CreateAsync(LineaVentaDTO dto)
        {
            return await _httpClient.PostAsJsonAsync("api/lineasventa", dto);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int nroLineaVenta, LineaVentaDTO dto)
        {
            return await _httpClient.PutAsJsonAsync($"api/lineasventa/{nroLineaVenta}", dto);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int nroLineaVenta)
        {
            return await _httpClient.DeleteAsync($"api/lineasventa/{nroLineaVenta}");
        }

        public async Task<List<LineaVentaDTO>?> GetByVentaIdAsync(int idVenta)
        {
            return await _httpClient.GetFromJsonAsync<List<LineaVentaDTO>>($"api/lineaventas/porventa/{idVenta}");
        }

    }
}
