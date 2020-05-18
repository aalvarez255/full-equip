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
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repository;
        private readonly ICourseStudentRepository _studentRepository;
        private readonly IUnitOfWork _uow;

        public CourseService(
            ICourseRepository repository,
            ICourseStudentRepository studentRepository,
            IUnitOfWork uow)
        {
            _repository = repository;
            _studentRepository = studentRepository;
            _uow = uow;
        }

        public async Task<Course> CreateAsync(Course course)
        {
            if (string.IsNullOrEmpty(course.Code))
                throw new ValidationException(CourseErrors.EmptyCode);

            if (await _repository.GetByCodeAsync(course.Code) != null)
                throw new ValidationException(CourseErrors.AlreadyExistsCode);

            if (course.PrerequisiteCourseId.HasValue
                && await _repository.GetAsync(course.PrerequisiteCourseId.Value) == null)
                throw new ValidationException(CourseErrors.InvalidPrerequisiteCourse);

            var createdCourse = await _repository.AddAsync(course);
            await _uow.CommitAsync();

            return createdCourse;
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Course> GetDetailAsync(Guid id)
        {
            return await _repository.GetWithStudentsAsync(id);
        }

        public async Task<Course> GetTreeAsync(Guid id)
        {
            return await _repository.GetWithNextCoursesAsync(id);
        }

        public async Task UpdateAsync(Course course)
        {
            var dbCourse = await GetDetailAsync(course.Id);
            if (dbCourse == null)
                throw new ValidationException(CourseErrors.NotFound);

            if (course.GetType() != dbCourse.GetType())
                throw new ValidationException(CourseErrors.ChangeTypeNotAllowed);

            if (string.IsNullOrEmpty(course.Code))
                throw new ValidationException(CourseErrors.EmptyCode);

            var sameCodeCourse = await _repository.GetByCodeAsync(course.Code);
            if (sameCodeCourse != null && sameCodeCourse.Id != course.Id)
                throw new ValidationException(CourseErrors.AlreadyExistsCode);

            if (course.PrerequisiteCourseId.HasValue
                && await _repository.GetAsync(course.PrerequisiteCourseId.Value) == null)
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
                await _studentRepository.DeleteAsync(deletedStudents);

            var addedUsers = course.Students
                .Where(x => !dbCourse.Students.Any(s => s.StudentId == x.StudentId))
                .ToList();

            if (addedUsers.Any())
                await _studentRepository.AddAsync(addedUsers);
        }

        private async Task UpdateCoursePropertiesAsync(Course dbCourse, Course course)
        {
            dbCourse.Code = course.Code;
            dbCourse.Name = course.Name;
            dbCourse.PrerequisiteCourseId = course.PrerequisiteCourseId;

            if (dbCourse is OnlineCourse)
                (dbCourse as OnlineCourse).VideoUrl = (course as OnlineCourse).VideoUrl;
            else if (dbCourse is ClassRoomCourse)
                (dbCourse as ClassRoomCourse).Address = (course as ClassRoomCourse).Address;

            await _repository.UpdateAsync(dbCourse);
        }

        public async Task DeleteAsync(Guid id)
        {
            var dbCourse = await _repository.GetAsync(id);
            if (dbCourse == null)
                throw new ValidationException(CourseErrors.NotFound);

            await _repository.DeleteAsync(dbCourse);
            await _uow.CommitAsync();
        }
    }
}
