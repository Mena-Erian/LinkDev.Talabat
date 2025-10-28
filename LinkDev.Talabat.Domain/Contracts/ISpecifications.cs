﻿using System;
using System.Collections.Generic;
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
        public Expression<Func<TEntity, bool>>? Criteria { get; set; } // P => P.Id == id
        public List<Expression<Func<TEntity, object>>> Includes { get; set; }
    }
}
