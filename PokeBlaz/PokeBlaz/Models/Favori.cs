namespace PokeBlaz.Models
{
    public class Favori
    {
        public int PokemonId { get; set; }
        public string PokemonName { get; set; } = string.Empty;
        public string PokemonImage { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public DateTime DateAjout { get; set; } = DateTime.Now;
    }
}