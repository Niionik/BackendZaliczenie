using Application.Repositories;
using Domain;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class CreateStudentUseCase
    {
        private readonly IStudentRepository _studentRepository;

        public CreateStudentUseCase(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task ExecuteAsync(Student student)
        {
            await _studentRepository.AddAsync(student);
        }
    }
} 