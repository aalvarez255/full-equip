using System;
using System.Collections.Generic;

namespace FullEquip.Core.Entities
{
    public abstract class Course : BaseEntity
    {
        public string Code { get; set; }

        public virtual ICollection<CourseStudent> Students { get; set; }

        public Guid? PrerequisiteCourseId { get; set; }
        public virtual Course PrerequisiteCourse { get; set; }
        public virtual ICollection<Course> NextCourses { get; set; }

        public Course()
        {
            Students = new List<CourseStudent>();
        }
    }
}
