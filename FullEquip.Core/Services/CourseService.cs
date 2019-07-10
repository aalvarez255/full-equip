using FullEquip.Core.Entities;
using FullEquip.Core.Interfaces.Repositories.ReadRepositories;
using FullEquip.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullEquip.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseReadRepository _readRepository;

        public CourseService(
            ICourseReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await _readRepository.GetAllAsync();
        }

        public async Task<Course> GetDetailAsync(Guid id)
        {
            return await _readRepository.GetWithStudentsAsync(id);
        }

        public async Task<Course> GetTreeAsync(Guid id)
        {
            return await _readRepository.GetWithNextCoursesAsync(id);
        }
    }
}
