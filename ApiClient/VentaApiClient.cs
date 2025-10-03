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

        public async Task<HttpResponseMessage> CreateCompletaAsync(CrearVentaCompletaDTO dto)
        {
            return await _httpClient.PostAsJsonAsync("api/ventas/completa", dto);
        }

        public async Task<HttpResponseMessage> UpdateCompletaAsync(CrearVentaCompletaDTO dto)
        {
            // Método añadido para actualizar una venta con sus líneas
            return await _httpClient.PutAsJsonAsync($"api/ventas/completa/{dto.IdVenta}", dto);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"api/ventas/{id}");
        }

        public async Task<HttpResponseMessage> FinalizarVentaAsync(int id)
        {
            // Este método ahora podría ser reemplazado por UpdateCompleta, pero lo mantenemos por si se usa en otro lado
            return await _httpClient.PutAsync($"api/ventas/{id}/finalizar", null);
        }
    }
}

