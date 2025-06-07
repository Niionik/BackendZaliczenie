namespace Application.DTOs
{
    public class EnrollmentDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public required StudentDto Student { get; set; }
        public required CourseDto Course { get; set; }
    }
} 