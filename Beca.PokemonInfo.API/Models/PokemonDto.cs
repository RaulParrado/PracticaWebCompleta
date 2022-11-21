namespace Beca.PokemonInfo.API.Models
{
    public class PokemonDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int NumberOfAttacks
        {
            get
            {
                return Attacks.Count;
            }
        }

        public ICollection<AttackDto> Attacks { get; set; }
            = new List<AttackDto>();
    }
}
