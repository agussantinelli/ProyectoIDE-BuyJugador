using DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    public class PrecioCompraApiClient
    {
        private readonly HttpClient _httpClient;

        public PrecioCompraApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PrecioCompraDTO>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PrecioCompraDTO>>("api/precios-compra");
        }

        public async Task<PrecioCompraDTO?> GetByIdAsync(int idProducto, int idProveedor)
        {
            return await _httpClient.GetFromJsonAsync<PrecioCompraDTO?>(
                $"api/precios-compra/{idProducto}/{idProveedor}");
        }

        public async Task<HttpResponseMessage> CreateOrUpdateAsync(PrecioCompraDTO dto)
        {
            return await _httpClient.PostAsJsonAsync("api/precios-compra", dto);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int idProducto, int idProveedor, PrecioCompraDTO dto)
        {
            return await _httpClient.PutAsJsonAsync(
                $"api/precios-compra/{idProducto}/{idProveedor}", dto);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int idProducto, int idProveedor)
        {
            return await _httpClient.DeleteAsync($"api/precios-compra/{idProducto}/{idProveedor}");
        }
    }
}
