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
                   Description = "Pokemon tipo planta de la primera generacion, te lanza un l�tigo cepa y te deja morat�n"
               },
               new Pokemon("Charmander")
               {
                   Id = 2,
                   Description = "Pokemon tipo de agua de la primera generaci�n, es el pokemon favorito de los verdaderos amantes de pokemon."
               },
               new Pokemon("Squirtle")
               {
                   Id = 3,
                   Description = "Pokemon tipo de agua de la primera generaci�n, tiene una concha a su espalda y est� siempre calmado."
               });

            modelBuilder.Entity<Attack>()
             .HasData(
               new Attack("Placaje")
               {
                   Id = 1,
                   PokemonId = 1,
                   Description = "Placaje causa da�o y no tiene ning�n efecto secundario. Este movimiento tiene una potencia de 35 y una precisi�n del 95%."
               },
               new Attack("L�tigo Cepa")
               {
                   Id = 2,
                   PokemonId = 1,
                   Description = "L�tigo cepa causa da�o y no tiene ning�n efecto secundario. El movimiento tiene una potencia de 35 y 10 PP."
               },
                 new Attack("L�tigo")
                 {
                     Id = 3,
                     PokemonId = 2,
                     Description = "L�tigo baja en un nivel la defensa del oponente. En combates dobles y triples afecta a todos los oponentes adyacentes. No afecta a Pok�mon con las habilidades cuerpo puro, humo blanco o sacapecho.\r\n\r\n"
                 },
               new Attack("Lanzallamas")
               {
                   Id = 4,
                   PokemonId = 2,
                   Description = "Lanzallamas causa da�o y tiene una probabilidad del 10% de quemar al objetivo. Lanzallamas tiene una potencia de 95.\r\n"
               },
               new Attack("Ara�azo")
               {
                   Id = 5,
                   PokemonId = 3,
                   Description = "Ara�azo causa da�o y no tiene ning�n efecto secundario."
               },
               new Attack("Pistola Agua")
               {
                   Id = 6,
                   PokemonId = 3,
                   Description = "Pistola agua causa da�o y no tiene ning�n efecto secundario."
               }
               );
            base.OnModelCreating(modelBuilder);
        }

    }
}
