using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specfications
{
	public class ProductWithBrandAndTypeSpecfication:BaseSpecfication<Product>
	{
		public ProductWithBrandAndTypeSpecfication(ProductSpecParams specParams)
			:base(p=>
			(!specParams.BrandId.HasValue||p.ProductBrandId== specParams.BrandId) &&
			(!specParams.TypeId.HasValue||p.ProductTypeId== specParams.TypeId)&&
            string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search)
            )
		{
			if (!string.IsNullOrEmpty(specParams.Sort))
			{
				switch (specParams.Sort)
				{
					case "priceAsc":
						AddOrderBy(P => P.Price);
						break;

					case "priceDesc":
						AddOrderByDesc(P => P.Price);

						break;

					default: 
						AddOrderBy(P => P.Name);
						break;
				}
			}

			/*
			 * total products=100
			 * Page Size=10
			 * Page Index=3
			 * 
			 * skip=10*(3-1) take=10
			 */
			ApplyPagination(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
			Includes.Add(P => P.ProductBrand);
			Includes.Add(P => P.ProductType);
		}

		public ProductWithBrandAndTypeSpecfication(int id):base(P=>P.Id==id)
		{
			Includes.Add(P => P.ProductBrand);
			Includes.Add(P => P.ProductType);
		}
	}
}
