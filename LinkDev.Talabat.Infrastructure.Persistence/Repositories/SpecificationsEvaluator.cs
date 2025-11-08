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


            if (spec.OrderByAsc is not null)
                query = query.OrderBy(spec.OrderByAsc);

            else if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc); // P => P.Name


            /// foreach (var include in spec.IncludeExpression)
            /// {
            ///     query = query.Include(include);
            /// }

            if (spec.IncludeExpression?.Count > 0)
                query = spec.IncludeExpression.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));


            return query;
        }

    }
}
