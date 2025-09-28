using DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    public class PedidoApiClient
    {
        private readonly HttpClient _httpClient;

        public PedidoApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PedidoDTO>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PedidoDTO>>("api/pedidos");
        }

        public async Task<PedidoDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PedidoDTO?>($"api/pedidos/{id}");
        }

        public async Task<HttpResponseMessage> CreateAsync(PedidoDTO dto)
        {
            return await _httpClient.PostAsJsonAsync("api/pedidos", dto);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int id, PedidoDTO dto)
        {
            return await _httpClient.PutAsJsonAsync($"api/pedidos/{id}", dto);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"api/pedidos/{id}");
        }
    }
}
