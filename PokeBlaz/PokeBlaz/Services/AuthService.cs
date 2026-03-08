namespace PokeBlaz.Services
{
    public class AuthService
    {
        public bool EstConnecte { get; private set; } = false;
        public string NomUtilisateur { get; private set; } = "";

        public event Action? OnAuthChanged;

        private readonly Dictionary<string, string> _utilisateurs = new()
        {
            { "ash", "pikachu" },
            { "misty", "togepi" }
        };

        public bool Connecter(string nom, string motDePasse)
        {
            if (_utilisateurs.TryGetValue(nom.ToLower(), out var mdp) && mdp == motDePasse)
            {
                EstConnecte = true;
                NomUtilisateur = nom;
                OnAuthChanged?.Invoke();
                return true;
            }
            return false;
        }

        public void Deconnecter()
        {
            EstConnecte = false;
            NomUtilisateur = "";
            OnAuthChanged?.Invoke();
        }
    }
}