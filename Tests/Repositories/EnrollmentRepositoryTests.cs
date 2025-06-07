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
    public class EnrollmentRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly IEnrollmentRepository _repository;

        public EnrollmentRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _repository = new EnrollmentRepository(_context);
        }

        [Fact]
        public async Task GetAll_ReturnsAllEnrollments()
        {
            var enrollments = new[]
            {
                new Enrollment { StudentId = 1, CourseId = 1 },
                new Enrollment { StudentId = 2, CourseId = 1 }
            };

            await _context.Enrollments.AddRangeAsync(enrollments);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAllAsync();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, e => e.StudentId == 1);
            Assert.Contains(result, e => e.StudentId == 2);
        }

        [Fact]
        public async Task GetById_WithValidId_ReturnsEnrollment()
        {
            var enrollment = new Enrollment { StudentId = 1, CourseId = 1 };
            await _context.Enrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByIdAsync(enrollment.Id);

            Assert.NotNull(result);
            Assert.Equal(1, result.StudentId);
            Assert.Equal(1, result.CourseId);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ReturnsNull()
        {
            var result = await _repository.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task Add_WithValidEnrollment_AddsToDatabase()
        {
            var enrollment = new Enrollment { StudentId = 1, CourseId = 1 };

            var result = await _repository.AddAsync(enrollment);

            Assert.NotNull(result);
            Assert.Equal(1, result.StudentId);
            Assert.Equal(1, result.CourseId);
            Assert.True(result.Id > 0);

            var savedEnrollment = await _context.Enrollments.FindAsync(result.Id);
            Assert.NotNull(savedEnrollment);
            Assert.Equal(1, savedEnrollment.StudentId);
        }

        [Fact]
        public async Task Update_WithValidEnrollment_UpdatesInDatabase()
        {
            var enrollment = new Enrollment { StudentId = 1, CourseId = 1 };
            await _context.Enrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();

            enrollment.StudentId = 2;
            enrollment.CourseId = 2;

            await _repository.UpdateAsync(enrollment);

            var updatedEnrollment = await _context.Enrollments.FindAsync(enrollment.Id);
            Assert.NotNull(updatedEnrollment);
            Assert.Equal(2, updatedEnrollment.StudentId);
            Assert.Equal(2, updatedEnrollment.CourseId);
        }

        [Fact]
        public async Task Delete_WithValidId_RemovesFromDatabase()
        {
            var enrollment = new Enrollment { StudentId = 1, CourseId = 1 };
            await _context.Enrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();

            await _repository.DeleteAsync(enrollment.Id);

            var deletedEnrollment = await _context.Enrollments.FindAsync(enrollment.Id);
            Assert.Null(deletedEnrollment);
        }
    }
} 