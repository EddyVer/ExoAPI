using AutoMapper;
using ExoAPI.Dto;
using ExoAPI.Entitie;

namespace ExoAPI.Mapping;

public class MappingProduit : Profile
{
    public MappingProduit()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<UsagesCollection, UsagesCollectionDto>().ReverseMap();
    }
}