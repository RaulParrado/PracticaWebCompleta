using AutoMapper;
using Beca.PokemonInfo.API.Models;
using Beca.PokemonInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Beca.PokemonInfo.API.Controllers
{
    [ApiController]
    //[Authorize]
    //[ApiVersion("1.0")]
    //[ApiVersion("2.0")]
    [Route("api/pokemons")]
    public class PokemonsController : ControllerBase
    {
        private readonly IPokemonInfoRepository _pokemonInfoRepository;
        private readonly IMapper _mapper;
        const int maxPokemonsPageSize = 20;

        public PokemonsController(IPokemonInfoRepository pokemonInfoRepository,
            IMapper mapper)
        {
            _pokemonInfoRepository = pokemonInfoRepository ??
                throw new ArgumentNullException(nameof(pokemonInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }
        /// <summary>
        /// Get all pokemons and paginate the solution, with filtering
        /// </summary>
        /// <param name="name">Filter by pokemon name</param>
        /// <param name="searchQuery">Filter by search query</param>
        /// /// <param name="pageNumber">Page number if results are > page size</param>
        /// /// <param name="pageSize">The quantity of items on a page</param>
        /// <returns>An ActionResult</returns>
        /// <response code="200">Returns the requested pokemons</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PokemonWithoutAttacksDto>>> GetPokemons(
            string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxPokemonsPageSize)
            {
                pageSize = maxPokemonsPageSize;
            }

            var (pokemonEntities, paginationMetadata) = await _pokemonInfoRepository
                .GetPokemonsAsync(name, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<PokemonWithoutAttacksDto>>(pokemonEntities));
        }

        /// <summary>
        /// Get a pokemon by id
        /// </summary>
        /// <param name="id">The id of the pokemon to get</param>
        /// <param name="includeAttacks">Whether or not to include the attacks</param>
        /// <returns>An IActionResult</returns>
        /// <response code="200">Returns the requested pokemon</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPokemon(
            int id, bool includeAttacks = false)
        {
            var pokemon = await _pokemonInfoRepository.GetPokemonAsync(id, includeAttacks);
            if (pokemon == null)
            {
                return NotFound();
            }

            if (includeAttacks)
            {
                return Ok(_mapper.Map<PokemonDto>(pokemon));
            }

            return Ok(_mapper.Map<PokemonWithoutAttacksDto>(pokemon));
        }
    }
}
