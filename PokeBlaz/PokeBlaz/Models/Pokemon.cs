using System.Text.Json.Serialization;

namespace PokeBlaz.Models
{
    /// <summary>
    /// Représente un Pokémon tel que retourné par l'API officielle pokeapi.co.
    /// La structure JSON est plus complexe que pokebuildapi — les stats et types
    /// sont des tableaux imbriqués plutôt que des objets simples.
    /// </summary>
    public class Pokemon
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Les sprites sont imbriqués dans un objet complexe.
        /// On utilise PokemonSprites pour naviguer jusqu'à l'image officielle.
        /// </summary>
        [JsonPropertyName("sprites")]
        public PokemonSprites Sprites { get; set; } = new();

        /// <summary>
        /// Propriété calculée pour accéder facilement à l'image.
        /// Utilisée dans les pages Razor à la place de pokemon.Image.
        /// </summary>
        public string Image => Sprites?.Other?.OfficialArtwork?.FrontDefault
            ?? Sprites?.FrontDefault
            ?? "";

        /// <summary>
        /// Stats sous forme de tableau dans PokeAPI.
        /// Ex: [{ "base_stat": 45, "stat": { "name": "hp" } }]
        /// </summary>
        [JsonPropertyName("stats")]
        public List<PokemonStat> Stats { get; set; } = new();

        /// <summary>Propriété calculée pour accéder aux stats comme avant.</summary>
        public PokemonStats Base => new PokemonStats
        {
            HP = Stats.FirstOrDefault(s => s.Stat.Name == "hp")?.BaseStat ?? 0,
            attack = Stats.FirstOrDefault(s => s.Stat.Name == "attack")?.BaseStat ?? 0,
            defense = Stats.FirstOrDefault(s => s.Stat.Name == "defense")?.BaseStat ?? 0,
            special_attack = Stats.FirstOrDefault(s => s.Stat.Name == "special-attack")?.BaseStat ?? 0,
            special_defense = Stats.FirstOrDefault(s => s.Stat.Name == "special-defense")?.BaseStat ?? 0,
            speed = Stats.FirstOrDefault(s => s.Stat.Name == "speed")?.BaseStat ?? 0,
        };

        /// <summary>
        /// Types sous forme de tableau dans PokeAPI.
        /// Ex: [{ "slot": 1, "type": { "name": "fire" } }]
        /// </summary>
        [JsonPropertyName("types")]
        public List<PokemonTypeSlot> Types { get; set; } = new();

        /// <summary>Propriété calculée pour garder la compatibilité avec les pages Razor.</summary>
        public List<PokemonType> ApiTypes => Types
            .Select(t => new PokemonType
            {
                Name = t.Type.Name,
                // PokeAPI ne fournit pas d'images de types — on génère l'URL manuellement
                Image = $"https://raw.githubusercontent.com/duiker101/pokemon-type-svg-icons/master/icons/{t.Type.Name}.svg"
            })
            .ToList();
    }

    /// <summary>Entrée dans le tableau des stats de PokeAPI.</summary>
    public class PokemonStat
    {
        [JsonPropertyName("base_stat")]
        public int BaseStat { get; set; }

        [JsonPropertyName("stat")]
        public StatInfo Stat { get; set; } = new();
    }

    public class StatInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }

    /// <summary>Entrée dans le tableau des types de PokeAPI.</summary>
    public class PokemonTypeSlot
    {
        [JsonPropertyName("slot")]
        public int Slot { get; set; }

        [JsonPropertyName("type")]
        public TypeInfo Type { get; set; } = new();
    }

    public class TypeInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }

    /// <summary>
    /// Statistiques de combat — structure identique à avant.
    /// Calculée depuis le tableau Stats via les propriétés de Pokemon.
    /// </summary>
    public class PokemonStats
    {
        public int HP { get; set; }
        public int attack { get; set; }
        public int defense { get; set; }
        public int special_attack { get; set; }
        public int special_defense { get; set; }
        public int speed { get; set; }
    }

    /// <summary>Type d'un Pokémon — structure identique à avant pour la compatibilité.</summary>
    public class PokemonType
    {
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }

    // ===== Sprites =====
    public class PokemonSprites
    {
        [JsonPropertyName("front_default")]
        public string? FrontDefault { get; set; }

        [JsonPropertyName("other")]
        public OtherSprites? Other { get; set; }
    }

    public class OtherSprites
    {
        [JsonPropertyName("official-artwork")]
        public OfficialArtwork? OfficialArtwork { get; set; }
    }

    public class OfficialArtwork
    {
        [JsonPropertyName("front_default")]
        public string? FrontDefault { get; set; }
    }

    // ===== Liste =====
    /// <summary>
    /// Modèle pour la réponse de l'endpoint /pokemon?limit=151.
    /// PokeAPI retourne une liste paginée avec juste les noms et URLs.
    /// Il faut ensuite fetch chaque Pokémon individuellement.
    /// </summary>
    public class PokemonListResponse
    {
        [JsonPropertyName("results")]
        public List<PokemonListItem> Results { get; set; } = new();
    }

    public class PokemonListItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }
}