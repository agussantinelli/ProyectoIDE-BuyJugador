using DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    public class ProductoApiClient
    {
        private readonly HttpClient _httpClient;

        public ProductoApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductoDTO>?> GetProductosAsync()
        {
            return await GetAllAsync();
        }

        public async Task<List<ProductoDTO>?> GetProductosByProveedorIdAsync(int idProveedor)
        {
            return await _httpClient.GetFromJsonAsync<List<ProductoDTO>>($"api/producto-proveedor/{idProveedor}");
        }

        public async Task<List<ProductoDTO>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductoDTO>>("api/productos");
        }

        public async Task<ProductoDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProductoDTO?>($"api/productos/{id}");
        }

        public async Task<HttpResponseMessage> CreateAsync(ProductoDTO dto)
        {
            return await _httpClient.PostAsJsonAsync("api/productos", dto);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int id, ProductoDTO dto)
        {
            return await _httpClient.PutAsJsonAsync($"api/productos/{id}", dto);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"api/productos/{id}");
        }

        public async Task<List<ProductoDTO>?> GetAllInactivosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductoDTO>>("api/productos/inactivos");
        }

        public async Task<HttpResponseMessage> ReactivarAsync(int id)
        {
            return await _httpClient.PutAsync($"api/productos/{id}/reactivar", null);
        }
    }
}

