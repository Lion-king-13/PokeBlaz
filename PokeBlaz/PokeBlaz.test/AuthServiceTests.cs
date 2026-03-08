using PokeBlaz.Services;

namespace PokeBlaz.Tests
{
    public class AuthServiceTests
    {
        private AuthService _service = new();

        // ✅ Connexion valide
        [Fact]
        public void Connecter_IdentifiantsValides_RetourneTrue()
        {
            var result = _service.Connecter("ash", "pikachu");
            Assert.True(result);
            Assert.True(_service.EstConnecte);
            Assert.Equal("ash", _service.NomUtilisateur);
        }

        // ✅ Connexion invalide
        [Fact]
        public void Connecter_IdentifiantsInvalides_RetourneFalse()
        {
            var result = _service.Connecter("ash", "mauvaismdp");
            Assert.False(result);
            Assert.False(_service.EstConnecte);
        }

        // ✅ Déconnexion
        [Fact]
        public void Deconnecter_UtilisateurConnecte_ReinitialiseLEtat()
        {
            _service.Connecter("ash", "pikachu");
            _service.Deconnecter();
            Assert.False(_service.EstConnecte);
            Assert.Equal("", _service.NomUtilisateur);
        }

        // ✅ Connexion insensible à la casse
        [Fact]
        public void Connecter_NomEnMajuscules_FonctionneQuandMeme()
        {
            var result = _service.Connecter("ASH", "pikachu");
            Assert.True(result);
        }
    }
}