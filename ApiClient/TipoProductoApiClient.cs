using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DominioModelo;

namespace ApiClient
{
    // Esta clase maneja toda la comunicación con los endpoints de tipos de producto de la API.
    public class TipoProductoApiClient
    {
        private readonly HttpClient _httpClient;

        public TipoProductoApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Obtiene una lista de todos los tipos de producto desde la API.
        public async Task<List<TipoProducto>?> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/tiposproducto");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<TipoProducto>>();
        }

        // Obtiene un tipo de producto por su ID.
        public async Task<TipoProducto?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/tiposproducto/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TipoProducto>();
        }

        // Crea un nuevo tipo de producto en la API.
        public async Task<TipoProducto?> CreateAsync(TipoProducto tipoProducto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/tiposproducto", tipoProducto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TipoProducto>();
        }

        // Actualiza un tipo de producto existente.
        public async Task UpdateAsync(int id, TipoProducto tipoProducto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/tiposproducto/{id}", tipoProducto);
            response.EnsureSuccessStatusCode();
        }

        // Elimina un tipo de producto por su ID.
        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/tiposproducto/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
