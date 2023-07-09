using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specfications
{
	public class BaseSpecfication<T> : ISpecification<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>> Criteria { get; set; }
		public List<Expression<Func<T, object>>> Includes { get; set; }= new List<Expression<Func<T, object>>>();
		public Expression<Func<T, object>> OrderBy { get; set; }
		public Expression<Func<T, object>> OrderByDesc { get; set; }
		public int Skip { get; set ; }
		public int Take { get ; set; }
		public bool IsPaginationEnabled { get; set ; }

		public BaseSpecfication()
		{
			Includes = new List<Expression<Func<T, object>>>();
		}

		public BaseSpecfication(Expression<Func<T, bool>> criteriaExpression)
		{
			Criteria = criteriaExpression;
			//Includes = new List<Expression<Func<T, object>>>();
		}

		public void AddOrderBy(Expression<Func<T, object>> OrderbyExp)
		{
			OrderBy = OrderbyExp;

		}

		public void AddOrderByDesc(Expression<Func<T, object>> OrderbyDescExp)
		{
			OrderByDesc = OrderbyDescExp;

		}

		public void ApplyPagination(int skip,int take)
		{
			IsPaginationEnabled= true;
			Skip= skip;
			Take= take;
		}
	}
}
