using System;
using System.Collections.Generic;

namespace FullEquip.Core.Entities
{
    public abstract class Course
    {
        public Course()
        {
            Students = new List<CourseStudent>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CourseStudent> Students { get; set; }

        public Guid? PrerequisiteCourseId { get; set; }
        public virtual Course PrerequisiteCourse { get; set; }
        public virtual ICollection<Course> NextCourses { get; set; }
    }
}
