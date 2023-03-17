using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.DAL.Entities;
using Talabat.Pl.Dtos;

namespace Talabat.Pl.Helper
{
    public class ProductResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration configuration;

        public ProductResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return configuration["LocalHost"] + source.PictureUrl;
            return null;
        }
    }
}
