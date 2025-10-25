using DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    public class LocalidadApiClient
    {
        private readonly HttpClient _httpClient;

        public LocalidadApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<LocalidadDTO>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<LocalidadDTO>>("api/localidades");
        }

        public async Task<LocalidadDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<LocalidadDTO>($"api/localidades/{id}");
        }

        public async Task<LocalidadDTO?> CreateAsync(LocalidadDTO localidad)
        {
            var response = await _httpClient.PostAsJsonAsync("api/localidades", localidad);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<LocalidadDTO>();
        }

        public async Task<List<LocalidadDTO>?> GetAllOrderedAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<LocalidadDTO>>("api/localidades/ordenadas");
        }
    }
}
