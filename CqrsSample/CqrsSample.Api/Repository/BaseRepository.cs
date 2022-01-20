using CqrsSample.Api.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CqrsSample.Api.Repository
{
    public class BaseRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : class
    {
        private readonly BaseContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(BaseContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext can not be null");

            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }


        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();//.AsNoTracking() bunu kullanırsak performans artışı sağlayabiliriz
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).FirstOrDefault();
        }

        public void Add(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            _dbSet.Add(entity);

        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null)
                return;

            Delete(entity);
        }

        public void Delete(T entity)
        {
            //DbEntityEntry dbEntityEntry = _dbContext.Entry(entity);

            //if (dbEntityEntry.State != EntityState.Deleted)
            //{
            //    dbEntityEntry.State = EntityState.Deleted;
            //}
            //else
            //{
            //    _dbSet.Attach(entity);
            //    _dbSet.Remove(entity);
            //}
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

        }
        public void DetectUpdate(T entity)
        {
            _dbSet.Attach(entity);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
