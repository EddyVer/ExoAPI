using AutoMapper;
using ExoAPI.Dto;
using ExoAPI.Entitie;

namespace ExoAPI.Mapping;

public class MappingData : Profile
{
    public MappingData()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<UsagesCollection, UsagesCollectionDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Entrepot, EntrepotDto>().ReverseMap();
    }
}