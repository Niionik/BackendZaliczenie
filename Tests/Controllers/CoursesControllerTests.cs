using Microsoft.AspNetCore.Mvc;
using Moq;
using Application.Repositories;
using Domain;
using StudentManagementSystem.Controllers;
using Xunit;

namespace Tests.Controllers
{
    public class CoursesControllerTests
    {
        private readonly Mock<ICourseRepository> _mockRepo;
        private readonly CoursesController _controller;

        public CoursesControllerTests()
        {
            _mockRepo = new Mock<ICourseRepository>();
            _controller = new CoursesController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithCourses()
        {
            var courses = new List<Course>
            {
                new Course { Id = 1, Name = "Matematyka", Description = "Podstawy matematyki" },
                new Course { Id = 2, Name = "Informatyka", Description = "Podstawy informatyki" }
            };

            _mockRepo.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(courses);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Course>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task Get_WithValidId_ReturnsOkResult()
        {
            var course = new Course { Id = 1, Name = "Matematyka", Description = "Podstawy matematyki" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(course);

            var result = await _controller.Get(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Course>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task Get_WithInvalidId_ReturnsNotFound()
        {
            _mockRepo.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync((Course)null);

            var result = await _controller.Get(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_WithValidCourse_ReturnsCreatedAtAction()
        {
            var course = new Course { Name = "Matematyka", Description = "Podstawy matematyki" };
            _mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Course>()))
                .ReturnsAsync(course);

            var result = await _controller.Create(course);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Course>(createdAtActionResult.Value);
            Assert.Equal("Get", createdAtActionResult.ActionName);
            Assert.Equal(course.Id, createdAtActionResult.RouteValues["id"]);
        }

        [Fact]
        public async Task Update_WithValidCourse_ReturnsNoContent()
        {
            var course = new Course { Id = 1, Name = "Matematyka", Description = "Podstawy matematyki" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(course);

            var result = await _controller.Update(1, course);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_WithInvalidId_ReturnsBadRequest()
        {
            var course = new Course { Id = 1, Name = "Matematyka", Description = "Podstawy matematyki" };

            var result = await _controller.Update(2, course);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_WithValidId_ReturnsNoContent()
        {
            var course = new Course { Id = 1, Name = "Matematyka", Description = "Podstawy matematyki" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(course);

            var result = await _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_WithInvalidId_ReturnsNotFound()
        {
            _mockRepo.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync((Course)null);

            var result = await _controller.Delete(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
} 