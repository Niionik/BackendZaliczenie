using System.Collections.Generic;

namespace Domain
{
    public class Student
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new();

        public Student()
        {
            Enrollments = new List<Enrollment>();
        }
    }
} 