using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class GraphQLStudentsModel : PageModel
{
    public List<StudentDto> Students { get; set; }

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
                FirstName = s.GetProperty("firstName").GetString(),
                LastName = s.GetProperty("lastName").GetString(),
                Email = s.GetProperty("email").GetString()
            });
        }
    }

    public class StudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
} 