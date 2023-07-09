using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specfications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T:BaseEntity
	{
		private readonly StoreContext _context;

		public GenericRepository(StoreContext context)
		{
			_context = context;
		}
		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			if (typeof(T) == typeof(Product))
		    {
				return (IReadOnlyList<T>)
					await _context.Set<Product>().Include(P=>P.ProductBrand).Include(P=>P.ProductType).ToListAsync();
				

			} 
		 return await _context.Set<T>().ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
		{
			return await ApplySpecfication(spec).ToListAsync();
		}

		

		public async Task<T> GetIdWithSpecAsync(ISpecification<T> spec)
		{
			return await ApplySpecfication(spec).FirstOrDefaultAsync();
		}
		private IQueryable<T> ApplySpecfication(ISpecification<T> spec)
		{
			return SpecficationEvaluator<T>.GetQurey(_context.Set<T>(),spec);

		}

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecfication(spec).CountAsync();
        }

        public async Task Add(T item)
        =>await _context.Set<T>().AddAsync(item);
        

        public void Update(T item)
        =>_context.Set<T>().Update(item);

        public void Delete(T item)
        =>_context.Set<T>().Remove(item);
    }
}
