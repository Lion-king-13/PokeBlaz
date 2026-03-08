using System.Text.Json.Serialization;

namespace PokeBlaz.Models
{
    /// <summary>
    /// Représente un Pokémon tel que retourné par l'API pokebuildapi.fr.
    /// Chaque propriété est mappée sur un champ JSON via [JsonPropertyName].
    /// </summary>
    public class Pokemon
    {
        /// <summary>Identifiant unique du Pokémon (ex: 1 pour Bulbizarre).</summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>Nom du Pokémon (ex: "Bulbizarre").</summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>URL de l'image officielle du Pokémon.</summary>
        [JsonPropertyName("image")]
        public string Image { get; set; } = string.Empty;

        /// <summary>
        /// Statistiques de combat du Pokémon.
        /// Mappé sur le champ "stats" dans le JSON de l'API.
        /// </summary>
        [JsonPropertyName("stats")]
        public PokemonStats Base { get; set; } = new();

        /// <summary>
        /// Liste des types du Pokémon (ex: Feu, Eau, Plante...).
        /// Chaque type contient un nom et une image.
        /// </summary>
        [JsonPropertyName("apiTypes")]
        public List<PokemonType> ApiTypes { get; set; } = new();
    }

    /// <summary>
    /// Représente les statistiques de combat d'un Pokémon.
    /// Les noms des propriétés correspondent exactement aux champs JSON de l'API.
    /// </summary>
    public class PokemonStats
    {
        /// <summary>Points de vie — détermine la résistance aux dégâts.</summary>
        [JsonPropertyName("HP")]
        public int HP { get; set; }

        /// <summary>Attaque physique — détermine les dégâts des capacités physiques.</summary>
        [JsonPropertyName("attack")]
        public int attack { get; set; }

        /// <summary>Défense physique — réduit les dégâts des attaques physiques reçues.</summary>
        [JsonPropertyName("defense")]
        public int defense { get; set; }

        /// <summary>Attaque spéciale — détermine les dégâts des capacités spéciales.</summary>
        [JsonPropertyName("special_attack")]
        public int special_attack { get; set; }

        /// <summary>Défense spéciale — réduit les dégâts des attaques spéciales reçues.</summary>
        [JsonPropertyName("special_defense")]
        public int special_defense { get; set; }

        /// <summary>Vitesse — détermine quel Pokémon attaque en premier.</summary>
        [JsonPropertyName("speed")]
        public int speed { get; set; }
    }

    /// <summary>
    /// Représente un type de Pokémon (ex: Feu, Eau, Plante...).
    /// Contient le nom du type et l'URL de son icône.
    /// </summary>
    public class PokemonType
    {
        /// <summary>Nom du type (ex: "Feu", "Eau").</summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>URL de l'icône représentant ce type.</summary>
        [JsonPropertyName("image")]
        public string Image { get; set; } = string.Empty;
    }
}