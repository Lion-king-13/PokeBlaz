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

        [JsonPropertyName("stats")]
        public PokemonStats Base { get; set; } = new();

        [JsonPropertyName("apiTypes")]
        public List<PokemonType> ApiTypes { get; set; } = new();
    }

    public class PokemonStats
    {
        [JsonPropertyName("HP")]
        public int HP { get; set; }

        [JsonPropertyName("attack")]
        public int attack { get; set; }

        [JsonPropertyName("defense")]
        public int defense { get; set; }

        [JsonPropertyName("special_attack")]
        public int special_attack { get; set; }

        [JsonPropertyName("special_defense")]
        public int special_defense { get; set; }


        [JsonPropertyName("speed")]
        public int speed { get; set; }
    }

    public class PokemonType
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("image")]
        public string Image { get; set; } = string.Empty;
    }
}