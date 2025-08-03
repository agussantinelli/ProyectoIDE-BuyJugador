using DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WinForms.Services
{
    public class ApiClient
    {
        private static HttpClient client = new HttpClient();

        static ApiClient()
        {
            // Configura la dirección base de la API.
            client.BaseAddress = new Uri("https://localhost:7145/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Métodos para Provincias
        public static async Task<List<ProvinciaDto>> GetProvinciasAsync()
        {
            HttpResponseMessage response = await client.GetAsync("provincias");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ProvinciaDto>>();
        }

        public static async Task<ProvinciaDto> GetProvinciaAsync(int codigo)
        {
            HttpResponseMessage response = await client.GetAsync($"provincias/{codigo}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ProvinciaDto>();
            }
            return null;
        }

        public static async Task AddProvinciaAsync(ProvinciaDto provincia)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("provincias", provincia);
            response.EnsureSuccessStatusCode();
        }

        public static async Task UpdateProvinciaAsync(ProvinciaDto provincia)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"provincias/{provincia.CodigoProvincia}", provincia);
            response.EnsureSuccessStatusCode();
        }

        public static async Task DeleteProvinciaAsync(int codigo)
        {
            HttpResponseMessage response = await client.DeleteAsync($"provincias/{codigo}");
            response.EnsureSuccessStatusCode();
        }

        // Métodos para Tipos de Producto
        public static async Task<List<TipoProductoDto>> GetTiposProductoAsync()
        {
            HttpResponseMessage response = await client.GetAsync("tiposproducto");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<TipoProductoDto>>();
        }

        public static async Task<TipoProductoDto> GetTipoProductoAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"tiposproducto/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TipoProductoDto>();
            }
            return null;
        }

        public static async Task AddTipoProductoAsync(TipoProductoDto tipoProducto)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("tiposproducto", tipoProducto);
            response.EnsureSuccessStatusCode();
        }

        public static async Task UpdateTipoProductoAsync(TipoProductoDto tipoProducto)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"tiposproducto/{tipoProducto.IdTipoProducto}", tipoProducto);
            response.EnsureSuccessStatusCode();
        }

        public static async Task DeleteTipoProductoAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"tiposproducto/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}