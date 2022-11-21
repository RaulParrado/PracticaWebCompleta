using AutoMapper;

namespace Beca.PokemonInfo.API.Profiles
{
    public class AttackProfile : Profile
    {
        public AttackProfile()
        {
            CreateMap<Entities.Attack, Models.AttackDto>();
            CreateMap<Models.AttackForCreateOrUpdateDto, Entities.Attack>();
            CreateMap<Models.AttackDto, Entities.Attack>();
            CreateMap<Entities.Attack, Models.AttackForCreateOrUpdateDto>();
        }
    }
}
