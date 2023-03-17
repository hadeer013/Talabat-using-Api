using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.DAL.Entities.Order_Aggregate
{
    public class OrderItem:BaseEntity
    {
        public OrderItem()
        {
        }

        public OrderItem(ProductOrderItem productOrder, decimal price, int quantity)
        {
            this.productOrder = productOrder;
            Price = price;
            Quantity = quantity;
        }

        public ProductOrderItem productOrder { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
