using FullEquip.Core.Entities;
using FullEquip.Core.Interfaces.Repositories.WriteRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FullEquip.Infrastructure.DataAccess.Repositories.WriteRepositories
{
    public class CourseWriteRepository : WriteRepository<Course>, ICourseWriteRepository
    {
        public CourseWriteRepository(ApplicationDbContext db) : base(db) { }

        public async Task<Course> GetWithStudentsAsync(Guid id)
        {
            return await _db.Courses
                .Include(x => x.Students)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
