using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data
{
	public static class StoreContextSeed
	{

		public static async Task SeedAsync(StoreContext Dbcontext)
		{
			if (!Dbcontext.ProductBrands.Any())
			{
				var BrandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
				var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);

				if (Brands?.Count > 0)
				{
					foreach (var brand in Brands)
					{
						await Dbcontext.Set<ProductBrand>().AddAsync(brand);
					}
					await Dbcontext.SaveChangesAsync();
				}
			}


			if (!Dbcontext.ProductTypes.Any())
			{
				var TypeData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
				var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);

				if (Types?.Count > 0)
				{
					foreach (var type in Types)
					{
						await Dbcontext.Set<ProductType>().AddAsync(type);
					}
					await Dbcontext.SaveChangesAsync();
				}
			}

			if (!Dbcontext.Products.Any())
			{
				var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
				var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);

				if (Products?.Count > 0)
				{
					foreach (var product in Products)
					{
						await Dbcontext.Set<Product>().AddAsync(product);
					}
					await Dbcontext.SaveChangesAsync();
				}
			}

            if (!Dbcontext.DeliveryMethods.Any())
            {
                var DeliveryMethodsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);

                if (DeliveryMethods?.Count > 0)
                {
                    foreach (var DeliveryMethod in DeliveryMethods)
                    {
                        await Dbcontext.Set<DeliveryMethod>().AddAsync(DeliveryMethod);
                    }
                    await Dbcontext.SaveChangesAsync();
                }
            }

        }
	}
}
