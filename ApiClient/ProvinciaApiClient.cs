using DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    public class ProvinciaApiClient
    {
        private static readonly HttpClient client = new HttpClient();

        static ProvinciaApiClient()
        {
            client.BaseAddress = new Uri("https://localhost:7145/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

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
    }
}
