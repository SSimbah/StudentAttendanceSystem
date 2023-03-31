using Domain.DataAccess;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private DatabaseDbContext context;

        public StudentRepository(DatabaseDbContext context) {
            this.context = context;
        }
        public async Task CreateStudentAsync(Student student)
        {
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            var student = await context.Students.FindAsync(studentId);
            context.Students.Remove(student);
            await context.SaveChangesAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            var student = await context.Students.FindAsync(studentId);
            return student;
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            var students = await context.Students.ToListAsync();
            return students;
        }

        public async Task UpdateStudentAsync(Student student)
        {
            context.Students.Update(student);
            await context.SaveChangesAsync();
        }
    }
}
