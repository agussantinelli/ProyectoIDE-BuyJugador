using DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    public class LineaPedidoApiClient
    {
        private readonly HttpClient _httpClient;

        public LineaPedidoApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<LineaPedidoDTO>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<LineaPedidoDTO>>("api/lineaspedido");
        }

        public async Task<LineaPedidoDTO?> GetByIdAsync(int nroLineaPedido)
        {
            return await _httpClient.GetFromJsonAsync<LineaPedidoDTO?>($"api/lineaspedido/{nroLineaPedido}");
        }

        public async Task<HttpResponseMessage> CreateAsync(LineaPedidoDTO dto)
        {
            return await _httpClient.PostAsJsonAsync("api/lineaspedido", dto);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int nroLineaPedido, LineaPedidoDTO dto)
        {
            return await _httpClient.PutAsJsonAsync($"api/lineaspedido/{nroLineaPedido}", dto);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int nroLineaPedido)
        {
            return await _httpClient.DeleteAsync($"api/lineaspedido/{nroLineaPedido}");
        }
    }
}
