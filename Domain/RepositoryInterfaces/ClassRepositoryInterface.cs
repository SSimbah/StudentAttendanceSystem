using Domain.Entities;
namespace Domain.RepositoryInterfaces
{
    public interface IClassRepository
    {
        Task<IEnumerable<ClassModel>> GetClassesAsync();
        Task<ClassModel> GetClassByIdAsync(int classId);
        Task CreateClassesAsync(ClassModel classModel);
        Task UpdateClassesAsync(ClassModel classModel);
        Task DeleteClassAsync(int classId);
        Task<List<Subject>> GetSubjects();
        //List<Instructor> GetInstructors();
        Task CheckInputAsync(ClassModel classModel);
    }
}
