using Domain.Entities;

namespace Domain.RepositoryInterfaces
{
    public interface IStudentClassRepository
    {
        Task<IEnumerable<ClassStudent>> GetStudentClassesAsync(int id);
        Task CreateStudentClassAsync(ClassStudent classStudent);
        Task DeleteStudentClassAsync(int classStudentId, int studentId);
    }
}
