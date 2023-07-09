using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;

        private Hashtable _repositories;

       /* public IGenericRepository<Product> ProdcutsRepo { get; set; }
        public IGenericRepository<ProductBrand> BrandsRepo { get; set; }
        public IGenericRepository<ProductType> TypesRepo { get; set; }
        public IGenericRepository<Order> OrderRepo { get; set; }
        public IGenericRepository<OrderItem> OrderItemRepo { get; set; }
        public IGenericRepository<DeliveryMethod> DeliveryMethodsRepo { get; set; }*/

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
          /*  ProdcutsRepo = new GenericRepository<Product>(dbContext);
            BrandsRepo=new GenericRepository<ProductBrand>(dbContext);
            TypesRepo=new GenericRepository<ProductType>(dbContext);
            OrderRepo=new GenericRepository<Order>(dbContext);
            OrderItemRepo=new GenericRepository<OrderItem>(dbContext);
            DeliveryMethodsRepo=new GenericRepository<DeliveryMethod>(dbContext);*/
            _repositories= new Hashtable();
        }

        public async Task<int> Complete()
       => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
       =>await _dbContext.DisposeAsync();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repository=new GenericRepository<TEntity>(_dbContext);
                _repositories.Add(type, repository);
            }
            return _repositories[type] as IGenericRepository<TEntity>;
        }
    }
}
