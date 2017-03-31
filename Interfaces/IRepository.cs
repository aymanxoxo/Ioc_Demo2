using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        T Find(object id);

        IQueryable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);

        void Add(T entity);

        void Attach(object obj, EntityState state);

        void SaveChanges();
    }
}
