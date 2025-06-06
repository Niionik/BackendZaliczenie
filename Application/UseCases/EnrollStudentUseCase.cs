using Application.Repositories;
using Domain;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class EnrollStudentUseCase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollStudentUseCase(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task ExecuteAsync(Enrollment enrollment)
        {
            await _enrollmentRepository.AddAsync(enrollment);
        }
    }
} 