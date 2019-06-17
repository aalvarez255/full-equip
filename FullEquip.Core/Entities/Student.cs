using System.Collections.Generic;

namespace FullEquip.Core.Entities
{
    public class Student : BaseEntity
    {
        public Student()
        {
            Courses = new List<CourseStudent>();
        }

        public string Name { get; set; }
        public string Email { get; set; }

        public virtual StudentAddress Address { get; set; }
        public virtual ICollection<CourseStudent> Courses { get; set; }
    }
}
