using PokeBlaz.Models;

namespace PokeBlaz.Services
{
    public class FavoriService
    {
        private readonly List<Favori> _favoris = new();

        public List<Favori> GetAll() => _favoris;

        public bool IsFavori(int pokemonId) =>
            _favoris.Any(f => f.PokemonId == pokemonId);

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

        public void Supprimer(int pokemonId)
        {
            var favori = _favoris.FirstOrDefault(f => f.PokemonId == pokemonId);
            if (favori != null) _favoris.Remove(favori);
        }
    }
}