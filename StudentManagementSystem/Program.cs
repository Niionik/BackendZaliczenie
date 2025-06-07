using Infrastructure;
using Infrastructure.Repositories;
using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// EF InMemory
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TestDb"));

// Repozytoria
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

// JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey12345"))
    };
});

builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Dodawanie przykładowych danych do bazy InMemory
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Infrastructure.AppDbContext>();

    // Przykładowi studenci
    var student1 = new Domain.Student { FirstName = "Jan", LastName = "Kowalski", Email = "jan.kowalski@example.com" };
    var student2 = new Domain.Student { FirstName = "Anna", LastName = "Nowak", Email = "anna.nowak@example.com" };
    var student3 = new Domain.Student { FirstName = "Piotr", LastName = "Wiśniewski", Email = "piotr.wisniewski@example.com" };
    var student4 = new Domain.Student { FirstName = "Maria", LastName = "Dąbrowska", Email = "maria.dabrowska@example.com" };
    var student5 = new Domain.Student { FirstName = "Tomasz", LastName = "Lewandowski", Email = "tomasz.lewandowski@example.com" };
    db.Students.AddRange(student1, student2, student3, student4, student5);

    // Przykładowe kursy
    var course1 = new Domain.Course { Name = "Matematyka", Description = "Podstawy matematyki" };
    var course2 = new Domain.Course { Name = "Informatyka", Description = "Podstawy informatyki" };
    var course3 = new Domain.Course { Name = "Programowanie w C#", Description = "Zaawansowane programowanie w języku C#" };
    var course4 = new Domain.Course { Name = "Bazy danych", Description = "Projektowanie i zarządzanie bazami danych" };
    var course5 = new Domain.Course { Name = "Algorytmy i struktury danych", Description = "Podstawowe algorytmy i struktury danych" };
    var course6 = new Domain.Course { Name = "Sieci komputerowe", Description = "Podstawy sieci komputerowych" };
    var course7 = new Domain.Course { Name = "Systemy operacyjne", Description = "Architektura i działanie systemów operacyjnych" };
    var course8 = new Domain.Course { Name = "Sztuczna inteligencja", Description = "Wprowadzenie do sztucznej inteligencji" };
    var course9 = new Domain.Course { Name = "Programowanie webowe", Description = "Tworzenie aplikacji internetowych" };
    var course10 = new Domain.Course { Name = "Bezpieczeństwo IT", Description = "Podstawy bezpieczeństwa w systemach IT" };
    db.Courses.AddRange(course1, course2, course3, course4, course5, course6, course7, course8, course9, course10);

    db.SaveChanges();

    // Przykładowe zapisy
    var enrollment1 = new Domain.Enrollment { StudentId = student1.Id, CourseId = course1.Id };
    var enrollment2 = new Domain.Enrollment { StudentId = student1.Id, CourseId = course3.Id };
    var enrollment3 = new Domain.Enrollment { StudentId = student2.Id, CourseId = course2.Id };
    var enrollment4 = new Domain.Enrollment { StudentId = student2.Id, CourseId = course4.Id };
    var enrollment5 = new Domain.Enrollment { StudentId = student3.Id, CourseId = course5.Id };
    var enrollment6 = new Domain.Enrollment { StudentId = student3.Id, CourseId = course7.Id };
    var enrollment7 = new Domain.Enrollment { StudentId = student4.Id, CourseId = course6.Id };
    var enrollment8 = new Domain.Enrollment { StudentId = student4.Id, CourseId = course8.Id };
    var enrollment9 = new Domain.Enrollment { StudentId = student5.Id, CourseId = course9.Id };
    var enrollment10 = new Domain.Enrollment { StudentId = student5.Id, CourseId = course10.Id };
    db.Enrollments.AddRange(enrollment1, enrollment2, enrollment3, enrollment4, enrollment5, 
                          enrollment6, enrollment7, enrollment8, enrollment9, enrollment10);
    db.SaveChanges();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware logujący nagłówki HTTP
app.Use(async (context, next) =>
{
    var headers = context.Request.Headers;
    foreach (var header in headers)
    {
        Console.WriteLine($"{header.Key}: {header.Value}");
    }
    await next.Invoke();
});

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = true,
    DefaultContentType = "text/html",
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Cache-Control", "no-cache, no-store");
        ctx.Context.Response.Headers.Append("Pragma", "no-cache");
    }
});

app.MapRazorPages();
app.MapGraphQL("/graphql");

app.MapGet("/", (HttpContext ctx) => {
    ctx.Response.Redirect("/index.html");
    return Results.Empty;
});

app.MapPost("/api/auth/login", async (HttpContext context) =>
{
    var loginData = await context.Request.ReadFromJsonAsync<LoginRequest>();
    if (loginData?.Username == "admin" && loginData?.Password == "admin")
    {
        var token = "admin-token";
        return Results.Ok(new { token });
    }
    else if (loginData?.Username == "user" && loginData?.Password == "user")
    {
        var token = "user-token";
        return Results.Ok(new { token });
    }
    return Results.Unauthorized();
});

app.MapControllers();

app.Run(); 