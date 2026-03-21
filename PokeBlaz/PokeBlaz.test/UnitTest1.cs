using PokeBlaz.Models;
using PokeBlaz.Services;

namespace PokeBlaz.Tests
{
    // =============================================
    // Tests unitaires — FavoriService
    // =============================================
    public class FavoriServiceTests
    {
        private FavoriService _service = new();

        /// <summary>Crée un Pokémon de test avec un ID donné.</summary>
        private Pokemon FakePokemon(int id = 1) => new Pokemon
        {
            Id = id,
            Name = $"Pokemon{id}",
            Sprites = new PokemonSprites
            {
                Other = new OtherSprites
                {
                    OfficialArtwork = new OfficialArtwork { FrontDefault = "image.png" }
                }
            }
        };

        [Fact]
        public void Ajouter_PokemonNonExistant_AjouteDansLaListe()
        {
            _service.Ajouter(FakePokemon(1));
            Assert.Single(_service.GetAll());
        }

        [Fact]
        public void Ajouter_PokemonDejaExistant_NAjoutePasEnDouble()
        {
            _service.Ajouter(FakePokemon(1));
            _service.Ajouter(FakePokemon(1));
            Assert.Single(_service.GetAll());
        }

        [Fact]
        public void Supprimer_PokemonExistant_RetireDelaListe()
        {
            _service.Ajouter(FakePokemon(1));
            _service.Supprimer(1);
            Assert.Empty(_service.GetAll());
        }

        [Fact]
        public void IsFavori_PokemonAjoute_RetourneTrue()
        {
            _service.Ajouter(FakePokemon(1));
            Assert.True(_service.IsFavori(1));
        }

        [Fact]
        public void IsFavori_PokemonNonAjoute_RetourneFalse()
        {
            Assert.False(_service.IsFavori(99));
        }

        [Fact]
        public void Ajouter_AvecNote_SauvegardeNote()
        {
            _service.Ajouter(FakePokemon(1), "Mon préféré");
            Assert.Equal("Mon préféré", _service.GetAll().First().Note);
        }
    }

    // =============================================
    // Tests unitaires — AuthService
    // =============================================
    public class AuthServiceTests
    {
        private AuthService _service = new();

        [Fact]
        public void Connecter_IdentifiantsValides_RetourneTrue()
        {
            var result = _service.Connecter("ash", "pikachu");
            Assert.True(result);
            Assert.True(_service.EstConnecte);
            Assert.Equal("ash", _service.NomUtilisateur);
        }

        [Fact]
        public void Connecter_IdentifiantsInvalides_RetourneFalse()
        {
            var result = _service.Connecter("ash", "mauvaismdp");
            Assert.False(result);
            Assert.False(_service.EstConnecte);
        }

        [Fact]
        public void Deconnecter_UtilisateurConnecte_ReinitialiseLEtat()
        {
            _service.Connecter("ash", "pikachu");
            _service.Deconnecter();
            Assert.False(_service.EstConnecte);
            Assert.Equal("", _service.NomUtilisateur);
        }

        [Fact]
        public void Connecter_NomEnMajuscules_FonctionneQuandMeme()
        {
            Assert.True(_service.Connecter("ASH", "pikachu"));
        }
    }
}