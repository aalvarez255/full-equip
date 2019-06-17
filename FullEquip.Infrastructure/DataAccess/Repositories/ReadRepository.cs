using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullEquip.Core.Entities;
using FullEquip.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FullEquip.Infrastructure.DataAccess.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _db;

        public ReadRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<T>> GetAllAsync()
        {
            var result = _db.Set<T>().AsNoTracking().ToList();
            return await Task.FromResult(result);
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await _db.Set<T>().AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
