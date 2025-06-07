using Microsoft.AspNetCore.Mvc;
using Moq;
using Application.Repositories;
using Domain;
using StudentManagementSystem.Controllers;
using Xunit;

namespace Tests.Controllers
{
    public class EnrollmentsControllerTests
    {
        private readonly Mock<IEnrollmentRepository> _mockRepo;
        private readonly EnrollmentsController _controller;

        public EnrollmentsControllerTests()
        {
            _mockRepo = new Mock<IEnrollmentRepository>();
            _controller = new EnrollmentsController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithEnrollments()
        {
            var enrollments = new List<Enrollment>
            {
                new Enrollment { Id = 1, StudentId = 1, CourseId = 1 },
                new Enrollment { Id = 2, StudentId = 2, CourseId = 1 }
            };

            _mockRepo.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(enrollments);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Enrollment>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task Get_WithValidId_ReturnsOkResult()
        {
            var enrollment = new Enrollment { Id = 1, StudentId = 1, CourseId = 1 };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(enrollment);

            var result = await _controller.Get(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Enrollment>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task Get_WithInvalidId_ReturnsNotFound()
        {
            _mockRepo.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync((Enrollment)null);

            var result = await _controller.Get(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_WithValidEnrollment_ReturnsCreatedAtAction()
        {
            var enrollment = new Enrollment { StudentId = 1, CourseId = 1 };
            _mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Enrollment>()))
                .ReturnsAsync(enrollment);

            var result = await _controller.Create(enrollment);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Enrollment>(createdAtActionResult.Value);
            Assert.Equal("Get", createdAtActionResult.ActionName);
            Assert.Equal(enrollment.Id, createdAtActionResult.RouteValues["id"]);
        }

        [Fact]
        public async Task Update_WithValidEnrollment_ReturnsNoContent()
        {
            var enrollment = new Enrollment { Id = 1, StudentId = 1, CourseId = 1 };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(enrollment);

            var result = await _controller.Update(1, enrollment);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_WithInvalidId_ReturnsBadRequest()
        {
            var enrollment = new Enrollment { Id = 1, StudentId = 1, CourseId = 1 };

            var result = await _controller.Update(2, enrollment);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_WithValidId_ReturnsNoContent()
        {
            var enrollment = new Enrollment { Id = 1, StudentId = 1, CourseId = 1 };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(enrollment);

            var result = await _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_WithInvalidId_ReturnsNotFound()
        {
            _mockRepo.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync((Enrollment)null);

            var result = await _controller.Delete(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
} 