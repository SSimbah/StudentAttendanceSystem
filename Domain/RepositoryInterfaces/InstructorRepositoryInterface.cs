using Domain.Entities;

namespace Domain.RepositoryInterfaces
{
    public interface IInstructorRepository
    {
        Task<IEnumerable<Instructor>> GetInstructorsAsync();
        Task<Instructor> GetInstructorByIdAsync(int instructorId);
        Task CreateInstructortAsync(Instructor instructor);
        Task UpdateInstructorAsync(Instructor instructor);
        Task DeleteInstructorAsync(int instructorId);
        Task<IEnumerable<ClassModel>> GetInstructorClassesAsync(int instructorId);
        Task CheckInputAsync(Instructor instructor);
    }
}
