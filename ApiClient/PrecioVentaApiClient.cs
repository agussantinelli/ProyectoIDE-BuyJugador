using DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    public class PrecioVentaApiClient
    {
        private readonly HttpClient _httpClient;

        public PrecioVentaApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // # (NUEVO) Consume el endpoint del historial de precios.
        public async Task<List<HistorialPrecioProductoDTO>?> GetHistorialAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<HistorialPrecioProductoDTO>>("api/precios-venta/historial");
        }

        public async Task<PrecioVentaDTO> GetPrecioVigenteAsync(int idProducto)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<PrecioVentaDTO>($"api/precios-venta/vigente/{idProducto}");
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<List<PrecioVentaDTO>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PrecioVentaDTO>>("api/precios-venta");
        }

        public async Task<PrecioVentaDTO?> GetByIdAsync(int idProducto, DateTime fechaDesde)
        {
            string fechaParam = fechaDesde.ToString("yyyy-MM-ddTHH:mm:ss");
            return await _httpClient.GetFromJsonAsync<PrecioVentaDTO?>($"api/precios-venta/{idProducto}/{fechaParam}");
        }

        public async Task<HttpResponseMessage> CreateAsync(PrecioVentaDTO dto)
        {
            return await _httpClient.PostAsJsonAsync("api/precios-venta", dto);
        }

        public async Task<HttpResponseMessage> UpdateAsync(int idProducto, DateTime fechaDesde, PrecioVentaDTO dto)
        {
            string fechaParam = fechaDesde.ToString("yyyy-MM-ddTHH:mm:ss");
            return await _httpClient.PutAsJsonAsync($"api/precios-venta/{idProducto}/{fechaParam}", dto);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int idProducto, DateTime fechaDesde)
        {
            string fechaParam = fechaDesde.ToString("yyyy-MM-ddTHH:mm:ss");
            return await _httpClient.DeleteAsync($"api/precios-venta/{idProducto}/{fechaParam}");
        }
    }
}