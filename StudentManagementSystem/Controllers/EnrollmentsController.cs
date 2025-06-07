using Application.Repositories;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Presentation.RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentsController(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var enrollments = await _enrollmentRepository.GetAllAsync();
            var result = enrollments.Select(e => new EnrollmentDto
            {
                Id = e.Id,
                StudentId = e.StudentId,
                CourseId = e.CourseId
            }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(id);
            if (enrollment == null) return NotFound();
            
            var enrollmentDto = new EnrollmentDto
            {
                Id = enrollment.Id,
                StudentId = enrollment.StudentId,
                CourseId = enrollment.CourseId
            };
            
            return Ok(enrollmentDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] EnrollmentDto enrollmentDto)
        {
            var enrollment = new Enrollment
            {
                StudentId = enrollmentDto.StudentId,
                CourseId = enrollmentDto.CourseId
            };
            
            await _enrollmentRepository.AddAsync(enrollment);
            enrollmentDto.Id = enrollment.Id;
            return CreatedAtAction(nameof(Get), new { id = enrollment.Id }, enrollmentDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] EnrollmentDto enrollmentDto)
        {
            if (id != enrollmentDto.Id) return BadRequest();
            
            var enrollment = new Enrollment
            {
                Id = enrollmentDto.Id,
                StudentId = enrollmentDto.StudentId,
                CourseId = enrollmentDto.CourseId
            };
            
            await _enrollmentRepository.UpdateAsync(enrollment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _enrollmentRepository.DeleteAsync(id);
            return NoContent();
        }
    }
} 