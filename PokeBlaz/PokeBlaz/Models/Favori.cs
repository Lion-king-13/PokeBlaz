namespace PokeBlaz.Models
{
    /// <summary>
    /// Représente un Pokémon sauvegardé localement par l'utilisateur.
    /// Contrairement au modèle Pokemon, ce modèle est indépendant de l'API
    /// et est géré entièrement par FavoriService en mémoire.
    /// </summary>
    public class Favori
    {
        /// <summary>
        /// Identifiant du Pokémon d'origine.
        /// Permet de faire le lien avec les données de l'API si besoin.
        /// </summary>
        public int PokemonId { get; set; }

        /// <summary>
        /// Nom du Pokémon copié au moment de l'ajout aux favoris.
        /// Stocké localement pour éviter un appel API supplémentaire.
        /// </summary>
        public string PokemonName { get; set; } = string.Empty;

        /// <summary>
        /// URL de l'image du Pokémon copiée au moment de l'ajout.
        /// Stockée localement pour l'affichage sans appel API.
        /// </summary>
        public string PokemonImage { get; set; } = string.Empty;

        /// <summary>
        /// Note personnelle que l'utilisateur peut écrire pour ce favori.
        /// Modifiable depuis la page Favoris.razor.
        /// </summary>
        public string Note { get; set; } = string.Empty;

        /// <summary>
        /// Date et heure d'ajout aux favoris.
        /// Initialisée automatiquement à la création de l'objet.
        /// </summary>
        public DateTime DateAjout { get; set; } = DateTime.Now;
    }
}