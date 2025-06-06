using Application.Repositories;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class DeleteStudentModel : PageModel
{
    private readonly IStudentRepository _studentRepository;
    [BindProperty] public int Id { get; set; }
    [BindProperty] public string FirstName { get; set; }
    [BindProperty] public string LastName { get; set; }

    public DeleteStudentModel(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student == null) return RedirectToPage("/Admin/Students");
        Id = student.Id;
        FirstName = student.FirstName;
        LastName = student.LastName;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _studentRepository.DeleteAsync(Id);
        return RedirectToPage("/Admin/Students");
    }
} 