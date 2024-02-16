using AutoMapper;
using ICommerce.Catalog.Models;
using ICommerce.Contracts;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductResponse>().ReverseMap();

    }
}