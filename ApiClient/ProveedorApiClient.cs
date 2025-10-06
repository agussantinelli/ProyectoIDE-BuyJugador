using DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    public class ProveedorApiClient
    {
        private readonly HttpClient _httpClient;

        public ProveedorApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProveedorDTO>?> GetProveedoresAsync()
        {
            return await GetAllAsync();
        }

        public async Task<List<ProveedorDTO>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProveedorDTO>>("api/proveedores");
        }

        public async Task<ProveedorDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProveedorDTO?>($"api/proveedores/{id}");
        }

        public async Task<HttpResponseMessage> CreateAsync(ProveedorDTO dto)
        {
            return await _httpClient.PostAsJsonAsync("api/proveedores", dto);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int id, ProveedorDTO dto)
        {
            return await _httpClient.PutAsJsonAsync($"api/proveedores/{id}", dto);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"api/proveedores/{id}");
        }

        public async Task<List<ProveedorDTO>?> GetInactivosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProveedorDTO>>("api/proveedores/inactivos");
        }

        public async Task<HttpResponseMessage> ReactivarAsync(int id)
        {
            return await _httpClient.PutAsync($"api/proveedores/{id}/reactivar", null);
        }
    }
}

