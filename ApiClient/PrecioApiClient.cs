using DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    public class PrecioApiClient
    {
        private readonly HttpClient _httpClient;

        public PrecioApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PrecioDTO>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PrecioDTO>>("api/precios");
        }

        public async Task<PrecioDTO?> GetByIdAsync(int idProducto)
        {
            return await _httpClient.GetFromJsonAsync<PrecioDTO?>($"api/precios/{idProducto}");
        }

        public async Task<HttpResponseMessage> CreateAsync(PrecioDTO dto)
        {
            return await _httpClient.PostAsJsonAsync("api/precios", dto);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int idProducto, PrecioDTO dto)
        {
            return await _httpClient.PutAsJsonAsync($"api/precios/{idProducto}", dto);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int idProducto)
        {
            return await _httpClient.DeleteAsync($"api/precios/{idProducto}");
        }
    }
}
