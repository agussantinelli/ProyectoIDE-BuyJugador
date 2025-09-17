using DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    public class PersonaApiClient
    {
        private readonly HttpClient _httpClient;

        public PersonaApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PersonaDTO>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PersonaDTO>>("api/personas");
        }

        public async Task<PersonaDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PersonaDTO>($"api/personas/{id}");
        }

        public async Task<PersonaDTO?> CreateAsync(PersonaDTO persona)
        {
            var response = await _httpClient.PostAsJsonAsync("api/personas", persona);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PersonaDTO>();
            }
            return null;
        }

        public async Task<bool> UpdateAsync(int id, PersonaDTO persona)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/personas/{id}", persona);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/personas/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
