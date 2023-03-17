using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.BLL.Interfaces;
using Talabat.DAL.Entities;
using Talabat.Pl.Dtos;

namespace Talabat.Pl.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult>GetBasketById(string id)
        {
            var basket = await basketRepository.GetCustomerBasket(id);
            return Ok(basket??new CustomerBasket(id));
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto customerBasket)
        {
            var mapped = mapper.Map<CustomerBasketDto, CustomerBasket>(customerBasket);
            var basket = await basketRepository.UpdateCustomerBasket(mapped);
            return Ok(basket);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>>DeleteBasket(string id)
        {
            return await basketRepository.DeleteCustomerBasket(id);
        }
    }
}
