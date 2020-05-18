using FullEquip.Core.Entities;
using FullEquip.Core.Interfaces.Repositories;

namespace FullEquip.Infrastructure.DataAccess.Repositories
{
    public class CourseStudentRepository : Repository<CourseStudent>, ICourseStudentRepository
    {
        public CourseStudentRepository(ApplicationDbContext db) : base(db) { }
    }
}
