using DominioModelo;
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

        public async Task<List<Producto>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Producto>>("api/productos");
        }

        public async Task<Producto?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Producto>($"api/productos/{id}");
        }

        public async Task<Producto?> CreateAsync(ProductoDTO producto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/productos", producto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Producto>();
        }

        public async Task UpdateAsync(int id, ProductoDTO producto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/productos/{id}", producto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/productos/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
