using FullEquip.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullEquip.Core.Interfaces.Services
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllAsync();
        Task<Course> GetDetailAsync(Guid id);
        Task<Course> GetTreeAsync(Guid id);
    }
}
