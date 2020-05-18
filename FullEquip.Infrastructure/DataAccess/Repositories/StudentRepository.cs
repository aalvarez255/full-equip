using System;
using System.Linq;
using System.Threading.Tasks;
using FullEquip.Core.Entities;
using FullEquip.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FullEquip.Infrastructure.DataAccess.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository 
    {
        public StudentRepository(ApplicationDbContext db) : base(db) { }
    }
}
