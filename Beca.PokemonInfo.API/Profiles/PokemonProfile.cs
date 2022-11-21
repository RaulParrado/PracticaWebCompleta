using AutoMapper;

namespace Beca.PokemonInfo.API.Profiles
{
    public class PokemonProfile : Profile
    {
        public PokemonProfile()
        {
            CreateMap<Entities.Pokemon, Models.PokemonWithoutAttacksDto>();
            CreateMap<Entities.Pokemon, Models.PokemonDto>();
        }
    }
}