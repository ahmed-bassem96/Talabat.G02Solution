using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.APIs.Controllers
{
    public class BasketController : ApiBaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        [HttpGet]//GET: api/Baskets?id=1
   
    public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string basketId) {

         var basket=await _basketRepository.GetBasketAsync(basketId);

            return basket is null? new CustomerBasket(basketId) : basket;
        
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket=_mapper.Map<CustomerBasketDto,CustomerBasket>(basket);

         var CreatedOrUpdatedBasket= await  _basketRepository.UpdateBasketAsync(mappedBasket);

            if(CreatedOrUpdatedBasket is null) {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(CreatedOrUpdatedBasket);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string basketId)
        {
            return await _basketRepository.DeleteBasketAsync(basketId);
        }
       
    }
}
