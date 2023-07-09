using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {

        }
        public Order(string buyerEmail, Address shipingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShipingAddress = shipingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }=DateTimeOffset.Now;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Address ShipingAddress { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }///Navigational Property [One]

        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();//Navigational Property [Many]


        public decimal SubTotal { get; set; }  //Price of product* Quantity  

       // [NotMapped]
       // public decimal Total { get=> SubTotal+DeliveryMethod.Cost;  }   //SubTotal + Delivery Method Cost

        public decimal GetTotal()
        {
           return SubTotal + DeliveryMethod.Cost;
        }

        public string PaymentIntentId { get; set; } = string.Empty;
    }

}
