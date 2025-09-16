using DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    public class TipoProductoApiClient
    {
        private readonly HttpClient _httpClient;

        public TipoProductoApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TipoProductoDTO>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TipoProductoDTO>>("api/tiposproducto");
        }

        public async Task<TipoProductoDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<TipoProductoDTO>($"api/tiposproducto/{id}");
        }

        public async Task<TipoProductoDTO?> CreateAsync(TipoProductoDTO tipoProducto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/tiposproducto", tipoProducto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TipoProductoDTO>();
        }

        public async Task UpdateAsync(int id, TipoProductoDTO tipoProducto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/tiposproducto/{id}", tipoProducto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/tiposproducto/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                throw new InvalidOperationException("No se puede eliminar el tipo de producto porque tiene productos asociados.");
            }
            response.EnsureSuccessStatusCode();
        }
    }
}
