using DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    public class ProductoProveedorApiClient
    {
        private readonly HttpClient _httpClient;
        public ProductoProveedorApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductoDTO>> GetByProveedorIdAsync(int idProveedor)
        {
            var response = await _httpClient.GetAsync($"api/productos/proveedor/{idProveedor}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ProductoDTO>>();
        }

        public async Task<HttpResponseMessage> CreateAsync(ProductoProveedorDTO dto)
        {
            return await _httpClient.PostAsJsonAsync("api/producto-proveedor", dto);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int idProducto, int idProveedor)
        {
            return await _httpClient.DeleteAsync($"api/producto-proveedor/{idProducto}/{idProveedor}");
        }

        public async Task<HttpResponseMessage> UpdateProductosProveedorAsync(ProductoProveedorDTO dto)
        {
            return await _httpClient.PostAsJsonAsync("api/producto-proveedor", dto);
        }
    }
}

