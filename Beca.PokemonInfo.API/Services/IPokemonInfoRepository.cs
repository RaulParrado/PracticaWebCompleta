using Beca.PokemonInfo.API.Entities;

namespace Beca.PokemonInfo.API.Services
{
    public interface IPokemonInfoRepository
    {
        Task<IEnumerable<Pokemon>> GetPokemonsAsync();
        Task<(IEnumerable<Pokemon>, PaginationMetadata)> GetPokemonsAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<Pokemon?> GetPokemonAsync(int pokemonId, bool includeAttacks);
        Task<bool> PokemonExistsAsync(int pokemonId);
        Task<IEnumerable<Attack>> GetAttacksForPokemonAsync(int pokemonId);
        Task<Attack?> GetAttackForPokemonAsync(int pokemonId, 
            int attackId);
        Task AddAttackForPokemonAsync(int pokemonId, Attack attack);
        void DeleteAttack(Attack attack);
        Task<bool> PokemonNameMatchesPokemonId(string? cityName, int pokemonId);
        Task<bool> SaveChangesAsync();
    }
}
