using Domain.DataAccess;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private DatabaseDbContext context;

        public InstructorRepository(DatabaseDbContext context)
        {
            this.context = context;
        }
        public async Task CreateInstructortAsync(Instructor instructor)
        {
            await context.Instructors.AddAsync(instructor);
            await context.SaveChangesAsync();
        }

        public async Task DeleteInstructorAsync(int instructorId)
        {
            var instructor = await context.Instructors.FindAsync(instructorId);
            context.Instructors.Remove(instructor);
            await context.SaveChangesAsync();
        }

        public async Task<Instructor> GetInstructorByIdAsync(int instructorId)
        {
            var instructor = await context.Instructors.FindAsync(instructorId);
            return instructor;
        }

        public async Task<IEnumerable<Instructor>> GetInstructorsAsync()
        {
            var instructors = await context.Instructors.ToListAsync();
            return instructors;
        }

        public async Task<IEnumerable<ClassModel>> GetClassesAsync()
        {
            var classes = await context.ClassModels.Include(j => j.Subject).ToListAsync();
            return classes;
        }
        public async Task<IEnumerable<ClassModel>> GetInstructorClassesAsync(int instructorId)
        {
            var classes = await GetClassesAsync();
            var instructorClasses = classes.Where(i => i.InstructorID == instructorId);
            return instructorClasses;
        }

        public async Task UpdateInstructorAsync(Instructor instructor)
        {
            context.Instructors.Update(instructor);
            await context.SaveChangesAsync();
        }
    }
}
