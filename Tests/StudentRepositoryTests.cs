using Xunit;
using Domain;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

public class StudentRepositoryTests
{
    [Fact]
    public async Task AddAndGetStudent_Works()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb_Students")
            .Options;
        using var context = new AppDbContext(options);
        var repo = new StudentRepository(context);

        var student = new Student { FirstName = "Test", LastName = "User", Email = "test@user.com" };
        await repo.AddAsync(student);

        var all = await repo.GetAllAsync();
        Assert.Single(all);
        Assert.Equal("Test", all.First().FirstName);
    }
} 