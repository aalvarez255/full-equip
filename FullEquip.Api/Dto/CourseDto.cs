using System;
using System.Collections.Generic;

namespace FullEquip.Api.Dto
{
    public class CourseDto
    {
        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public CourseTypeDto Type { get; private set; }

        public CourseDto(Guid id, string code, CourseTypeDto type)
        {
            Id = id;
            Code = code;
            Type = type;
        }
    }

    public class CourseDetailDto : CourseDto
    {
        public string Address { get; private set; }
        public string VideoUrl { get; private set; }
        public List<StudentDto> Students { get; private set; }

        public CourseDetailDto(Guid id, string code, CourseTypeDto type, string address, string videoUrl, List<StudentDto> students)
            : base(id, code, type)
        {
            Address = address;
            VideoUrl = videoUrl;
        }
    }

    public class CourseTreeDto : CourseDto
    {
        public List<CourseTreeDto> NextCourses { get; private set; }

        public CourseTreeDto(Guid id, string code, CourseTypeDto type, List<CourseTreeDto> nextCourses)
            : base(id, code, type)
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
