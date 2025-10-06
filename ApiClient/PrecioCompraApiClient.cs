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

        public async Task<List<PrecioCompraDTO>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PrecioCompraDTO>>("api/preciocompra");
        }

        public async Task<PrecioCompraDTO> GetByIdAsync(int idProducto, int idProveedor)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<PrecioCompraDTO>($"api/preciocompra/{idProducto}/{idProveedor}");
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<HttpResponseMessage> CreateAsync(PrecioCompraDTO dto)
        {
            return await _httpClient.PostAsJsonAsync("api/preciocompra", dto);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int idProducto, int idProveedor, PrecioCompraDTO dto)
        {
            return await _httpClient.PutAsJsonAsync($"api/preciocompra/{idProducto}/{idProveedor}", dto);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int idProducto, int idProveedor)
        {
            return await _httpClient.DeleteAsync($"api/preciocompra/{idProducto}/{idProveedor}");
        }
    }
}

