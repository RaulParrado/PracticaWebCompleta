using Beca.PokemonInfo.API.Services;
using Beca.PokemonInfo.API.DbContexts;
using Beca.PokemonInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Beca.PokemonInfo.API.Services
{
    public class PokemonInfoRepository : IPokemonInfoRepository
    {
        private readonly PokemonInfoContext _context;

        public PokemonInfoRepository(PokemonInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Pokemon>> GetPokemonsAsync()
        {
            return await _context.Pokemons.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<bool> PokemonNameMatchesPokemonId(string? pokemonName, int pokemonId)
        {
            return await _context.Pokemons.AnyAsync(c => c.Id == pokemonId && c.Name == pokemonName);
        }

        public async Task<(IEnumerable<Pokemon>, PaginationMetadata)> GetPokemonsAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            // collection to start from
            var collection = _context.Pokemons as IQueryable<Pokemon>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery)
                    || (a.Description != null && a.Description.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(c => c.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }



        public async Task<Pokemon?> GetPokemonAsync(int pokemonId, bool includeAttacks)
        {
            if (includeAttacks)
            {
                return await _context.Pokemons.Include(c => c.Attacks)
                    .Where(c => c.Id == pokemonId).FirstOrDefaultAsync();
            }

            return await _context.Pokemons
                  .Where(c => c.Id == pokemonId).FirstOrDefaultAsync();
        }

        public async Task<bool> PokemonExistsAsync(int pokemonId)
        {
            return await _context.Pokemons.AnyAsync(c => c.Id == pokemonId);
        }

        public async Task<Attack?> GetAttackForPokemonAsync(
            int pokemonId,
            int attackId)
        {
            return await _context.Attacks
               .Where(a => a.PokemonId == pokemonId && a.Id == attackId).OrderBy(x => x.Name)
               .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Attack>> GetAttacksForPokemonAsync(
            int pokemonId)
        {
            return await _context.Attacks
                           .Where(p => p.PokemonId == pokemonId).ToListAsync();
        }

        public async Task AddAttackForPokemonAsync(int pokemonId,
            Attack attack)
        {
            var pokemon = await GetPokemonAsync(pokemonId, false);
            if (pokemon != null)
            {
                pokemon.Attacks.Add(attack);
            }
        }

        public void DeleteAttack(Attack attack)
        {
            _context.Attacks.Remove(attack);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
