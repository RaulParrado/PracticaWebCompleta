using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beca.PokemonInfo.API.Entities
{
    public class Pokemon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        //[Required]
        //[MaxLength(50)]
        //public string Tipo { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        public ICollection<Attack> Attacks { get; set; }    
               = new List<Attack>();

        public Pokemon(string name)
        {
            Name = name;
        }
    }
}
