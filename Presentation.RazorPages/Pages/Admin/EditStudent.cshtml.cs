using Application.Repositories;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class EditStudentModel : PageModel
{
    private readonly IStudentRepository _studentRepository;
    [BindProperty] public int Id { get; set; }
    [BindProperty] public string FirstName { get; set; }
    [BindProperty] public string LastName { get; set; }
    [BindProperty] public string Email { get; set; }

    public EditStudentModel(IStudentRepository studentRepository)
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
        Email = student.Email;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        var student = new Student { Id = Id, FirstName = FirstName, LastName = LastName, Email = Email };
        await _studentRepository.UpdateAsync(student);
        return RedirectToPage("/Admin/Students");
    }
} 