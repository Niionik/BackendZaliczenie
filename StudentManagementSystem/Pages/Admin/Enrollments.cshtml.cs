using Application.DTOs;
using Application.Repositories;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class AdminEnrollmentsModel : PageModel
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    public required IEnumerable<EnrollmentDto> Enrollments { get; set; }

    public AdminEnrollmentsModel(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public void OnGet()
    {
        Enrollments = _enrollmentRepository.GetAllAsync().Result.Select(e => new EnrollmentDto
        {
            Id = e.Id,
            StudentId = e.StudentId,
            CourseId = e.CourseId,
            Student = new StudentDto
            {
                Id = e.Student.Id,
                FirstName = e.Student.FirstName,
                LastName = e.Student.LastName,
                Email = e.Student.Email,
                Enrollments = new List<EnrollmentDto>()
            },
            Course = new CourseDto
            {
                Id = e.Course.Id,
                Name = e.Course.Name,
                Description = e.Course.Description,
                Enrollments = new List<EnrollmentDto>()
            }
        });
    }
} 