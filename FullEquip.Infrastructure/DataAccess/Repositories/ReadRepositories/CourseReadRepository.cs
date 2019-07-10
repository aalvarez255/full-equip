using System;
using System.Linq;
using System.Threading.Tasks;
using FullEquip.Core.Entities;
using FullEquip.Core.Interfaces.Repositories.ReadRepositories;
using Microsoft.EntityFrameworkCore;

namespace FullEquip.Infrastructure.DataAccess.Repositories.ReadRepositories
{
    public class CourseReadRepository : ReadRepository<Course>, ICourseReadRepository 
    {
        public CourseReadRepository(ApplicationDbContext db) : base(db) { }

        public async Task<Course> GetWithNextCoursesAsync(Guid id)
        {
            var course = _db.Courses
                .AsNoTracking()
                .Include(x => x.NextCourses)
                .AsEnumerable()
                .Where(x => x.Id == id)
                .SingleOrDefault();

            return await Task.FromResult(course);
        }

        public async Task<Course> GetWithStudentsAsync(Guid id)
        {
            return await _db.Courses
                .AsNoTracking()
                .Include(x => x.Students)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
