namespace ClothesStoreMobileApplication.Repository.DataAccess.Repository
{
    using ClothesStoreMobileApplication.Models;
    using ClothesStoreMobileApplication.Repository.IRepository;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ClothesStoreContext _db;
        private readonly DbSet<T> _dbSet;

        public Repository(ClothesStoreContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null,
                                     Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                     string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).AsNoTracking().ToList();
            }

            return query.AsNoTracking().ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.AsNoTracking().FirstOrDefault();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Remove(int id)
        {
            T entity = _dbSet.Find(id);
            Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
