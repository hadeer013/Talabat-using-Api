using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.BLL.Interfaces;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase dataBase;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            dataBase = redis.GetDatabase();
        }

        public Task<bool> DeleteCustomerBasket(string BasketId)
        {
            return dataBase.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket> GetCustomerBasket(string BasketId)
        {
            var value=await dataBase.StringGetAsync(BasketId);
            return value.IsNullOrEmpty?null:JsonSerializer.Deserialize<CustomerBasket>(value);
        }

        public async Task<CustomerBasket> UpdateCustomerBasket(CustomerBasket customerBasket)
        {
            var created = await dataBase.StringSetAsync(customerBasket.Id, JsonSerializer.Serialize(customerBasket), TimeSpan.FromDays(30));
            if(!created) return null;
            return await GetCustomerBasket(customerBasket.Id);
        }
    }
}
