using Beca.PokemonInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Beca.PokemonInfo.API.DbContexts
{
    public class PokemonInfoContext : DbContext
    {
        public DbSet<Pokemon> Pokemons { get; set; } = null!;
        public DbSet<Attack> Attacks { get; set; } = null!;

        public PokemonInfoContext(DbContextOptions<PokemonInfoContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pokemon>()
                .HasData(
               new Pokemon("Bulbasaur")
               {
                   Id = 1,
                   Description = "Pokemon tipo planta de la primera generacion, te lanza un látigo cepa y te deja moratón"
               },
               new Pokemon("Charmander")
               {
                   Id = 2,
                   Description = "Pokemon tipo de agua de la primera generación, es el pokemon favorito de los verdaderos amantes de pokemon."
               },
               new Pokemon("Squirtle")
               {
                   Id = 3,
                   Description = "Pokemon tipo de agua de la primera generación, tiene una concha a su espalda y está siempre calmado."
               });

            modelBuilder.Entity<Attack>()
             .HasData(
               new Attack("Placaje")
               {
                   Id = 1,
                   PokemonId = 1,
                   Description = "Placaje causa daño y no tiene ningún efecto secundario. Este movimiento tiene una potencia de 35 y una precisión del 95%."
               },
               new Attack("Látigo Cepa")
               {
                   Id = 2,
                   PokemonId = 1,
                   Description = "Látigo cepa causa daño y no tiene ningún efecto secundario. El movimiento tiene una potencia de 35 y 10 PP."
               },
                 new Attack("Látigo")
                 {
                     Id = 3,
                     PokemonId = 2,
                     Description = "Látigo baja en un nivel la defensa del oponente. En combates dobles y triples afecta a todos los oponentes adyacentes. No afecta a Pokémon con las habilidades cuerpo puro, humo blanco o sacapecho.\r\n\r\n"
                 },
               new Attack("Lanzallamas")
               {
                   Id = 4,
                   PokemonId = 2,
                   Description = "Lanzallamas causa daño y tiene una probabilidad del 10% de quemar al objetivo. Lanzallamas tiene una potencia de 95.\r\n"
               },
               new Attack("Arañazo")
               {
                   Id = 5,
                   PokemonId = 3,
                   Description = "Arañazo causa daño y no tiene ningún efecto secundario."
               },
               new Attack("Pistola Agua")
               {
                   Id = 6,
                   PokemonId = 3,
                   Description = "Pistola agua causa daño y no tiene ningún efecto secundario."
               }
               );
            base.OnModelCreating(modelBuilder);
        }

    }
}
