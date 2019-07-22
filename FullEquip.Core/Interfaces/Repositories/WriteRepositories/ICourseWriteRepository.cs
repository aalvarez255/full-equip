using FullEquip.Core.Entities;
using System;
using System.Threading.Tasks;

namespace FullEquip.Core.Interfaces.Repositories.WriteRepositories
{
    public interface ICourseWriteRepository : IWriteRepository<Course>
    {
        Task<Course> GetWithStudentsAsync(Guid id);
    }
}
