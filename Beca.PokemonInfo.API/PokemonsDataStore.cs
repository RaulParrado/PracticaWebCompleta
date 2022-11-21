using Beca.PokemonInfo.API.Models;

namespace Beca.PokemonInfo.API
{
    public class PokemonsDataStore
    {
        public List<PokemonDto> Pokemons { get; set; }
        public static PokemonsDataStore Current { get; } = new PokemonsDataStore();//Devuelve una instancia de pokemonsDataStore, osea las ciudades inicialidadas

        public PokemonsDataStore()
        {
            // init dummy data
            Pokemons = new List<PokemonDto>()
            {
                new PokemonDto()
                {
                     Id = 1,
                     Name = "Bulbasaur",
                     Description = "Pokemon tipo planta de la primera generacion, te lanza un látigo cepa y te deja moratón",
                     Attacks = new List<AttackDto>()
                     {
                         new AttackDto() {
                             Id = 1,
                             Name = "Placaje",
                             Description = "Placaje causa daño y no tiene ningún efecto secundario. Este movimiento tiene una potencia de 35 y una precisión del 95%." },
                          new AttackDto() {
                             Id = 2,
                             Name = "Látigo Cepa",
                             Description = "Látigo cepa causa daño y no tiene ningún efecto secundario. El movimiento tiene una potencia de 35 y 10 PP." },
                     }
                },
                new PokemonDto()
                {
                    Id = 2,
                    Name = "Charmander",
                    Description = "Pokemon tipo de agua de la primera generación, es el pokemon favorito de los verdaderos amantes de pokemon.",
                    Attacks = new List<AttackDto>()
                     {
                         new AttackDto() {
                             Id = 3,
                             Name = "Látigo",
                             Description = "Látigo baja en un nivel la defensa del oponente. En combates dobles y triples afecta a todos los oponentes adyacentes. No afecta a Pokémon con las habilidades cuerpo puro, humo blanco o sacapecho.\r\n\r\n" },
                          new AttackDto() {
                             Id = 4,
                             Name = "Lanzallamas",
                             Description = "Lanzallamas causa daño y tiene una probabilidad del 10% de quemar al objetivo. Lanzallamas tiene una potencia de 95.\r\n" },
                     }
                },
                new PokemonDto()
                {
                    Id= 3,
                    Name = "Squirtle",
                    Description = "Pokemon tipo de agua de la primera generación, tiene una concha a su espalda y está siempre calmado.",
                    Attacks = new List<AttackDto>()
                     {
                         new AttackDto() {
                             Id = 5,
                             Name = "Arañazo",
                             Description = "Arañazo causa daño y no tiene ningún efecto secundario." },
                          new AttackDto() {
                             Id = 6,
                             Name = "Pistola Agua",
                             Description = "Pistola agua causa daño y no tiene ningún efecto secundario." },
                     }
                }
            };

        }

    }
}
