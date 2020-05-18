using FullEquip.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FullEquip.Api.Dto.Extensions
{
    public static class CourseExtensions
    {
        public static CourseDto ToDto(this Course course)
        {
            return new CourseDto(course.Id, course.Code, course.Name, GetCourseType(course));
        }

        public static CourseDetailDto ToDetailDto(this Course course)
        {
            var type = GetCourseType(course);
            var students = course.Students.Select(x => x.Student.ToDto()).ToList();

            switch (type)
            {
                case CourseTypeDto.Online:
                    return new CourseDetailDto(
                        course.Id,
                        course.Code,
                        course.Name,
                        type,
                        null,
                        (course as OnlineCourse).VideoUrl,
                        students);
                case CourseTypeDto.ClassRoom:
                    return new CourseDetailDto(
                        course.Id,
                        course.Code,
                        course.Name,
                        type,
                        (course as ClassRoomCourse).Address,
                        null,
                        students);
                default:
                    return null;
            }
        }

        public static CourseTreeDto ToTreeDto(this Course course)
        {
            return new CourseTreeDto(
                course.Id,
                course.Code,
                course.Name,
                GetCourseType(course),
                course.NextCourses
                    .Select(x => x.ToTreeDto())
                    .ToList());
        }

        public static Course ToEntity(this CourseCreateEditDto dto)
        {
            var courseStudents = dto.Students?
                .Select(x => new CourseStudent() { StudentId = x.Id })
                .ToList();

            switch (dto.Type)
            {
                case CourseTypeDto.ClassRoom:
                    return new ClassRoomCourse()
                    {
                        Id = dto.Id,
                        Name = dto.Name,
                        Code = dto.Code,
                        Students = courseStudents ?? new List<CourseStudent>(),
                        PrerequisiteCourseId = dto.PrerequisiteCourseId,
                        Address = dto.Address                        
                    };
                case CourseTypeDto.Online:
                    return new OnlineCourse()
                    {
                        Id = dto.Id,
                        Name = dto.Name,
                        Code = dto.Code,
                        Students = courseStudents ?? new List<CourseStudent>(),
                        PrerequisiteCourseId = dto.PrerequisiteCourseId,
                        VideoUrl = dto.VideoUrl
                    };
                default:
                    return null;
            }
        }

        private static CourseTypeDto GetCourseType(Course course)
        {
            switch (course)
            {
                case OnlineCourse _:
                    return CourseTypeDto.Online;
                case ClassRoomCourse _:
                    return CourseTypeDto.ClassRoom;
                default:
                    throw new Exception("Invalid course type");
            }
        }
    }
}
