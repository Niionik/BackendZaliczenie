using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class GraphQLStudentsModel : PageModel
{
    public List<StudentDto> Students { get; set; } = new();

    public async Task OnPostAsync()
    {
        using var client = new HttpClient();
        var query = new
        {
            query = "{ students { id firstName lastName email } }"
        };
        var content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("http://localhost:5001/graphql", content); // zmień port jeśli inny!
        var json = await response.Content.ReadAsStringAsync();
        var doc = JsonDocument.Parse(json);
        Students = new List<StudentDto>();
        foreach (var s in doc.RootElement.GetProperty("data").GetProperty("students").EnumerateArray())
        {
            Students.Add(new StudentDto
            {
                Id = s.GetProperty("id").GetInt32(),
                FirstName = s.GetProperty("firstName").GetString() ?? string.Empty,
                LastName = s.GetProperty("lastName").GetString() ?? string.Empty,
                Email = s.GetProperty("email").GetString() ?? string.Empty
            });
        }
    }

    public class StudentDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
    }
} 