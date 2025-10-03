using DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    public class VentaApiClient
    {
        private readonly HttpClient _httpClient;

        public VentaApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<VentaDTO>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<VentaDTO>>("api/ventas");
        }

        public async Task<VentaDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<VentaDTO?>($"api/ventas/{id}");
        }

        public async Task<HttpResponseMessage> CreateAsync(VentaDTO dto)
        {
            return await _httpClient.PostAsJsonAsync("api/ventas", dto);
        }

        public async Task<HttpResponseMessage> CreateCompletaAsync(CrearVentaCompletaDTO dto)
        {
            return await _httpClient.PostAsJsonAsync("api/ventas/completa", dto);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int id, VentaDTO dto)
        {
            return await _httpClient.PutAsJsonAsync($"api/ventas/{id}", dto);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"api/ventas/{id}");
        }

        // Método agregado para finalizar una venta a través de la API.
        public async Task<HttpResponseMessage> FinalizarVentaAsync(int id)
        {
            // Se usa un POST a un endpoint de acción específico. El contenido es nulo.
            return await _httpClient.PostAsync($"api/ventas/{id}/finalizar", null);
        }
    }
}

