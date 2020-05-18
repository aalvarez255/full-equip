using System;
using System.Linq;
using System.Threading.Tasks;
using FullEquip.Core.Entities;
using FullEquip.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FullEquip.Infrastructure.DataAccess.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository 
    {
        public CourseRepository(ApplicationDbContext db) : base(db) { }

        public async Task<Course> GetAsync(Guid id)
        {
            return await _db.Courses.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Course> GetByCodeAsync(string code)
        {
            return await _db.Courses
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Code == code);
        }

        public async Task<Course> GetWithNextCoursesAsync(Guid id)
        {
            var course = _db.Courses
                .AsNoTracking()
                .Include(x => x.NextCourses)
                .Where(x => x.Id == id)
                .SingleOrDefault();

            return await Task.FromResult(course);
        }

        public async Task<Course> GetWithStudentsAsync(Guid id)
        {
            return await _db.Courses
                .AsNoTracking()
                .Include(x => x.Students)
                .ThenInclude(s => s.Student)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
