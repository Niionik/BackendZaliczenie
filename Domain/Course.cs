using System.Collections.Generic;

namespace Domain
{
    public class Course
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new();

        public Course()
        {
            Enrollments = new List<Enrollment>();
        }
    }
} 