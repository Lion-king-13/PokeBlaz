using PokeBlaz.Models;

namespace PokeBlaz.Services
{
    // Service chargé d'interagir avec l'API externe pour récupérer
    // les données des pokémons. Encapsule l'utilisation de HttpClient.
    public class PokemonService
    {
        private readonly HttpClient _http;
        // URL de base de l'API utilisée pour récupérer les pokémons
        private const string BaseUrl = "https://pokebuildapi.fr/api/v1";

        public PokemonService(HttpClient http)
        {
            _http = http;
        }

        // Récupère la liste (limitée) des pokémons depuis l'API.
        // Renvoie une liste vide si la désérialisation retourne null.
        public async Task<List<Pokemon>> GetAllPokemonsAsync()
        {
            return await _http.GetFromJsonAsync<List<Pokemon>>($"{BaseUrl}/pokemon/Limit/200")
                   ?? new List<Pokemon>();
        }

        // Récupère le détail d'un pokémon par son id. Peut renvoyer null
        // si le pokémon n'est pas trouvé.
        public async Task<Pokemon?> GetPokemonByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<Pokemon>($"{BaseUrl}/pokemon/{id}");
        }
    }
}
