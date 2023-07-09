using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Core.Specfications;

namespace Talabat.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepository,IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
          
        }
        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId,
            Address ShippingAddress)
        {
            // 1. Get basket From Basket Repo
            var basket = await _basketRepository.GetBasketAsync(BasketId);

            // 2. Get Selected Items at Basket From Product Repo
            var orderItems=new List<OrderItem>();
            if(basket?.Items?.Count > 0)
            {
                foreach(var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    var productItemOrdered=new ProductItemOrdered(product.Id,product.Name,product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrdered, item.Quantity, product.Price);
                    orderItems.Add(orderItem);
                }
            }

            // 3. Calculate SubTotal
            var subTotal=orderItems.Sum(X=>X.Price*X.Quantity);

            // 4. Get Delivery Method From Delivery Method Repo
            var deliveryMethod= await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);

            // 5. Create Order
            var order = new Order(BuyerEmail,ShippingAddress,deliveryMethod,orderItems,subTotal);
           await _unitOfWork.Repository<Order>().Add(order);
            // 6. Save To Database [TODO]
            var result= await _unitOfWork.Complete();

            if (result <= 0) { return null; }
            return order;

            
        }

        public Task<Order> GetOrderByIdForUserAsync(string BuyerEmail, int OrderId)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string BuyerEmail)
        {
            var spec=new OrderSpecfication(BuyerEmail);
            var orders= await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return orders;
        }
    }
}
