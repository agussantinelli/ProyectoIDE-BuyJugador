using DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ApiClient
{
    public class PedidoApiClient
    {
        private readonly HttpClient _httpClient;

        public PedidoApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PedidoDTO>?> GetPedidosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PedidoDTO>>("api/pedidos");
        }

        public async Task<PedidoDTO> CreatePedidoCompletoAsync(CrearPedidoCompletoDTO pedido)
        {
            var response = await _httpClient.PostAsJsonAsync("api/pedidos/completo", pedido);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PedidoDTO>();
        }

        public async Task<HttpResponseMessage> MarcarComoRecibidoAsync(int id)
        {
            return await _httpClient.PutAsync($"api/pedidos/recibir/{id}", null);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"api/pedidos/{id}");
        }
    }
}

