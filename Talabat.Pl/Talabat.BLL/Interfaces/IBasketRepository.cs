using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetCustomerBasket(string BasketId);
        Task<CustomerBasket> UpdateCustomerBasket(CustomerBasket customerBasket);
        Task<bool> DeleteCustomerBasket(string BasketId);
    }
}
