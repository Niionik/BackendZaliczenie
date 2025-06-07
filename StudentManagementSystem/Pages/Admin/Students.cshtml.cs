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

    public IEnumerable<StudentDto> Students { get; set; } = new List<StudentDto>();

    public void OnGet()
    {
        Students = _studentRepository.GetAllAsync().Result.Select(s => new StudentDto
        {
            Id = s.Id,
            FirstName = s.FirstName,
            LastName = s.LastName,
            Email = s.Email,
            Enrollments = new List<EnrollmentDto>()
        });
    }
} 