using Application.Repositories;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class AdminEnrollmentsModel : PageModel
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    public List<Enrollment> Enrollments { get; set; }

    public AdminEnrollmentsModel(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task OnGetAsync()
    {
        Enrollments = (await _enrollmentRepository.GetAllAsync()).ToList();
    }
} 