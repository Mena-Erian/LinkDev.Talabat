using LinkDev.Talabat.Domain.Common;
using LinkDev.Talabat.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories
{
    internal static class SpecificationsEvaluator<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> spec)
        {
            var query = inputQuery; // _dbContext.Set<TEntity>()

            if (spec.Criteria is not null) // p => p.Id.Equals(1)
                query = query.Where(spec.Criteria);

            if (spec.Includes is not null)
                query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            //query = query.Include(spec.Includes.First());

            return query;
        }
    }
}
