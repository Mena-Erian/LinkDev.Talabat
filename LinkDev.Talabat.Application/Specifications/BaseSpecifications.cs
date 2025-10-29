using LinkDev.Talabat.Domain.Common;
using LinkDev.Talabat.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Specifications
{
    internal abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
         where TEntity : BaseEntity<TKey>
         where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; } = null;
        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; private set; } = new();

        protected BaseSpecifications()
        {
            //Criteria = null;
        }
        protected BaseSpecifications(TKey id)
        {
            Criteria = entity => entity.Id.Equals(id);
        }

        protected BaseSpecifications(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        public void AddIncludes(Expression<Func<TEntity, object>> Include)
        {
            if (Include != null)
                IncludeExpression.Add(Include);
        }
    }
}
