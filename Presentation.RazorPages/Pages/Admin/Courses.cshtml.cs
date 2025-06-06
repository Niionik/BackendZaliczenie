using Application.Repositories;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class AdminCoursesModel : PageModel
{
    private readonly ICourseRepository _courseRepository;
    public List<Course> Courses { get; set; }

    public AdminCoursesModel(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task OnGetAsync()
    {
        Courses = (await _courseRepository.GetAllAsync()).ToList();
    }
} 