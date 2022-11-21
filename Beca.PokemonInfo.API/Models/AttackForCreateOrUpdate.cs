using System.ComponentModel.DataAnnotations;

namespace Beca.PokemonInfo.API.Models
{
    public class AttackForCreateOrUpdateDto
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(300)]
        public string? Description { get; set; }

    }
}
