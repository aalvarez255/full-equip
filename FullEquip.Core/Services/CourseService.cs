using FullEquip.Core.Entities;
using FullEquip.Core.Exceptions;
using FullEquip.Core.Exceptions.Errors;
using FullEquip.Core.Interfaces.Repositories;
using FullEquip.Core.Interfaces.Repositories.ReadRepositories;
using FullEquip.Core.Interfaces.Repositories.WriteRepositories;
using FullEquip.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullEquip.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseReadRepository _readRepository;
        private readonly ICourseWriteRepository _writeRepository;
        private readonly ICourseStudentWriteRepository _studentsWriteRepository;
        private readonly IUnitOfWork _uow;

        public CourseService(
            ICourseReadRepository readRepository,
            ICourseWriteRepository writeRepository,
            ICourseStudentWriteRepository studentsWriteReposirory,
            IUnitOfWork uow)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _studentsWriteRepository = studentsWriteReposirory;
            _uow = uow;
        }

        public async Task<Course> CreateAsync(Course course)
        {
            if (String.IsNullOrEmpty(course.Code))
                throw new ValidationException(CourseErrors.EmptyCode);

            if (await _readRepository.GetByCodeAsync(course.Code) != null)
                throw new ValidationException(CourseErrors.AlreadyExistsCode);

            if (course.PrerequisiteCourseId.HasValue
                && await _readRepository.GetAsync(course.PrerequisiteCourseId.Value) == null)
                throw new ValidationException(CourseErrors.InvalidPrerequisiteCourse);

            var createdCourse = await _writeRepository.AddAsync(course);
            await _uow.CommitAsync();

            return createdCourse;
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

        public async Task UpdateAsync(Course course)
        {
            var dbCourse = await _writeRepository.GetWithStudentsAsync(course.Id);
            if (dbCourse == null)
                throw new ValidationException(CourseErrors.NotFound);

            if (course.GetType() != dbCourse.GetType())
                throw new ValidationException(CourseErrors.ChangeTypeNotAllowed);

            if (String.IsNullOrEmpty(course.Code))
                throw new ValidationException(CourseErrors.EmptyCode);

            var sameCodeCourse = await _readRepository.GetByCodeAsync(course.Code);
            if (sameCodeCourse != null && sameCodeCourse.Id != course.Id)
                throw new ValidationException(CourseErrors.AlreadyExistsCode);

            if (course.PrerequisiteCourseId.HasValue
                && await _readRepository.GetAsync(course.PrerequisiteCourseId.Value) == null)
                throw new ValidationException(CourseErrors.InvalidPrerequisiteCourse);

            await UpdateCourseStudentsAsync(dbCourse, course);
            await UpdateCoursePropertiesAsync(dbCourse, course);
            await _uow.CommitAsync();
        }

        private async Task UpdateCourseStudentsAsync(Course dbCourse, Course course)
        {
            var deletedStudents = dbCourse.Students
                .Where(x => !course.Students.Any(s => s.StudentId == x.StudentId))
                .ToList();

            if (deletedStudents.Any())
                await _studentsWriteRepository.DeleteAsync(deletedStudents);

            var addedUsers = course.Students
                .Where(x => !dbCourse.Students.Any(s => s.StudentId == x.StudentId))
                .ToList();

            if (addedUsers.Any())
                await _studentsWriteRepository.AddAsync(addedUsers);
        }

        private async Task UpdateCoursePropertiesAsync(Course dbCourse, Course course)
        {
            dbCourse.Code = course.Code;
            dbCourse.PrerequisiteCourseId = course.PrerequisiteCourseId;

            if (dbCourse is OnlineCourse)
                (dbCourse as OnlineCourse).VideoUrl = (course as OnlineCourse).VideoUrl;
            else if (dbCourse is ClassRoomCourse)
                (dbCourse as ClassRoomCourse).Address = (course as ClassRoomCourse).Address;

            await _writeRepository.UpdateAsync(dbCourse);
        }
    }
}
