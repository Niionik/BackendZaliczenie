using Application.Repositories;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class AdminStudentsModel : PageModel
{
    private readonly IStudentRepository _studentRepository;
    public List<Student> Students { get; set; }

    public AdminStudentsModel(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task OnGetAsync()
    {
        Students = (await _studentRepository.GetAllAsync()).ToList();
    }
} 