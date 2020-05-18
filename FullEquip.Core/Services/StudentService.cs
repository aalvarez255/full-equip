using FullEquip.Core.Constants.Errors;
using FullEquip.Core.Entities;
using FullEquip.Core.Exceptions;
using FullEquip.Core.Interfaces.Repositories;
using FullEquip.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullEquip.Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(
            IStudentRepository repository)
        {
            _repository = repository;
        }

       
        public async Task<List<Student>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
