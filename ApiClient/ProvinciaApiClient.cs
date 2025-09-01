using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DominioModelo;

namespace ApiClient
{
    // Esta clase maneja toda la comunicación con los endpoints de provincias de la API.
    // Usamos el patrón de inyección de dependencias para el HttpClient.
    public class ProvinciaApiClient
    {
        private readonly HttpClient _httpClient;

        public ProvinciaApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Obtiene una lista de todas las provincias desde la API.
        public async Task<List<Provincia>?> GetAllAsync()
        {
            // Realiza la petición GET al endpoint de provincias.
            var response = await _httpClient.GetAsync("api/provincias");
            // Lanza una excepción si la respuesta no es exitosa.
            response.EnsureSuccessStatusCode();
            // Deserializa el JSON de la respuesta a una lista de objetos Provincia.
            return await response.Content.ReadFromJsonAsync<List<Provincia>>();
        }

        // Obtiene una provincia por su ID.
        public async Task<Provincia?> GetByIdAsync(int id)
        {
            // Realiza la petición GET al endpoint específico de la provincia.
            var response = await _httpClient.GetAsync($"api/provincias/{id}");
            // Si la respuesta es 404 Not Found, devuelve null.
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            // Lanza una excepción si la respuesta no es exitosa por otro motivo.
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Provincia>();
        }

        // Crea una nueva provincia en la API.
        public async Task<Provincia?> CreateAsync(Provincia provincia)
        {
            // Envía el objeto Provincia como JSON en una petición POST.
            var response = await _httpClient.PostAsJsonAsync("api/provincias", provincia);
            response.EnsureSuccessStatusCode();
            // Devuelve el objeto creado, que incluye el ID generado por la API.
            return await response.Content.ReadFromJsonAsync<Provincia>();
        }

        // Actualiza una provincia existente.
        public async Task UpdateAsync(int id, Provincia provincia)
        {
            // Envía el objeto actualizado en una petición PUT.
            var response = await _httpClient.PutAsJsonAsync($"api/provincias/{id}", provincia);
            response.EnsureSuccessStatusCode();
        }

        // Elimina una provincia por su ID.
        public async Task DeleteAsync(int id)
        {
            // Envía una petición DELETE para eliminar el recurso.
            var response = await _httpClient.DeleteAsync($"api/provincias/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
