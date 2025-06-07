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

    public required IEnumerable<CourseDto> Courses { get; set; }

    public async Task OnGetAsync()
    {
        Courses = await _courseRepository.GetAllAsync();
    }
} 