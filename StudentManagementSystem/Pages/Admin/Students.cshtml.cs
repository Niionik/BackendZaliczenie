using Application.Repositories;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;

[Authorize(Roles = "Admin")]
public class AdminStudentsModel : PageModel
{
    private readonly IStudentRepository _studentRepository;

    public AdminStudentsModel(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public required IEnumerable<StudentDto> Students { get; set; }

    public async Task OnGetAsync()
    {
        Students = await _studentRepository.GetAllAsync();
    }
} 