using FullEquip.Core.Entities;
using System;
using System.Threading.Tasks;

namespace FullEquip.Core.Interfaces.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<Course> GetAsync(Guid id);
        Task<Course> GetByCodeAsync(string code);
        Task<Course> GetWithStudentsAsync(Guid id);
        Task<Course> GetWithNextCoursesAsync(Guid id);
    }
}
