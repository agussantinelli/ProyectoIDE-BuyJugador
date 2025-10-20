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

        public async Task<List<PedidoDTO>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PedidoDTO>>("api/pedidos");
        }

        public async Task<PedidoDTO> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PedidoDTO>($"api/pedidos/{id}");
        }

        public async Task<HttpResponseMessage> CreateAsync(CrearPedidoCompletoDTO dto)
        {
            return await _httpClient.PostAsJsonAsync("api/pedidos/completo", dto);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"api/pedidos/{id}");
        }

        public async Task<HttpResponseMessage> MarcarComoRecibidoAsync(int id)
        {
            return await _httpClient.PutAsync($"api/pedidos/recibir/{id}", null);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int id, PedidoDTO pedido)
        {
            return await _httpClient.PutAsJsonAsync($"api/pedidos/{id}", pedido);
        }

        public async Task<int> GetCantidadPedidosPendientesAsync()
        {
            return await _httpClient.GetFromJsonAsync<int?>("api/pedidos/cantidad-pendientes") ?? 0;
        }
    }
}

