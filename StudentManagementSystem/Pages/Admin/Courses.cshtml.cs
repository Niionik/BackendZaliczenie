using Application.Repositories;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;

[Authorize(Roles = "Admin")]
public class AdminCoursesModel : PageModel
{
    private readonly ICourseRepository _courseRepository;

    public AdminCoursesModel(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public IEnumerable<CourseDto> Courses { get; set; } = new List<CourseDto>();

    public void OnGet()
    {
        Courses = _courseRepository.GetAllAsync().Result.Select(c => new CourseDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            Enrollments = new List<EnrollmentDto>()
        });
    }
} 