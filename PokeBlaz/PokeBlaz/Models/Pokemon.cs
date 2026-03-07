using System.Text.Json.Serialization;

namespace PokeBlaz.Models
{
    public class Pokemon
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("image")]
        public string Image { get; set; } = string.Empty;

        [JsonPropertyName("base")]
        public PokemonStats Base { get; set; } = new();

        [JsonPropertyName("apiTypes")]
        public List<PokemonType> ApiTypes { get; set; } = new();
    }

    public class PokemonStats
    {
        [JsonPropertyName("HP")]
        public int HP { get; set; }

        [JsonPropertyName("Attack")]
        public int Attack { get; set; }

        [JsonPropertyName("Defense")]
        public int Defense { get; set; }

        [JsonPropertyName("Speed")]
        public int Speed { get; set; }
    }

    public class PokemonType
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("image")]
        public string Image { get; set; } = string.Empty;
    }
}