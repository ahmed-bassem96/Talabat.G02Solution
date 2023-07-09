using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories;

namespace Talabat.Core
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        /* public IGenericRepository<Product> ProdcutsRepo { get; set; }
         public IGenericRepository<ProductBrand> BrandsRepo { get; set; }
         public IGenericRepository<ProductType> TypesRepo { get; set; }
         public IGenericRepository<Order> OrderRepo { get; set; }

         public IGenericRepository<OrderItem> OrderItemRepo { get; set; }
         public IGenericRepository<DeliveryMethod> DeliveryMethodsRepo { get; set; }*/

        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        Task<int> Complete();


    }
}
