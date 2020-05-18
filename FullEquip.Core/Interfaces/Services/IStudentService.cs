using FullEquip.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullEquip.Core.Interfaces.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllAsync();
    }
}
