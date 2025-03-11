using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public interface IEnrollmentRepository
    {
        Task<Enrollment> GetByIdAsync(int id);
        Task<IEnumerable<Enrollment>> GetAllAsync();
        Task AddAsync(Enrollment enrollment);
        Task UpdateAsync(Enrollment enrollment);
        Task DeleteAsync(int id);
    }
} 