using FullEquip.Core.Entities;
using FullEquip.Core.Interfaces.Repositories.WriteRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FullEquip.Infrastructure.DataAccess.Repositories.WriteRepositories
{
    public class CourseStudentWriteRepository : WriteRepository<CourseStudent>, ICourseStudentWriteRepository
    {
        public CourseStudentWriteRepository(ApplicationDbContext db) : base(db) { }
    }
}
