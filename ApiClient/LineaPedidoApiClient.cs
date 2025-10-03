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

        public async Task<List<LineaPedidoDTO>?> GetLineasByPedidoIdAsync(int idPedido)
        {
            return await _httpClient.GetFromJsonAsync<List<LineaPedidoDTO>>($"api/lineapedidos/porpedido/{idPedido}");
        }
    }
}
