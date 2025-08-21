using DTOs;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ApiClient
{
    public class TipoProductoApiClient
    {
        private static readonly HttpClient client = new HttpClient();

        static TipoProductoApiClient()
        {
            client.BaseAddress = new Uri("https://localhost:7145/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<List<TipoProductoDto>> GetTiposProductoAsync()
        {
            HttpResponseMessage response = await client.GetAsync("tiposproducto");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<TipoProductoDto>>();
        }

        public static async Task<TipoProductoDto> GetTipoProductoAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"tiposproducto/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TipoProductoDto>();
            }
            return null;
        }

        public static async Task AddTipoProductoAsync(TipoProductoDto tipoProducto)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("tiposproducto", tipoProducto);
            response.EnsureSuccessStatusCode();
        }

        public static async Task UpdateTipoProductoAsync(TipoProductoDto tipoProducto)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"tiposproducto/{tipoProducto.IdTipoProducto}", tipoProducto);
            response.EnsureSuccessStatusCode();
        }

        public static async Task DeleteTipoProductoAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"tiposproducto/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}