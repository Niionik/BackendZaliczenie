namespace Application.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public List<EnrollmentDto> Enrollments { get; set; } = new();
    }
} 