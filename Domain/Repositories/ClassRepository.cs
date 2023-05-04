using Domain.DataAccess;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private DatabaseDbContext context;

        public ClassRepository(DatabaseDbContext context)
        {
            this.context = context;
        }
        public async Task CheckInputAsync(ClassModel classModel)
        {
            context.Entry(classModel).State = EntityState.Modified;
        }
        public async Task<IEnumerable<ClassModel>> GetClassesAsync()
        {
            var classModel = await context.ClassModels
                .Include(c => c.Instructor)
                .Include(j => j.Subject).
                ToListAsync();
            return classModel;
        }
        public async Task<ClassModel> GetClassByIdAsync(int classId)
        {
            var classModel = await context.ClassModels.FindAsync(classId);
            return classModel;
        }
        public async Task CreateClassesAsync(ClassModel classModel)
        {
            await context.ClassModels.AddAsync(classModel);
            await context.SaveChangesAsync();
        }
        public async Task UpdateClassesAsync(ClassModel classModel)
        {
            context.ClassModels.Update(classModel);
            await context.SaveChangesAsync();
        }
        public async Task DeleteClassAsync(int classId)
        {
            var classModel = await context.ClassModels.FindAsync(classId);
            context.ClassModels.Remove(classModel);
            await context.SaveChangesAsync();
        }
        public List<Subject> GetSubjects()
        {
            var subject = context.Subjects.ToList();
            return subject;
        }
        public List<Instructor> GetInstructors()
        {
            var intructors = context.Instructors.ToList();
            return intructors;
        }
    }
}