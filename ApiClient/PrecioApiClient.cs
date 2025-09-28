using DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;

namespace ApiClient
{
    public class PrecioApiClient
    {
        private readonly HttpClient _httpClient;

        public PrecioApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PrecioDTO>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PrecioDTO>>("api/precios");
        }

        public async Task<PrecioDTO?> GetByIdAsync(int idProducto, DateTime fechaDesde)
        {
            string fechaParam = fechaDesde.ToString("yyyy-MM-ddTHH:mm:ss");
            return await _httpClient.GetFromJsonAsync<PrecioDTO?>($"api/precios/{idProducto}/{fechaParam}");
        }

        public async Task<HttpResponseMessage> CreateAsync(PrecioDTO dto)
        {
            return await _httpClient.PostAsJsonAsync("api/precios", dto);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int idProducto, DateTime fechaDesde, PrecioDTO dto)
        {
            string fechaParam = fechaDesde.ToString("yyyy-MM-ddTHH:mm:ss");
            return await _httpClient.PutAsJsonAsync($"api/precios/{idProducto}/{fechaParam}", dto);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int idProducto, DateTime fechaDesde)
        {
            string fechaParam = fechaDesde.ToString("yyyy-MM-ddTHH:mm:ss");
            return await _httpClient.DeleteAsync($"api/precios/{idProducto}/{fechaParam}");
        }
    }
}
