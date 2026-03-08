using PokeBlaz.Models;
using System.Net.Http.Json;

namespace PokeBlaz.Services
{
    public class PokemonService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "https://pokebuildapi.fr/api/v1";

        public PokemonService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Pokemon>> GetAllPokemonsAsync()
        {
            return await _http.GetFromJsonAsync<List<Pokemon>>($"{BaseUrl}/pokemon")
                   ?? new List<Pokemon>();
        }

        public async Task<Pokemon?> GetPokemonByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<Pokemon>($"{BaseUrl}/pokemon/{id}");
        }
    }
}