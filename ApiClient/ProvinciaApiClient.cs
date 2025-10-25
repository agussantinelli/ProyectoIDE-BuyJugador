using DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    public class ProvinciaApiClient
    {
        private readonly HttpClient _httpClient;

        public ProvinciaApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProvinciaDTO>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProvinciaDTO>>("api/provincias");
        }

        public async Task<ProvinciaDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProvinciaDTO>($"api/provincias/{id}");
        }

        public async Task<ProvinciaDTO?> CreateAsync(ProvinciaDTO provincia)
        {
            var response = await _httpClient.PostAsJsonAsync("api/provincias", provincia);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProvinciaDTO>();
        }
    }
}
