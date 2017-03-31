using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DataRepositories.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _entity;

        public GenericRepository()
        {
            _context = new DataContext();
            _entity = _context.Set<T>();
        }

        public T Find(object id)
        {
            return _entity.Find(id);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> result = _entity;
            if (filter != null)
                result = result.Where(filter);
            if(orderBy != null)
                result = orderBy.Invoke(result);
            foreach (var include in includes)
            {
                result = result.Include(include);
            }


            return result;
        }

        public void Add(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Added;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Attach(object obj, EntityState state)
        {
            _context.Entry(obj).State = state;
        }
    }
}
