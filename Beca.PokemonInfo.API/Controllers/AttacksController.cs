using AutoMapper;
using Beca.PokemonInfo.API.Models;
using Beca.PokemonInfo.API.Services;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Beca.PokemonInfo.API.Controllers
{
    [Route("api/pokemons/{pokemonId}/attacks")]
    [ApiController]
    public class AttacksController : ControllerBase
    {
        private readonly ILogger<AttacksController> _logger;
        private readonly IPokemonInfoRepository _pokemonInfoRepository;
        private readonly IMapper _mapper;

        public object Debug { get; private set; }

        public AttacksController(ILogger<AttacksController> logger,
            IPokemonInfoRepository pokemonInfoRepository,
            IMapper mapper)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            _pokemonInfoRepository = pokemonInfoRepository ??
                throw new ArgumentNullException(nameof(pokemonInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }
        /// <summary>
        /// Get all attacks from a pokemon by pokemonId
        /// </summary>
        /// <param name="pokemonId">The pokemon we want to see the attacks of</param>
        /// <returns>An ActionResult</returns>
        /// <response code="200">Returns the requested attacks</response>
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<AttackDto>>> GetAttacks(
            int pokemonId)
        {

            if (!await _pokemonInfoRepository.PokemonExistsAsync(pokemonId))
            {
                _logger.LogInformation(
                    $"Pokemon with id {pokemonId} wasn't found when accessing attacks.");
                return NotFound();
            }

            var attacksForPokemon = await _pokemonInfoRepository
                .GetAttacksForPokemonAsync(pokemonId);

            return Ok(_mapper.Map<IEnumerable<AttackDto>>(attacksForPokemon));
        }
        /// <summary>
        /// Get one attack from a pokemon by pokemonId and attack id
        /// </summary>
        /// <param name="pokemonId">The pokemon we want to see the attacks of</param>
        /// <param name="attackId">The attack id we want</param>
        /// <returns>An ActionResult</returns>
        /// <response code="200">Returns the requested pokemon attack</response>
        [HttpGet("{attackId}", Name = "GetAttack")]
        public async Task<IActionResult> GetAttack(
            int pokemonId, int attackId)
        {
            if (!await _pokemonInfoRepository.PokemonExistsAsync(pokemonId))
            {
                return NotFound();
            }
            var attack = await _pokemonInfoRepository
                .GetAttackForPokemonAsync(pokemonId, attackId);

            if (attack == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AttackDto>(attack));
        }
        /// <summary>
        /// Create an attack and attach it to a pokemon
        /// </summary>
        /// <param name="attack">The values to create the attack</param>
        /// <returns>An ActionResult</returns>
        /// <response code="200">Returns the requested pokemons</response>
        [HttpPost()]
        public async Task<ActionResult<AttackDto>> CreateAttack(
           int pokemonId,
           AttackForCreateOrUpdateDto attack)
        {
            if (!await _pokemonInfoRepository.PokemonExistsAsync(pokemonId))
            {
                return NotFound();
            }

            var finalAttack = _mapper.Map<Entities.Attack>(attack);

            await _pokemonInfoRepository.AddAttackForPokemonAsync(
                pokemonId, finalAttack);

            await _pokemonInfoRepository.SaveChangesAsync();

            var createdAttackToReturn =
                _mapper.Map<Models.AttackDto>(finalAttack);

            return CreatedAtRoute("GetAttack",
                 new
                 {
                     pokemonId = pokemonId,
                     attackId = createdAttackToReturn.Id
                 },
                 createdAttackToReturn);
        }
        /// <summary>
        /// Update an attack by ids
        /// </summary>
        /// <param name="pokemonId">The pokemon we want to see the attacks of</param>
        /// <param name="attackId">The attack id we want</param>
        /// <param name="attack">The values to update the attack</param>
        /// <returns>An ActionResult</returns>
        /// <response code="200">Returns the requested pokemons</response>
        [HttpPut("{attackId}")]
        public async Task<ActionResult> UpdateAttack(int pokemonId, int attackId,
            AttackForCreateOrUpdateDto attack)
        {
            if (!await _pokemonInfoRepository.PokemonExistsAsync(pokemonId))
            {
                return NotFound();
            }

            var attackEntity = await _pokemonInfoRepository
                .GetAttackForPokemonAsync(pokemonId, attackId);
            if (attackEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(attack, attackEntity);

            await _pokemonInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Partially updates an attack by ids
        /// </summary>
        /// <param name="pokemonId">The pokemon we want to see the attacks of</param>
        /// <param name="attackId">The attack id we want</param>
        /// <param name="patchDocument">The values to update the attack</param>
        /// <returns>An ActionResult</returns>
        /// <response code="200">Returns the requested pokemons</response>
        [HttpPatch("{attackId}")]
        public async Task<ActionResult> PartiallyUpdateAttack(
            int pokemonId, int attackId,
            JsonPatchDocument<AttackForCreateOrUpdateDto> patchDocument)
        {
            if (!await _pokemonInfoRepository.PokemonExistsAsync(pokemonId))
            {
                return NotFound();
            }

            var attackEntity = await _pokemonInfoRepository
                .GetAttackForPokemonAsync(pokemonId, attackId);
            if (attackEntity == null)
            {
                return NotFound();
            }

            var attackToPatch = _mapper.Map<AttackForCreateOrUpdateDto>(
                attackEntity);

            patchDocument.ApplyTo(attackToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(attackToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(attackToPatch, attackEntity);
            await _pokemonInfoRepository.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Delete an attack by attack id
        /// </summary>
        /// <param name="attackId">The attack id we want to delete</param>
        /// <param name="pokemonId">The pokemon id we want to delete an attack from</param>
        /// <returns>An ActionResult</returns>
        /// <response code="204">Returns no content</response>
        /// <response code="404">Returns notFound if the attack id does not exist</response>
        [HttpDelete("{attackId}")]
        public async Task<ActionResult> DeleteAttack(
            int pokemonId, int attackId)
        {
            if (!await _pokemonInfoRepository.PokemonExistsAsync(pokemonId))
            {
                return NotFound();
            }

            var attackEntity = await _pokemonInfoRepository
                .GetAttackForPokemonAsync(pokemonId, attackId);
            if (attackEntity == null)
            {
                return NotFound();
            }

            _pokemonInfoRepository.DeleteAttack(attackEntity);
            await _pokemonInfoRepository.SaveChangesAsync();


            return NoContent();
        }

    }
}
