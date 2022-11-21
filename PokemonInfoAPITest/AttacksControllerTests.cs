using AutoMapper;
using Beca.PokemonInfo.API.Controllers;
using Beca.PokemonInfo.API.Entities;
using Beca.PokemonInfo.API.Services;
using Beca.PokemonInfo.API.Profiles;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using Beca.PokemonInfo.API.Models;
using System.Diagnostics;

namespace PokemonInfoAPITest
{
    public class AttacksControllerTests
    {
        private AttacksController _attacksController;
        private HttpContext _httpContext;

        public AttacksControllerTests()
        {

            var MockRepository = new Mock<IPokemonInfoRepository>();
            var MockLogger = new Mock<ILogger<AttacksController>>();

            var mapperConf = new MapperConfiguration(x => x.AddProfile<AttackProfile>());
            var mapper = new Mapper(mapperConf);

            _httpContext = new DefaultHttpContext();

            MockRepository.Setup(m => m.GetPokemonAsync(1, It.IsAny<bool>()))
                        .ReturnsAsync(new Pokemon("Mew") { Description = "Un maquina" });

            MockRepository.Setup(m => m.PokemonExistsAsync(1))
                    .ReturnsAsync(true);

            MockRepository.Setup(m => m.PokemonExistsAsync(100))
                        .ReturnsAsync(false);

            MockRepository.Setup(m => m.GetAttacksForPokemonAsync(1))
                        .ReturnsAsync((new List<Attack>() {
                        new Attack("Ataque 1") { Description = "Te ataca con un 1" },
                        new Attack("Ataque 2") { Description = "Te ataca con un 2 y te aturde" } }
                        ));

            MockRepository.Setup(m => m.GetAttackForPokemonAsync(1, 1))
                    .ReturnsAsync(new Attack("Ataque 1") { Description = "Te ataca con un 1" });

            MockRepository.Setup(m => m.DeleteAttack(It.IsAny<Attack>()));

            _attacksController = new AttacksController(MockLogger.Object,MockRepository.Object, mapper);

            _attacksController.ControllerContext = new ControllerContext()
            {
                HttpContext = _httpContext,
            };


        }
        [Fact]
        public async void GetPokemons_OkResult_MustReturn1Attack()
        {
            var ataque = await _attacksController.GetAttack(1,1);
            Debug.WriteLine("HOLA"+ataque);
            var okResult = Assert.IsType <OkObjectResult> (ataque);
            Debug.WriteLine(okResult);
            var DTO = Assert.IsAssignableFrom<AttackDto>(okResult.Value);
            Assert.Equal("Ataque 1", DTO.Name);
            Assert.NotNull(ataque);
        }

        [Fact]
        public async void GetPokemons_OkResult_MustReturnAttacks()
        {
            var ataque1 = await _attacksController.GetAttacks(1);

            var actionResult = Assert.IsType<ActionResult<IEnumerable<AttackDto>>>(ataque1);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var DTOs = Assert.IsAssignableFrom<IEnumerable<AttackDto>>(okResult.Value);
            Assert.Equal(2, DTOs.Count());
            Assert.Equal("Ataque 1", DTOs.First().Name);
            Assert.NotNull(ataque1);
        }

        [Fact]
        public async void DeleteAttack_MustReturnNoContent()
        {
            ActionResult res = await _attacksController.DeleteAttack(1, 1);

            Assert.IsType<NoContentResult>(res);
        }
    }
}