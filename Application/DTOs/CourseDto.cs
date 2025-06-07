namespace Application.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<EnrollmentDto> Enrollments { get; set; } = new();
    }
} 