using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BLL.Interfaces;
using Talabat.DAL.Data;
using Talabat.DAL.Entities;
using Talabat.DAL.Entities.Order_Aggregate;

namespace Talabat.BLL.Services
{
    public class OrderSevice : IOrderService
    {
        private readonly StoreContext storeContext;
        private readonly IBasketRepository basketRepository;
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<DeliveryMethod> delivaryRepo;

        public OrderSevice(StoreContext storeContext,IBasketRepository basketRepository,
            IGenericRepository<Product>ProductRepo,IGenericRepository<DeliveryMethod> DelivaryRepo)
        {
            this.storeContext = storeContext;
            this.basketRepository = basketRepository;
            productRepo = ProductRepo;
            delivaryRepo = DelivaryRepo;
        }

        public async Task<Order> CreateOrder(string buyerEmail, Address address, int deliveryMethodId, string BasketId)
        {
            //1- get basket by id
            var basket = await basketRepository.GetCustomerBasket(BasketId);
            //2- get items from basket
            var items =new List<OrderItem>();
            decimal subtotal = 0;
            foreach(var i in basket.Items)
            {
                var p = await productRepo.GetByIdAsync(i.Id);
                var ProductOrderItem = new ProductOrderItem(i.Id, p.Name, p.PictureUrl);
                var orderItem=new OrderItem(ProductOrderItem,p.Price,i.Quantity);
                subtotal+=(p.Price)* (i.Quantity);
                items.Add(orderItem);
            }
            var delivaryMethod = await delivaryRepo.GetByIdAsync(deliveryMethodId);
            var order = new Order(buyerEmail, address, delivaryMethod, items, subtotal);
            await storeContext.Set<Order>().AddAsync(order);
            return order;
        }
    }
}
