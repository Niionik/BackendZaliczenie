using Application.Repositories;
using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class GetAllStudentsUseCase
    {
        private readonly IStudentRepository _studentRepository;

        public GetAllStudentsUseCase(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<Student>> ExecuteAsync()
        {
            return await _studentRepository.GetAllAsync();
        }
    }
} 