using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.DAL.Entities.Order_Aggregate
{
    public class Order:BaseEntity
    {
        public Order()
        {
        }

        public Order(string buyerEmail, Address address, DeliveryMethod deliveryMethod, IEnumerable<OrderItem> items, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            Address = address;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset Date { get; set; }= DateTimeOffset.Now;
        public Address Address { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public DeliveryMethod DeliveryMethod { get; set; }
        public IEnumerable<OrderItem> Items { get; set;}
        public decimal SubTotal { get; set; }
        public int PaymentIntentId { get; set; }

        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Cost;
    }
}
