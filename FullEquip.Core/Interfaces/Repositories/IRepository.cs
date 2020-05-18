using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullEquip.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<List<T>> AddAsync(List<T> entities);
        Task<T> UpdateAsync(T entity);
        Task<List<T>> UpdateAsync(List<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteAsync(List<T> entities);
    }
}
