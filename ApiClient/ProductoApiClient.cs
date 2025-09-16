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

        // Corregido: Debe devolver una lista de ProductoDTO, no el modelo de dominio.
        public async Task<List<ProductoDTO>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductoDTO>>("api/productos");
        }

        public async Task<ProductoDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProductoDTO>($"api/productos/{id}");
        }

        public async Task<ProductoDTO?> CreateAsync(ProductoDTO producto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/productos", producto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ProductoDTO>();
            }
            return null;
        }

        public async Task<bool> UpdateAsync(int id, ProductoDTO producto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/productos/{id}", producto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/productos/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
