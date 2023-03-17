using AutoMapper;
using Talabat.DAL.Entities;
using Talabat.DAL.Entities.Identity;
using Talabat.Pl.Dtos;

namespace Talabat.Pl.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(p => p.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(p => p.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(p => p.PictureUrl, o => o.MapFrom<ProductResolver>());

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasketDto>();
            CreateMap<BasketItemDto,BasketItemDto>();
        }
    }
}
