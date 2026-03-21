namespace PokeBlaz.Services
{
    // Service simple d'authentification en mémoire utilisé pour
    // simuler une session utilisateur dans l'application.
    public class AuthService
    {
        // Indique si un utilisateur est connecté
        public bool EstConnecte { get; private set; } = false;
        // Nom de l'utilisateur connecté
        public string NomUtilisateur { get; private set; } = "";

        // Événement déclenché quand l'état d'auth change (souscrit par UI)
        public event Action? OnAuthChanged;

        // Utilisateurs en dur pour la démo (nom -> mot de passe)
        private readonly Dictionary<string, string> _utilisateurs = new()
        {
            { "ash", "pikachu" },
            { "misty", "togepi" },
        };

        // Tente de connecter un utilisateur en vérifiant le dictionnaire.
        // Retourne true si la connexion est réussie et notifie les abonnés.
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

        // Déconnecte l'utilisateur courant et notifie les abonnés.
        public void Deconnecter()
        {
            EstConnecte = false;
            NomUtilisateur = "";
            OnAuthChanged?.Invoke();
        }
    }
}
