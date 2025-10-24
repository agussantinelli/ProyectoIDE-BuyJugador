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

        public async Task<LoginResponseDTO?> LoginAsync(int dni, string password)
        {
            var loginRequest = new { Dni = dni, Password = password };
            var response = await _httpClient.PostAsJsonAsync("api/Authentication/login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
            }

            return null;
        }

        public async Task<List<PersonaDTO>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PersonaDTO>>("api/personas");
        }

        public async Task<List<PersonaSimpleDTO>?> GetPersonasActivasParaReporteAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PersonaSimpleDTO>>("api/personas/activos-reporte");
        }

        public async Task<List<PersonaDTO>?> GetInactivosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PersonaDTO>>("api/personas/inactivos");
        }

        public async Task<PersonaDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PersonaDTO?>($"api/personas/{id}");
        }

        public async Task<HttpResponseMessage> CreateAsync(PersonaDTO persona)
        {
            return await _httpClient.PostAsJsonAsync("api/personas", persona);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int id, PersonaDTO persona)
        {
            return await _httpClient.PutAsJsonAsync($"api/personas/{id}", persona);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"api/personas/{id}");
        }

        public async Task<HttpResponseMessage> ReactivarAsync(int id)
        {
            return await _httpClient.PutAsync($"api/personas/reactivar/{id}", null);
        }
    }
}

