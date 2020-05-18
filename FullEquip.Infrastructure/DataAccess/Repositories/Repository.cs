using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullEquip.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FullEquip.Infrastructure.DataAccess.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _db;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<T>> GetAllAsync()
        {
            var result = _db.Set<T>().AsNoTracking().ToList();
            return await Task.FromResult(result);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<List<T>> AddAsync(List<T> entities)
        {
            await _db.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public async Task DeleteAsync(T entity)
        {
            _db.Set<T>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(List<T> entities)
        {
            _db.Set<T>().RemoveRange(entities);
            await Task.CompletedTask;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            return await Task.FromResult(entity);
        }

        public async Task<List<T>> UpdateAsync(List<T> entities)
        {
            foreach (T entity in entities) _db.Entry(entity).State = EntityState.Modified;
            return await Task.FromResult(entities);
        }
    }
}
