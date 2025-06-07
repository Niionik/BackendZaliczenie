using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Repositories;
using Domain;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.Repositories
{
    public class CourseRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly ICourseRepository _repository;

        public CourseRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _repository = new CourseRepository(_context);
        }

        [Fact]
        public async Task GetAll_ReturnsAllCourses()
        {
            var courses = new[]
            {
                new Course { Name = "Course 1", Description = "Description 1" },
                new Course { Name = "Course 2", Description = "Description 2" }
            };

            await _context.Courses.AddRangeAsync(courses);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAllAsync();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.Name == "Course 1");
            Assert.Contains(result, c => c.Name == "Course 2");
        }

        [Fact]
        public async Task GetById_WithValidId_ReturnsCourse()
        {
            var course = new Course { Name = "Test Course", Description = "Test Description" };
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByIdAsync(course.Id);

            Assert.NotNull(result);
            Assert.Equal("Test Course", result.Name);
            Assert.Equal("Test Description", result.Description);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ReturnsNull()
        {
            var result = await _repository.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task Add_WithValidCourse_AddsToDatabase()
        {
            var course = new Course { Name = "New Course", Description = "New Description" };

            var result = await _repository.AddAsync(course);

            Assert.NotNull(result);
            Assert.Equal("New Course", result.Name);
            Assert.Equal("New Description", result.Description);
            Assert.True(result.Id > 0);

            var savedCourse = await _context.Courses.FindAsync(result.Id);
            Assert.NotNull(savedCourse);
            Assert.Equal("New Course", savedCourse.Name);
        }

        [Fact]
        public async Task Update_WithValidCourse_UpdatesInDatabase()
        {
            var course = new Course { Name = "Original Name", Description = "Original Description" };
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            course.Name = "Updated Name";
            course.Description = "Updated Description";

            await _repository.UpdateAsync(course);

            var updatedCourse = await _context.Courses.FindAsync(course.Id);
            Assert.NotNull(updatedCourse);
            Assert.Equal("Updated Name", updatedCourse.Name);
            Assert.Equal("Updated Description", updatedCourse.Description);
        }

        [Fact]
        public async Task Delete_WithValidId_RemovesFromDatabase()
        {
            var course = new Course { Name = "Course to Delete", Description = "Description" };
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            await _repository.DeleteAsync(course.Id);

            var deletedCourse = await _context.Courses.FindAsync(course.Id);
            Assert.Null(deletedCourse);
        }
    }
} 