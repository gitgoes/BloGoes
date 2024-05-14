using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BloGoes.Data.Repository.Interfaces;

namespace BloGoes.Data.Repository
{
    public class EntityBaseRepository<T> : IEntityRepository<T> where T : EntityBase
    {
        private readonly ApplicationQuerieContext _querieContext;
        private readonly ApplicationCommandContext _commandContext;
        public EntityBaseRepository(ApplicationQuerieContext querieContext, ApplicationCommandContext commandContext)
        {
            _querieContext = querieContext;
            _commandContext = commandContext;
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _querieContext.Set<T>().Where(predicate).ToList();
        }
        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _querieContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {

            IQueryable<T> query = _querieContext.Set<T>();


            if (includes != null)
            {
                foreach (Expression<Func<T, object>> inc in includes)
                {
                    query = query.Include(inc);
                }
            }

            return await query.Where(predicate).ToListAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return _querieContext.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _querieContext.Set<T>().ToListAsync();
        }

        public T GetById(int id)
        {
            var entity = _querieContext.Set<T>().Find(id);
            _querieContext.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _querieContext.Set<T>().FindAsync(id);
        }

        public void Add(T entity)
        {
            _commandContext.Add(entity);
            _commandContext.SaveChanges();
        }

        public async Task<T> AddAsync(T entity)
        {
            _commandContext.Add(entity);
            await _commandContext.SaveChangesAsync();
            return entity;

        }

        public void AddRange(IEnumerable<T> entities)
        {
            _commandContext.AddRange(entities);
            _commandContext.SaveChanges();
        }

        public async void AddRangeAsync(IEnumerable<T> entities)
        {
            await _commandContext.AddRangeAsync(entities);
            await _commandContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _commandContext.Update(entity);
            _commandContext.SaveChanges();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _commandContext.Update(entity);
            await _commandContext.SaveChangesAsync();
            return entity;
        }

        public void UpdateRange(IEnumerable<T> entities) { _commandContext.UpdateRange(entities); _commandContext.SaveChanges(); }

        public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
        {
            _commandContext.UpdateRange(entities);
            await _commandContext.SaveChangesAsync();
            return entities;
        }

        public void Remove(T entity) { _commandContext.Remove(entity); _commandContext.SaveChanges(); }

        public void Remove(int id)
        {
            var entity = _commandContext.Set<T>().Find(id);
            _commandContext.Remove(entity);
            _commandContext.SaveChanges();
        }
        public async Task<int> RemoveAsync(int id)
        {
            var entity = _commandContext.Set<T>().Find(id);
            _commandContext.Remove(entity);
            return await _commandContext.SaveChangesAsync();

        }

        public void RemoveRange(IEnumerable<T> entities) { _commandContext.RemoveRange(entities); _commandContext.SaveChanges(); }


    }
}
