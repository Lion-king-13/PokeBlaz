using PokeBlaz.Models;
using PokeBlaz.Services;

namespace PokeBlaz.Tests
{
    public class FavoriServiceTests
    {
        private FavoriService _service = new();

        private Pokemon FakePokemon(int id = 1) => new Pokemon
        {
            Id = id,
            Name = $"Pokemon{id}",
            Image = "image.png"
        };

        // ✅ Ajouter un favori
        [Fact]
        public void Ajouter_PokemonNonExistant_AjouteDansLaListe()
        {
            var pokemon = FakePokemon(1);
            _service.Ajouter(pokemon);
            Assert.Single(_service.GetAll());
        }

        // ✅ Ne pas ajouter en double
        [Fact]
        public void Ajouter_PokemonDejaExistant_NAjoutePasEnDouble()
        {
            var pokemon = FakePokemon(1);
            _service.Ajouter(pokemon);
            _service.Ajouter(pokemon);
            Assert.Single(_service.GetAll());
        }

        // ✅ Supprimer un favori
        [Fact]
        public void Supprimer_PokemonExistant_RetireDelaListe()
        {
            var pokemon = FakePokemon(1);
            _service.Ajouter(pokemon);
            _service.Supprimer(1);
            Assert.Empty(_service.GetAll());
        }

        // ✅ IsFavori retourne true
        [Fact]
        public void IsFavori_PokemonAjoute_RetourneTrue()
        {
            var pokemon = FakePokemon(1);
            _service.Ajouter(pokemon);
            Assert.True(_service.IsFavori(1));
        }

        // ✅ IsFavori retourne false
        [Fact]
        public void IsFavori_PokemonNonAjoute_RetourneFalse()
        {
            Assert.False(_service.IsFavori(99));
        }

        // ✅ Ajouter une note
        [Fact]
        public void Ajouter_AvecNote_SauvegardeNote()
        {
            var pokemon = FakePokemon(1);
            _service.Ajouter(pokemon, "Mon pokémon préféré");
            var favori = _service.GetAll().First();
            Assert.Equal("Mon pokémon préféré", favori.Note);
        }
    }
}