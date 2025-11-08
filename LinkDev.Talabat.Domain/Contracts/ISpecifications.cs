using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Domain.Contracts
{
    public interface ISpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        //public Expression<Predicate<TEntity>> Criteria { get; set; }  // P => P.Id == id
        public Expression<Func<TEntity, bool>>? Criteria { get; } // P => P.Id == id
        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; }

        public void AddIncludes(Expression<Func<TEntity, object>> Include);
        public Expression<Func<TEntity, object>>? OrderByAsc { get; }
        public Expression<Func<TEntity, object>>? OrderByDesc { get; }
    }
}
