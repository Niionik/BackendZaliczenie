namespace Application.DTOs
{
    public class EnrollmentDto
    {
        public int Id { get; set; }
        public required StudentDto Student { get; set; }
        public required CourseDto Course { get; set; }
    }
} 