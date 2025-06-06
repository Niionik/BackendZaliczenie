using Domain;
using Infrastructure;

public class Query
{
    public IQueryable<Student> GetStudents([Service] AppDbContext db) => db.Students;
    public IQueryable<Course> GetCourses([Service] AppDbContext db) => db.Courses;
    public IQueryable<Enrollment> GetEnrollments([Service] AppDbContext db) => db.Enrollments;
} 