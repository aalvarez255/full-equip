using FullEquip.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullEquip.Core.Interfaces.Repositories
{
    public interface IReadRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(Guid id);
        Task<List<T>> GetAllAsync();
    }
}
