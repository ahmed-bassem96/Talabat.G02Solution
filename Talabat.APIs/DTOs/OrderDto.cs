using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.APIs.DTOs
{
    public class OrderDto
    {
        public string basketId { get; set; }
        public int DeliveryMethodId { get; set; }

        public OrderAddressDto ShippingAddress { get; set; }
    }
}
