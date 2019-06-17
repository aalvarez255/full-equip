using System;

namespace FullEquip.Core.Entities
{
    public class CourseStudent
    {
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; }

        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }

        public double? Score { get; set; }
    }
}
