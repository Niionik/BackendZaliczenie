using Domain;
using Infrastructure;
using Application.DTOs;
using Microsoft.EntityFrameworkCore;

public class Query
{
    public IQueryable<StudentDto> GetStudents([Service] AppDbContext db) => 
        db.Students.Select(s => new StudentDto
        {
            Id = s.Id,
            FirstName = s.FirstName,
            LastName = s.LastName,
            Email = s.Email
        });

    public IQueryable<CourseDto> GetCourses([Service] AppDbContext db) => 
        db.Courses.Select(c => new CourseDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description
        });

    public IQueryable<EnrollmentDto> GetEnrollments([Service] AppDbContext db) => 
        db.Enrollments.Select(e => new EnrollmentDto
        {
            Id = e.Id,
            StudentId = e.StudentId,
            CourseId = e.CourseId
        });
} 