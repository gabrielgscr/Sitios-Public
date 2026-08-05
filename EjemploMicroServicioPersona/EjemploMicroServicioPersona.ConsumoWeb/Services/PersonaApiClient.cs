using System.Net;
using System.Net.Http.Json;

namespace EjemploMicroServicioPersona.ConsumoWeb.Services
{
    public interface IPersonaApiClient
    {
        Task<List<PersonaDto>> GetAllAsync(CancellationToken ct = default);
        Task<PersonaDto?> GetByIdAsync(string id, CancellationToken ct = default);
        Task<(bool ok, HttpStatusCode status, string? message)> CreateAsync(PersonaDto persona, CancellationToken ct = default);
        Task<bool> UpdateAsync(PersonaDto persona, CancellationToken ct = default);
        Task<bool> DeleteAsync(string id, CancellationToken ct = default);
    }

    public class PersonaApiClient : IPersonaApiClient
    {
        private readonly HttpClient _http;

        public PersonaApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PersonaDto>> GetAllAsync(CancellationToken ct = default)
        {
            var result = await _http.GetFromJsonAsync<List<PersonaDto>>("api/Persona", ct);
            return result ?? new List<PersonaDto>();
        }

        public async Task<PersonaDto?> GetByIdAsync(string id, CancellationToken ct = default)
        {
            return await _http.GetFromJsonAsync<PersonaDto>($"api/Persona/{id}", ct);
        }

        public async Task<(bool ok, HttpStatusCode status, string? message)> CreateAsync(PersonaDto persona, CancellationToken ct = default)
        {
            var response = await _http.PostAsJsonAsync("api/Persona", persona, ct);
            string? msg = null;
            try
            {
                // intentar leer objeto { message: "..." }
                var payload = await response.Content.ReadFromJsonAsync<Dictionary<string, string?>>(cancellationToken: ct);
                if (payload != null && payload.TryGetValue("message", out var m))
                {
                    msg = m;
                }
            }
            catch { /* ignorar parseos fallidos */ }
            return (response.IsSuccessStatusCode, response.StatusCode, msg);
        }

        public async Task<bool> UpdateAsync(PersonaDto persona, CancellationToken ct = default)
        {
            var response = await _http.PutAsJsonAsync($"api/Persona/{persona.PersonaId}", persona, ct);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string id, CancellationToken ct = default)
        {
            var response = await _http.DeleteAsync($"api/Persona/{id}", ct);
            return response.IsSuccessStatusCode;
        }
    }
}
