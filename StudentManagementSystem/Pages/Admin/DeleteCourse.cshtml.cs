using Application.Repositories;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class DeleteCourseModel : PageModel
{
    private readonly ICourseRepository _courseRepository;
    [BindProperty] public int Id { get; set; }
    [BindProperty] public required string Name { get; set; }

    public DeleteCourseModel(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course == null) return RedirectToPage("/Admin/Courses");
        Id = course.Id;
        Name = course.Name;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _courseRepository.DeleteAsync(Id);
        return RedirectToPage("/Admin/Courses");
    }
} 