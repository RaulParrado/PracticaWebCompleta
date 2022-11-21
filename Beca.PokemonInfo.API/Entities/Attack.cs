using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beca.PokemonInfo.API.Entities
{
    public class Attack
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        //[Required]
        //[MaxLength(50)]
        //public int Tipo { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }


        [ForeignKey("PokemonId")]
        public Pokemon? Pokemon { get; set; }
        public int PokemonId { get; set; }

        public Attack(string name)
        {
            Name = name;
        }
    }
}
