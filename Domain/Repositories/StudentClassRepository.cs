using Domain.DataAccess;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    public class StudentClassRepository : IStudentClassRepository
    {
        private DatabaseDbContext context;

        public StudentClassRepository(DatabaseDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ClassStudent>> GetStudentClassesAsync(int id)
        {
            var studentClasses = context.ClassStudents
                .Include(c => c.Class)
                .Include(c => c.Student)
                .Include(c => c.Class.Subject)
                .Include(i => i.Class.Instructor)
                .Where(s => s.StudentID == id);

            return await studentClasses.AsNoTracking().ToListAsync();
        }
        public async Task CreateStudentClassAsync(ClassStudent classStudent)
        {
            await context.ClassStudents.AddAsync(classStudent);
            await context.SaveChangesAsync();
        }
        public async Task DeleteStudentClassAsync(int classStudentId, int studentId) {
            var classStudentModel = await context.ClassStudents.FindAsync(classStudentId);
            if (classStudentModel != null)
            {
                context.ClassStudents.Remove(classStudentModel);
            }

            await context.SaveChangesAsync();
        }
    }
}
