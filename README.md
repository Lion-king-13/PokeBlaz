# PokeBlaz 🎮

Application web développée en **Blazor Server (.NET)** dans le cadre du cours de Programmation Orientée Objet.

Elle permet de consulter des Pokémons via une API externe, de gérer des favoris personnels et de composer un build.

---

## Fonctionnalités

- 📋 **Liste des Pokémons** — affichage en grille avec recherche en temps réel
- 🔍 **Détail d'un Pokémon** — stats, types, barres de progression
- 🔐 **Authentification** — connexion/déconnexion avec pages protégées
- ❤️ **Favoris** — ajout, suppression et notes personnelles (utilisateur connecté)
- ⚔️ **Build** — composition d'une équipe de 6 Pokémons avec stats moyennes et génération aléatoire (utilisateur connecté)

---

## API utilisée

**PokeBuildAPI** — [https://pokebuildapi.fr/api/v1](https://pokebuildapi.fr/api/v1)

API gratuite, sans authentification requise, qui fournit des données enrichies sur les Pokémons.

### Endpoints utilisés

| Endpoint | Description |
|----------|-------------|
| `GET /pokemon/limit/151` | Récupère les 151 premiers Pokémons |
| `GET /pokemon/{id}` | Récupère un Pokémon par son ID |

### Exemple de réponse (`/pokemon/1`)

```json
{
  "id": 1,
  "name": "Bulbizarre",
  "image": "https://raw.githubusercontent.com/.../1.png",
  "sprite": "https://raw.githubusercontent.com/.../1.png",
  "slug": "Bulbizarre",
  "stats": {
    "HP": 45,
    "attack": 49,
    "defense": 49,
    "special_attack": 65,
    "special_defense": 65,
    "speed": 45
  },
  "apiTypes": [
    {
      "name": "Plante",
      "image": "https://pokebuildapi.fr/..."
    }
  ]
}
```

---

## Stack technique

| Technologie | Usage |
|-------------|-------|
| **Blazor Server (.NET 9)** | Framework principal |
| **C#** | Langage de programmation |
| **HttpClient** | Appels vers l'API |
| **xUnit** | Tests unitaires |
| **CSS3 + Google Fonts** | Style (thème rétro-gaming) |

---

## Structure du projet

```
PokeBlaz/
├── Models/
│   ├── Pokemon.cs          # Modèle Pokémon mappé sur l'API
│   └── Favori.cs           # Modèle favori local
│
├── Services/
│   ├── PokemonService.cs   # Appels HTTP vers l'API
│   ├── FavoriService.cs    # Gestion des favoris en mémoire
│   └── AuthService.cs      # Authentification + événement OnAuthChanged
│
├── Pages/
│   ├── Home.razor          # Page d'accueil
│   ├── PokemonList.razor   # Liste avec recherche
│   ├── PokemonDetail.razor # Détail + stats
│   ├── Favoris.razor       # Gestion des favoris (protégée)
│   ├── Build.razor         # Composition d'équipe (protégée)
│   └── Login.razor         # Formulaire de connexion
│
├── Components/Layout/
│   ├── MainLayout.razor    # Structure générale des pages
│   └── NavMenu.razor       # Barre de navigation réactive
│
├── wwwroot/css/
│   └── pokemon.css         # Styles globaux
│
└── Program.cs              # Configuration des services

PokeBlaz.Tests/
├── FavoriServiceTests.cs   # 6 tests unitaires sur FavoriService
└── AuthServiceTests.cs     # 4 tests unitaires sur AuthService
```

---

## Installation et lancement

### Prérequis

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022 ou VS Code

### Lancer le projet

```bash
# Cloner le projet
git clone https://github.com/ton-utilisateur/PokeBlaz.git
cd PokeBlaz

# Lancer l'application
dotnet run --project PokeBlaz
```

L'application sera disponible sur `https://localhost:xxxx`.

### Lancer les tests

```bash
dotnet test PokeBlaz.Tests
```

Ou dans Visual Studio : **Test → Exécuter tous les tests** (`Ctrl+R, A`)

---

## Comptes de démonstration

| Utilisateur | Mot de passe |
|-------------|--------------|
| `ash` | `pikachu` |
| `misty` | `togepi` |

---

## Tests unitaires

10 tests couvrant les deux services critiques :

**FavoriServiceTests** (6 tests)
- Ajout d'un favori
- Pas de doublon
- Suppression
- `IsFavori` → true
- `IsFavori` → false
- Sauvegarde d'une note

**AuthServiceTests** (4 tests)
- Connexion avec identifiants valides
- Connexion avec identifiants invalides
- Déconnexion
- Insensibilité à la casse du nom d'utilisateur
