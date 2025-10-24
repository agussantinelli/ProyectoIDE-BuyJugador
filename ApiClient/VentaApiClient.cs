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
            => await _httpClient.GetFromJsonAsync<List<VentaDTO>>("api/ventas");

        public async Task<VentaDTO?> GetByIdAsync(int id)
            => await _httpClient.GetFromJsonAsync<VentaDTO?>($"api/ventas/{id}");

        public async Task<List<VentaDTO>?> GetByPersonaAsync(int idPersona)
            => await _httpClient.GetFromJsonAsync<List<VentaDTO>>($"api/ventas/persona/{idPersona}");

        public async Task<HttpResponseMessage> CreateCompletaAsync(CrearVentaCompletaDTO dto)
            => await _httpClient.PostAsJsonAsync("api/ventas/completa", dto);

        public async Task<HttpResponseMessage> UpdateCompletaAsync(CrearVentaCompletaDTO dto)
            => await _httpClient.PutAsJsonAsync($"api/ventas/completa/{dto.IdVenta}", dto);

        public async Task<HttpResponseMessage> DeleteAsync(int id)
            => await _httpClient.DeleteAsync($"api/ventas/{id}");

        public async Task<HttpResponseMessage> FinalizarVentaAsync(int id)
            => await _httpClient.PutAsync($"api/ventas/{id}/finalizar", null);

        public async Task<decimal> GetTotalVentasHoyAsync()
        {
            return await _httpClient.GetFromJsonAsync<decimal?>("api/ventas/total-hoy") ?? 0;
        }

    }
}
