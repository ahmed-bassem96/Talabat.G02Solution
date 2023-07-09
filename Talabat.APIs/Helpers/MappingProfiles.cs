using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Identity;

namespace Talabat.APIs.Helpers
{
	public class MappingProfiles:Profile
	{
		public MappingProfiles()
		{
			CreateMap<Product, ProductToReturnDto>()
				.ForMember(d => d.ProductBrand, O => O.MapFrom(s => s.ProductBrand.Name))
				.ForMember(d => d.ProductType, O => O.MapFrom(s => s.ProductType.Name));
			//.ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());

			CreateMap<Core.Identity.Address, AddressDto>().ReverseMap();

			CreateMap<CustomerBasketDto, CustomerBasket>();
			CreateMap<BasketItemDto, BasketItem>();
			CreateMap<OrderAddressDto,Core.Entities.Order_Aggregate.Address>();
		}
	}
}
