using Domain.Entities;

namespace Domain.RepositoryInterfaces
{
    public interface IStudentClassRepository
    {
        Task<IEnumerable<ClassStudent>> GetStudentClassesAsync(int id);
        Task CreateStudentClassAsync(int studentId, int classId);
        Task DeleteStudentClassAsync(int classStudentId);

        Task<List<ClassModel>> GetAvailableClassesAsync(int studentId);
    }
}
