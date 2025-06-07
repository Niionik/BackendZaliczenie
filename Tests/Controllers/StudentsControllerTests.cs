using Microsoft.AspNetCore.Mvc;
using Moq;
using Application.Repositories;
using Domain;
using StudentManagementSystem.Controllers;
using Xunit;

namespace Tests.Controllers
{
    public class StudentsControllerTests
    {
        private readonly Mock<IStudentRepository> _mockRepo;
        private readonly StudentsController _controller;

        public StudentsControllerTests()
        {
            _mockRepo = new Mock<IStudentRepository>();
            _controller = new StudentsController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithStudents()
        {
            var students = new List<Student>
            {
                new Student { Id = 1, FirstName = "Jan", LastName = "Kowalski", Email = "jan@example.com" },
                new Student { Id = 2, FirstName = "Anna", LastName = "Nowak", Email = "anna@example.com" }
            };

            _mockRepo.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(students);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Student>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task Get_WithValidId_ReturnsOkResult()
        {
            var student = new Student { Id = 1, FirstName = "Jan", LastName = "Kowalski", Email = "jan@example.com" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(student);

            var result = await _controller.Get(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Student>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task Get_WithInvalidId_ReturnsNotFound()
        {
            _mockRepo.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync((Student)null);

            var result = await _controller.Get(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_WithValidStudent_ReturnsCreatedAtAction()
        {
            var student = new Student { FirstName = "Jan", LastName = "Kowalski", Email = "jan@example.com" };
            _mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Student>()))
                .ReturnsAsync(student);

            var result = await _controller.Create(student);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Student>(createdAtActionResult.Value);
            Assert.Equal("Get", createdAtActionResult.ActionName);
            Assert.Equal(student.Id, createdAtActionResult.RouteValues["id"]);
        }

        [Fact]
        public async Task Update_WithValidStudent_ReturnsNoContent()
        {
            var student = new Student { Id = 1, FirstName = "Jan", LastName = "Kowalski", Email = "jan@example.com" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(student);

            var result = await _controller.Update(1, student);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_WithInvalidId_ReturnsBadRequest()
        {
            var student = new Student { Id = 1, FirstName = "Jan", LastName = "Kowalski", Email = "jan@example.com" };

            var result = await _controller.Update(2, student);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_WithValidId_ReturnsNoContent()
        {
            var student = new Student { Id = 1, FirstName = "Jan", LastName = "Kowalski", Email = "jan@example.com" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(student);

            var result = await _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_WithInvalidId_ReturnsNotFound()
        {
            _mockRepo.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync((Student)null);

            var result = await _controller.Delete(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
} 