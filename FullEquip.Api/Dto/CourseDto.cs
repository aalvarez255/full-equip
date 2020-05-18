using System;
using System.Collections.Generic;

namespace FullEquip.Api.Dto
{
    public class CourseDto
    {
        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public CourseTypeDto Type { get; private set; }

        public CourseDto(Guid id, string code, string name, CourseTypeDto type)
        {
            Id = id;
            Code = code;
            Name = name;
            Type = type;
        }
    }

    public class CourseDetailDto : CourseDto
    {
        public string Address { get; private set; }
        public string VideoUrl { get; private set; }
        public List<StudentDto> Students { get; private set; }

        public CourseDetailDto(Guid id, string code, string name, CourseTypeDto type, string address, string videoUrl, List<StudentDto> students)
            : base(id, code, name, type)
        {
            Address = address;
            VideoUrl = videoUrl;
            Students = students;
        }
    }

    public class CourseCreateEditDto : CourseDetailDto
    {
        public Guid? PrerequisiteCourseId { get; set; }

        public CourseCreateEditDto(Guid id, string code,  string name, CourseTypeDto type, string address, string videoUrl, List<StudentDto> students, Guid? prerequisiteId)
           : base(id, code, name, type, address, videoUrl, students)
        {
            PrerequisiteCourseId = prerequisiteId;
        }
    }

    public class CourseTreeDto : CourseDto
    {
        public List<CourseTreeDto> NextCourses { get; private set; }

        public CourseTreeDto(Guid id, string code, string name, CourseTypeDto type, List<CourseTreeDto> nextCourses)
            : base(id, code, name, type)
        {
            NextCourses = nextCourses;
        }
    }

    public enum CourseTypeDto
    {
        Online,
        ClassRoom
    }
}
