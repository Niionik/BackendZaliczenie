using Application.Repositories;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Presentation.RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public EnrollmentsController(IEnrollmentRepository enrollmentRepository, IStudentRepository studentRepository, ICourseRepository courseRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var enrollments = await _enrollmentRepository.GetAllAsync();
            var result = enrollments.Select(e => new EnrollmentDto
            {
                Id = e.Id,
                StudentId = e.StudentId,
                CourseId = e.CourseId,
                Student = new StudentDto
                {
                    Id = e.Student.Id,
                    FirstName = e.Student.FirstName,
                    LastName = e.Student.LastName,
                    Email = e.Student.Email,
                    Enrollments = new List<EnrollmentDto>()
                },
                Course = new CourseDto
                {
                    Id = e.Course.Id,
                    Name = e.Course.Name,
                    Description = e.Course.Description,
                    Enrollments = new List<EnrollmentDto>()
                }
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
                CourseId = enrollment.CourseId,
                Student = new StudentDto
                {
                    Id = enrollment.Student.Id,
                    FirstName = enrollment.Student.FirstName,
                    LastName = enrollment.Student.LastName,
                    Email = enrollment.Student.Email,
                    Enrollments = new List<EnrollmentDto>()
                },
                Course = new CourseDto
                {
                    Id = enrollment.Course.Id,
                    Name = enrollment.Course.Name,
                    Description = enrollment.Course.Description,
                    Enrollments = new List<EnrollmentDto>()
                }
            };
            
            return Ok(enrollmentDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] EnrollmentDto enrollmentDto)
        {
            var student = await _studentRepository.GetByIdAsync(enrollmentDto.StudentId);
            if (student == null)
                return NotFound("Student not found");

            var course = await _courseRepository.GetByIdAsync(enrollmentDto.CourseId);
            if (course == null)
                return NotFound("Course not found");

            var enrollment = new Enrollment
            {
                StudentId = enrollmentDto.StudentId,
                CourseId = enrollmentDto.CourseId,
                Student = student,
                Course = course
            };

            await _enrollmentRepository.AddAsync(enrollment);
            return CreatedAtAction(nameof(Get), new { id = enrollment.Id }, enrollmentDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] EnrollmentDto enrollmentDto)
        {
            if (id != enrollmentDto.Id)
                return BadRequest();

            var enrollment = await _enrollmentRepository.GetByIdAsync(id);
            if (enrollment == null)
                return NotFound();

            var student = await _studentRepository.GetByIdAsync(enrollmentDto.StudentId);
            if (student == null)
                return NotFound("Student not found");

            var course = await _courseRepository.GetByIdAsync(enrollmentDto.CourseId);
            if (course == null)
                return NotFound("Course not found");

            enrollment.StudentId = enrollmentDto.StudentId;
            enrollment.CourseId = enrollmentDto.CourseId;
            enrollment.Student = student;
            enrollment.Course = course;

            await _enrollmentRepository.UpdateAsync(enrollment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(id);
            if (enrollment == null)
                return NotFound();

            await _enrollmentRepository.DeleteAsync(id);
            return NoContent();
        }
    }
} 