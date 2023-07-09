using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specfications
{
    public class ProductWithFiltration:BaseSpecfication<Product>
    {
        public ProductWithFiltration(ProductSpecParams specParams):base(p =>
            (!specParams.BrandId.HasValue || p.ProductBrandId == specParams.BrandId) &&
            (!specParams.TypeId.HasValue || p.ProductTypeId == specParams.TypeId)&&
        string.IsNullOrEmpty(specParams.Search)||p.Name.ToLower().Contains(specParams.Search)
            )
        {

        }
    }
}
