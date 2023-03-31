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

        public async Task UpdateInstructorAsync(Instructor instructor)
        {
            context.Instructors.Update(instructor);
            await context.SaveChangesAsync();
        }
    }
}
