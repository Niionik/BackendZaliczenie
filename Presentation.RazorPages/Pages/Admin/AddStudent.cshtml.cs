using Application.Repositories;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class AddStudentModel : PageModel
{
    private readonly IStudentRepository _studentRepository;
    [BindProperty] public string FirstName { get; set; }
    [BindProperty] public string LastName { get; set; }
    [BindProperty] public string Email { get; set; }

    public AddStudentModel(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        var student = new Student { FirstName = FirstName, LastName = LastName, Email = Email };
        await _studentRepository.AddAsync(student);
        return RedirectToPage("/Admin/Students");
    }
} 