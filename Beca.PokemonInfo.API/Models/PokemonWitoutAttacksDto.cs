namespace Beca.PokemonInfo.API.Models
{
    public class PokemonWithoutAttacksDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

    }
}