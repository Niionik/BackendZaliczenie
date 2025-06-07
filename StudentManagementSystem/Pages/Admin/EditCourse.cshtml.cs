using Application.Repositories;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class EditCourseModel : PageModel
{
    private readonly ICourseRepository _courseRepository;
    [BindProperty] public int Id { get; set; }
    [BindProperty] public required string Name { get; set; }
    [BindProperty] public required string Description { get; set; }

    public EditCourseModel(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course == null) return RedirectToPage("/Admin/Courses");
        Id = course.Id;
        Name = course.Name;
        Description = course.Description;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        var course = new Course { Id = Id, Name = Name, Description = Description };
        await _courseRepository.UpdateAsync(course);
        return RedirectToPage("/Admin/Courses");
    }
} 