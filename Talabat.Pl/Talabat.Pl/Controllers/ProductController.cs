using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.BLL.Interfaces;
using Talabat.BLL.Specification;
using Talabat.BLL.Specification.ProductSpecification;
using Talabat.DAL.Entities;
using Talabat.Pl.Dtos;
using Talabat.Pl.Error;
using Talabat.Pl.Helper;

namespace Talabat.Pl.Controllers
{
    
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> productRepository;
        private readonly IGenericRepository<ProductBrand> brandRepo;
        private readonly IGenericRepository<ProductType> typeRepo;
        private readonly IMapper mapper;

        public ProductController(IGenericRepository<Product> productRepository,
            IGenericRepository<ProductBrand> brandRepo,IGenericRepository<ProductType> typeRepo,IMapper mapper)
        {
            this.productRepository = productRepository;
            this.brandRepo = brandRepo;
            this.typeRepo = typeRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>>GetAllProducts([FromQuery]ProductParams productParams)
        {
            var spec = new ProductSpecification(productParams);
            var products = await productRepository.GetAllWithSpecAsync(spec);
            var mapped = mapper.Map<IReadOnlyList<ProductDto>>(products);
            var countSpec = new ProductWithFiltersForCountSpec(productParams);
            var count = await productRepository.GetCountAsync(countSpec);
            var result=new Pagination<ProductDto>() { PageIndex=productParams.PageIndex, PageSize=productParams.PageSize, Count= count, Data=mapped };
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var spec = new ProductSpecification(id);
            var product = await productRepository.GetByIdWithSpecAsync(spec);
            if (product == null) return NotFound(new ApiErrorResponse(404));
            var mapped=mapper.Map<ProductDto>(product);
            return Ok(mapped);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var brands = await brandRepo.GetAllAsync();
            return Ok(brands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var types = await typeRepo.GetAllAsync();
            return Ok(types);
        }
    }
}
