using System.Net.Http.Json;
using PokeBlaz.Models;

namespace PokeBlaz.Services
{
    /// <summary>
    /// Service gérant les appels vers l'API officielle pokeapi.co.
    /// Différence majeure avec pokebuildapi : la liste ne retourne que les noms/URLs,
    /// il faut donc fetch chaque Pokémon individuellement en parallèle.
    /// </summary>
    public class PokemonService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "https://pokeapi.co/api/v2";

        // Cache pour éviter de re-fetcher les Pokémons déjà chargés
        private List<Pokemon>? _cache = null;

        public PokemonService(HttpClient http)
        {
            _http = http;
        }

        /// <summary>
        /// Récupère les 151 premiers Pokémons depuis PokeAPI.
        /// PokeAPI nécessite deux étapes :
        /// 1. Récupérer la liste (noms + URLs)
        /// 2. Fetcher chaque Pokémon en parallèle via Task.WhenAll
        /// Le résultat est mis en cache pour éviter les appels répétés.
        /// </summary>
        public async Task<List<Pokemon>> GetAllPokemonsAsync()
        {
            // Retourner le cache si déjà chargé
            if (_cache != null) return _cache;

            try
            {
                // Étape 1 — récupérer la liste des 151 premiers
                var liste = await _http.GetFromJsonAsync<PokemonListResponse>(
                    $"{BaseUrl}/pokemon?limit=151"
                );

                if (liste == null) return GetMockPokemons();

                // Étape 2 — fetcher chaque Pokémon en parallèle
                var taches = liste.Results.Select(item =>
                    _http.GetFromJsonAsync<Pokemon>(item.Url)
                );

                var resultats = await Task.WhenAll(taches);

                // Capitaliser les noms (PokeAPI retourne tout en minuscules)
                _cache = resultats
                    .Where(p => p != null)
                    .Select(p => { p!.Name = Capitalize(p.Name); return p; })
                    .OrderBy(p => p.Id)
                    .ToList()!;

                return _cache;
            }
            catch (Exception)
            {
                return GetMockPokemons();
            }
        }

        /// <summary>
        /// Récupère un Pokémon par son ID.
        /// Vérifie d'abord le cache avant de faire un appel API.
        /// </summary>
        public async Task<Pokemon?> GetPokemonByIdAsync(int id)
        {
            // Vérifier le cache d'abord
            if (_cache != null)
            {
                var cached = _cache.FirstOrDefault(p => p.Id == id);
                if (cached != null) return cached;
            }

            try
            {
                var pokemon = await _http.GetFromJsonAsync<Pokemon>($"{BaseUrl}/pokemon/{id}");
                if (pokemon != null)
                    pokemon.Name = Capitalize(pokemon.Name);
                return pokemon;
            }
            catch (Exception)
            {
                return GetMockPokemons().FirstOrDefault(p => p.Id == id);
            }
        }

        /// <summary>Capitalise la première lettre d'un nom.</summary>
        private string Capitalize(string name) =>
            string.IsNullOrEmpty(name) ? name :
            char.ToUpper(name[0]) + name[1..];

        /// <summary>
        /// Données de secours si l'API est inaccessible.
        /// Utilise les sprites officiels de GitHub.
        /// </summary>
        private List<Pokemon> GetMockPokemons() => new()
        {
            new Pokemon { Id = 1, Name = "Bulbizarre",
                Sprites = new() { Other = new() { OfficialArtwork = new() { FrontDefault = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/1.png" } } },
                Stats = new() {
                    new() { BaseStat = 45, Stat = new() { Name = "hp" } },
                    new() { BaseStat = 49, Stat = new() { Name = "attack" } },
                    new() { BaseStat = 49, Stat = new() { Name = "defense" } },
                    new() { BaseStat = 65, Stat = new() { Name = "special-attack" } },
                    new() { BaseStat = 65, Stat = new() { Name = "special-defense" } },
                    new() { BaseStat = 45, Stat = new() { Name = "speed" } }
                },
                Types = new() { new() { Type = new() { Name = "grass" } } }
            },
            new Pokemon { Id = 4, Name = "Salamèche",
                Sprites = new() { Other = new() { OfficialArtwork = new() { FrontDefault = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/4.png" } } },
                Stats = new() {
                    new() { BaseStat = 39, Stat = new() { Name = "hp" } },
                    new() { BaseStat = 52, Stat = new() { Name = "attack" } },
                    new() { BaseStat = 43, Stat = new() { Name = "defense" } },
                    new() { BaseStat = 60, Stat = new() { Name = "special-attack" } },
                    new() { BaseStat = 50, Stat = new() { Name = "special-defense" } },
                    new() { BaseStat = 65, Stat = new() { Name = "speed" } }
                },
                Types = new() { new() { Type = new() { Name = "fire" } } }
            },
            new Pokemon { Id = 7, Name = "Carapuce",
                Sprites = new() { Other = new() { OfficialArtwork = new() { FrontDefault = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/7.png" } } },
                Stats = new() {
                    new() { BaseStat = 44, Stat = new() { Name = "hp" } },
                    new() { BaseStat = 48, Stat = new() { Name = "attack" } },
                    new() { BaseStat = 65, Stat = new() { Name = "defense" } },
                    new() { BaseStat = 50, Stat = new() { Name = "special-attack" } },
                    new() { BaseStat = 64, Stat = new() { Name = "special-defense" } },
                    new() { BaseStat = 43, Stat = new() { Name = "speed" } }
                },
                Types = new() { new() { Type = new() { Name = "water" } } }
            },
        };
    }
}