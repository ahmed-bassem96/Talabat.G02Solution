using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specfications;

namespace Talabat.Repository
{
	public static class SpecficationEvaluator<TEntity>where TEntity: BaseEntity
	{
		public static IQueryable<TEntity> GetQurey(IQueryable<TEntity> inputQuery,ISpecification<TEntity> spec) 
		{
		   var query=inputQuery; ///dbContext.set<Product>()

			if(spec.Criteria is not null)//P=>P.id==1	
			{
				query = query.Where(spec.Criteria); //dbContext.set<Product>().Where()
			}
				

			if(spec.OrderBy is not null)
			{
				query = query.OrderBy(spec.OrderBy); 
			}
			
			if (spec.OrderByDesc is not null)
			{
				query=query.OrderByDescending(spec.OrderByDesc);
			}

			if(spec.IsPaginationEnabled)
			{
				query=query.Skip(spec.Skip).Take(spec.Take);
			}

			query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
			return query;
		}
	}
}
