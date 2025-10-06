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

        public async Task<List<PedidoDTO>> GetPedidosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PedidoDTO>>("api/pedidos");
        }

        public async Task<PedidoDTO> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PedidoDTO>($"api/pedidos/{id}");
        }

        public async Task<PedidoDTO> CreatePedidoCompletoAsync(CrearPedidoCompletoDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/pedidos/completo", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PedidoDTO>();
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"api/pedidos/{id}");
        }

        public async Task<HttpResponseMessage> MarcarComoRecibidoAsync(int id)
        {
            return await _httpClient.PostAsync($"api/pedidos/{id}/recibir", null);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int id, PedidoDTO pedido)
        {
            return await _httpClient.PutAsJsonAsync($"api/pedidos/{id}", pedido);
        }
    }
}
