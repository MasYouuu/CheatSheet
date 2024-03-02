using AutoMapper;
using CheatSheet.Domain.Entities;

namespace CheatSheet.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<GardenDTO, Garden>();
            CreateMap<Garden, GardenDTO>();
            CreateMap<FlowerDTO, Flower>();
            CreateMap<Flower, FlowerDTO>();
            CreateMap<TreeDTO, Tree>();
            CreateMap<Tree, TreeDTO>();
            CreateMap<OwnerDTO, Owner>();
            CreateMap<Owner, OwnerDTO>();
        }
    }
}
