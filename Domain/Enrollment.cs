using System.Collections.Generic;

namespace Domain
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public required Student Student { get; set; }
        public required Course Course { get; set; }
    }
} 