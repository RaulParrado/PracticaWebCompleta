using AutoMapper;
using Beca.PokemonInfo.API.Controllers;
using Beca.PokemonInfo.API.Entities;
using Beca.PokemonInfo.API.Models;
using Beca.PokemonInfo.API.Profiles;
using Beca.PokemonInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
namespace PokemonInfoAPITest
{
    public class PokemonControllerTests: IDisposable
    {
        private PokemonsController _pokemonsController;
        private HttpContext _httpContext;
        public PokemonControllerTests()
        {
            //var MockLogger = new Mock<ILogger<PokemonsController>>();
            var MockRepository = new Mock<IPokemonInfoRepository>();
            var mapper = new Mapper(new MapperConfiguration(x => x.AddProfile<PokemonProfile>()));

            _httpContext = new DefaultHttpContext();
            //Si recibe (1,bool), devuelve un pokemon
            MockRepository.Setup(m => m.GetPokemonAsync(1, It.IsAny<bool>()))
                .ReturnsAsync(new Pokemon("Mew") { Description = "Un maquina" });

            //Si recibe (999,bool), devuelve un null
            MockRepository.Setup(m => m.GetPokemonAsync(999, It.IsAny<bool>()))
                .ReturnsAsync((Pokemon)null);

            //Esta funcion mockeará que se devuelvan 2 pokemons
            MockRepository.Setup(m => m.GetPokemonsAsync(null, null, 1, 10))
                 .ReturnsAsync((new List<Pokemon>()
                 {
                    new Pokemon("Bulbasaur"){Description="Tipo planta"},
                    new Pokemon("Charmander"){Description="Tipo fuego"}
                 }, new PaginationMetadata(2, 10, 1)));

            _pokemonsController = new PokemonsController(MockRepository.Object, mapper);

            _pokemonsController.ControllerContext = new ControllerContext()
            {
                HttpContext = _httpContext
            };
        }
        public void Dispose()
        {

        }
        [Fact]
        public async Task GetPokemons_OkResult_MustReturnPokemons()
        {
            var pokemons = await _pokemonsController.GetPokemons(null, null, 1, 10);

            var actionResult = Assert.IsType<ActionResult<IEnumerable<PokemonWithoutAttacksDto>>>(pokemons);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var DTOs = Assert.IsAssignableFrom<IEnumerable<PokemonWithoutAttacksDto>>(okResult.Value);
            Assert.Equal(2, DTOs.Count());
            Assert.Equal("Bulbasaur", DTOs.First().Name);
            Assert.Equal("Charmander", DTOs.Last().Name);
        }

        [Fact]
        public async Task GetPokemon_OkResult_MustReturn1Pokemon()
        {
            var pokemon = await _pokemonsController.GetPokemon(1, true);
            var okResult = Assert.IsType<OkObjectResult>(pokemon);
            var dto = Assert.IsAssignableFrom<PokemonDto>(okResult.Value);
            Assert.Equal("Mew", dto.Name);
        }

        [Fact]
        public async Task GetPokemon_NotExistingPokemon_MustReturnPokemonNotFound()
        {
            var pokemon = await _pokemonsController.GetPokemon(999, false);

            Assert.IsType<NotFoundResult>(pokemon);
        }
    }
}