using PokeBlaz.Models;

namespace PokeBlaz.Services
{
    // Service gérant les favoris en mémoire.
    // Fournit des opérations basiques : récupérer, ajouter, vérifier, supprimer.
    public class FavoriService
    {
        // Stockage local des favoris (durée de vie de l'application)
        private readonly List<Favori> _favoris = new();

        // Retourne la liste actuelle des favoris
        public List<Favori> GetAll() => _favoris;

        // Indique si un pokémon (par id) est déjà dans les favoris
        public bool IsFavori(int pokemonId) =>
            _favoris.Any(f => f.PokemonId == pokemonId);

        // Ajoute un pokémon aux favoris si absent. Permet d'ajouter une note optionnelle.
        public void Ajouter(Pokemon pokemon, string note = "")
        {
            if (!IsFavori(pokemon.Id))
            {
                _favoris.Add(new Favori
                {
                    PokemonId = pokemon.Id,
                    PokemonName = pokemon.Name,
                    PokemonImage = pokemon.Image,
                    Note = note
                });
            }
        }

        // Supprime le favori correspondant à l'id donné (s'il existe)
        public void Supprimer(int pokemonId)
        {
            var favori = _favoris.FirstOrDefault(f => f.PokemonId == pokemonId);
            if (favori != null) _favoris.Remove(favori);
        }
    }
}
