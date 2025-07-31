using DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WinForms.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        // El puerto debe coincidir con el de launchSettings.json
        private readonly string _baseUri = "https://localhost:7145";

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        // --- CRUD para Provincias ---

        public async Task<List<Provincia>> GetProvinciasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Provincia>>($"{_baseUri}/provincias");
        }

        public async Task<bool> AddProvinciaAsync(Provincia provincia)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUri}/provincias", provincia);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProvinciaAsync(Provincia provincia)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUri}/provincias", provincia);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProvinciaAsync(int codigoProvincia)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUri}/provincias/{codigoProvincia}");
            return response.IsSuccessStatusCode;
        }

        // --- CRUD para Tipos de Producto ---

        public async Task<List<TipoProducto>> GetTiposProductoAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TipoProducto>>($"{_baseUri}/tiposproducto");
        }

        public async Task<bool> AddTipoProductoAsync(TipoProducto tipoProducto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUri}/tiposproducto", tipoProducto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTipoProductoAsync(TipoProducto tipoProducto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUri}/tiposproducto", tipoProducto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTipoProductoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUri}/tiposproducto/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}