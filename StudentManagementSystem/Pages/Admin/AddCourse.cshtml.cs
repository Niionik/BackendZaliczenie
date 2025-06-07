using Application.Repositories;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Application.DTOs;

namespace StudentManagementSystem.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class AddCourseModel : PageModel
    {
        private readonly ICourseRepository _courseRepository;
        [BindProperty]
        public required string Name { get; set; }
        [BindProperty]
        public required string Description { get; set; }

        public AddCourseModel(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var course = new Course { Name = Name, Description = Description };
            await _courseRepository.AddAsync(course);
            return RedirectToPage("/Admin/Courses");
        }
    }
} 