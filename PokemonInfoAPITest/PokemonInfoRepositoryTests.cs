using Beca.PokemonInfo.API.DbContexts;
using Beca.PokemonInfo.API.Entities;
using Beca.PokemonInfo.API.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PokemonInfoAPITest
{
    public class PokemonInfoRepositoryTests
    {
        private PokemonInfoRepository _pokemonInfoRepository;
        public PokemonInfoRepositoryTests()
        {
            var connection = new SqliteConnection("Data source=:memory:");
            connection.Open();

            var optionsBuilder = new DbContextOptionsBuilder<PokemonInfoContext>().UseSqlite(connection);
            var dbContext = new PokemonInfoContext(optionsBuilder.Options);
            dbContext.Database.Migrate();

            _pokemonInfoRepository = new PokemonInfoRepository(dbContext);
        }

        [Fact]
        public async Task GetPokemonsAsync_ExistingPokemons_MustReturnAllPokemons()
        {
            //// Arrange 
            // Act 
            var (pokemons, pMetadata) = await _pokemonInfoRepository.GetPokemonsAsync(null, null, 1, 10);
            // Assert
            Assert.NotNull(pokemons);
            Assert.Equal(3, pokemons.Count());

        }
        [Fact]
        public async Task GetPokemonsAsync_PaginationWith2Elements_MustReturn2PokemonsAndSamePagination()
        {
            var (pokemons, pMetadata) = await _pokemonInfoRepository.GetPokemonsAsync(null, null, 1, 2);
            Assert.NotNull(pokemons);
            Assert.Equal(2, pMetadata.TotalPageCount);
            Assert.Equal(3, pMetadata.TotalItemCount);
            Assert.Equal(1, pMetadata.CurrentPage);
            Assert.Equal(2, pokemons.Count());
        }

        [Fact]
        public async Task GetPokemonsAsync_GetPokemonsWithQuery_MustReturnCharmander()
        {
            string Query = "Charmander";
            var (pokemons, pMetadata) = await _pokemonInfoRepository.GetPokemonsAsync(Query, null, 1, 2);

            Assert.NotNull(pokemons);
            Assert.Single(pokemons);
            Assert.Equal(Query, pokemons.First().Name);
        }

        [Fact]
        public async Task GetPokemonAsync_GetPokemonsWith_MustReturnPokemonWithAttacks()
        {

            var pokemon = await _pokemonInfoRepository.GetPokemonAsync(1, true);

            Assert.Equal("Bulbasaur", pokemon.Name);
            Assert.NotNull(pokemon.Attacks);
        }

        [Fact]
        public async Task GetPokemonAsync_GetPokemonsWithoutAttacks_MustReturnPokemonWithNoAttacks()
        {

            var pokemon = await _pokemonInfoRepository.GetPokemonAsync(1, false);

            Assert.Equal("Bulbasaur", pokemon.Name);
            Assert.NotNull(pokemon);
            Assert.Empty(pokemon.Attacks);

        }
        [Fact]
        public async Task GetPokemonAsync_GetUnexistingPokemon_MustReturnNull()
        {
            var pokemon = await _pokemonInfoRepository.GetPokemonAsync(99, false);

            Assert.Null(pokemon);
        }



        [Fact]
        public async Task GetAttacksForPokemonAsync_GetAttacks_MustReturnTwoAttacks()
        {
            var pokemons = await _pokemonInfoRepository.GetPokemonAsync(1, true);

            Assert.NotNull(pokemons);
            Assert.Equal(2, pokemons.Attacks.Count());
        }


        [Fact]
        public async Task GetAttacksForPokemonAsync_GetAttacks_MustReturn2Attacks()
        {
            var attacks = await _pokemonInfoRepository.GetAttacksForPokemonAsync(1);

            Assert.NotNull(attacks);
            Assert.Equal("Placaje", attacks.First().Name);
            Assert.Equal("Látigo Cepa", attacks.Last().Name);
            Assert.Equal(2,attacks.Count());
        }


        [Fact]
        public async Task AddAttackForPokemonAsync_NewAttack_MustReturnOneMoreAttack()
        {
            Attack ataque = new Attack("Bimbazo")
            {
                Description = "Te da una colleja poco floja."
            };
            await _pokemonInfoRepository.AddAttackForPokemonAsync(1, ataque);
            var pokemon = await _pokemonInfoRepository.GetPokemonAsync(1, true);

            Assert.Equal(3, pokemon.Attacks.Count());

            // Guarda los cambios y comprueba que se añade un nuevo pokemon.
            await _pokemonInfoRepository.SaveChangesAsync();
            await _pokemonInfoRepository.AddAttackForPokemonAsync(1, ataque);
            var pokemon2 = await _pokemonInfoRepository.GetPokemonAsync(1, true);

            Assert.Equal(4, pokemon2.Attacks.Count());

        }

        [Fact]
        public async Task DeleteAttack_DeleteExistingAttackById_MustDeleteAttackButNoPokemon()
        {

            Attack? attack = await _pokemonInfoRepository.GetAttackForPokemonAsync(1, 1);
            _pokemonInfoRepository.DeleteAttack(attack);

            //Se comprueba que no sa borra hasta que no se guardan los datos
            Assert.NotNull(attack);
            await _pokemonInfoRepository.SaveChangesAsync();
            Attack? attack2 = await _pokemonInfoRepository.GetAttackForPokemonAsync(1, 1);


            Assert.Null(attack2);
        }
    }
}