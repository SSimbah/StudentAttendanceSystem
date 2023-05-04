using Domain.Entities;

namespace Domain.RepositoryInterfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<Student> GetStudentByIdAsync(int studentId); 
        Task CreateStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int studentId);
        Task CheckInputAsync(Student student);
    }
}
