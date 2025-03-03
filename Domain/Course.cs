using System.Collections.Generic;

namespace Domain
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }

        public Course()
        {
            Enrollments = new List<Enrollment>();
        }
    }
} 