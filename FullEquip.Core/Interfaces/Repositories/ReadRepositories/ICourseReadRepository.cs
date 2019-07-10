using FullEquip.Core.Entities;
using System;
using System.Threading.Tasks;

namespace FullEquip.Core.Interfaces.Repositories.ReadRepositories
{
    public interface ICourseReadRepository : IReadRepository<Course>
    {
        Task<Course> GetWithStudentsAsync(Guid id);
        Task<Course> GetWithNextCoursesAsync(Guid id);
    }
}
