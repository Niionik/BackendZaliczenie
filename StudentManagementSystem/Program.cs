using Application.Repositories;
using Domain;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Dodajemy obsługę stron Razor
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();

// Dodajemy konfigurację API
builder.Configuration["ApiBaseUrl"] = "http://localhost:5000";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("StudentManagementDb"));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not found")))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<Student>()
    .AddType<Course>()
    .AddType<Enrollment>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Wyłączamy przekierowanie HTTPS w środowisku deweloperskim
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseRouting();

// Konfiguracja routingu
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Przekierowanie z głównego adresu na index.html
app.MapGet("/", async context =>
{
    context.Response.Redirect("/index.html", permanent: true);
    await Task.CompletedTask;
});

// Dodajemy przekierowanie z /login na /login.html
app.MapGet("/login", context =>
{
    context.Response.Redirect("/login.html");
    return Task.CompletedTask;
});

app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();

    if (!context.Students.Any())
    {
        var students = new List<Student>
        {
            new() { FirstName = "Jan", LastName = "Kowalski", Email = "jan.kowalski@example.com" },
            new() { FirstName = "Anna", LastName = "Nowak", Email = "anna.nowak@example.com" }
        };
        context.Students.AddRange(students);

        var courses = new List<Course>
        {
            new() { Name = "Matematyka", Description = "Podstawy matematyki" },
            new() { Name = "Fizyka", Description = "Podstawy fizyki" }
        };
        context.Courses.AddRange(courses);

        context.SaveChanges();

        var enrollments = new List<Enrollment>
        {
            new() { Student = students[0], Course = courses[0] },
            new() { Student = students[1], Course = courses[1] }
        };
        context.Enrollments.AddRange(enrollments);
        context.SaveChanges();
    }
}

app.Run(); 