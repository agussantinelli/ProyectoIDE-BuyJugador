using DTOs;
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

        public async Task<HttpResponseMessage> UpdateProductosProveedorAsync(ProductoProveedorDTO dto)
        {
            return await _httpClient.PostAsJsonAsync("api/producto-proveedor", dto);
        }
    }
}
