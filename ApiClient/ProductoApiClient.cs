using DominioModelo;
using DTOs; // Asegúrate de que DTOs.Producto exista
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    public class ProductoApiClient
    {
        private readonly HttpClient _httpClient;
        // Reemplaza esta URL base por la URL real de tu WebAPI en ejecución
        private readonly string _baseUrl = "https://localhost:7001/api/productos";

        public ProductoApiClient(string apiBaseUrl = "https://localhost:7001")
        {
            _httpClient = new HttpClient();
            // Ajusta la URL base si es necesario, incluyendo el prefijo /api/productos
            _baseUrl = $"{apiBaseUrl.TrimEnd('/')}/api/productos";
        }

        public async Task<List<Producto>> GetProductosAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Producto>>(_baseUrl);
            }
            catch (HttpRequestException ex)
            {
                // Manejo de errores (log, lanzar excepción personalizada, etc.)
                Console.WriteLine($"Error al obtener productos: {ex.Message}");
                return new List<Producto>();
            }
        }

        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Producto>($"{_baseUrl}/{id}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al obtener producto por ID: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateProductoAsync(Producto producto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, producto);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al crear producto: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateProductoAsync(int id, Producto producto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", producto);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al actualizar producto: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteProductoAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al eliminar producto: {ex.Message}");
                return false;
            }
        }
    }
}