using Domain.DataAccess;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    public class ClassStudentRepository : IClassStudentRepository
    {
        private DatabaseDbContext context;

        public ClassStudentRepository(DatabaseDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ClassStudent>> GetClassStudent(int id)
        {
            var classStudent = from s in context.ClassStudents.Include(c => c.Class).Include(c => c.Student)
                               select s;

            classStudent = classStudent.Where(s => s.ClassID == id);

            return await classStudent.AsNoTracking().ToListAsync();
        }
    }
}
