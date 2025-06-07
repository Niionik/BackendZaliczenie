using Domain;
using Infrastructure;
using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class Query
{
    public IEnumerable<StudentDto> GetStudents([Service] AppDbContext db)
        => db.Students.Select(s => new StudentDto
        {
            Id = s.Id,
            FirstName = s.FirstName,
            LastName = s.LastName,
            Email = s.Email,
            Enrollments = new List<EnrollmentDto>()
        });

    public IEnumerable<CourseDto> GetCourses([Service] AppDbContext db)
        => db.Courses.Select(c => new CourseDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            Enrollments = new List<EnrollmentDto>()
        });

    public IEnumerable<EnrollmentDto> GetEnrollments([Service] AppDbContext db)
        => db.Enrollments.Include(e => e.Student).Include(e => e.Course).Select(e => new EnrollmentDto
        {
            Id = e.Id,
            StudentId = e.Student.Id,
            CourseId = e.Course.Id,
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
        });
} 